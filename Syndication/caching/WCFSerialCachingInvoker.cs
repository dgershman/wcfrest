using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.ServiceModel;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel.Web;

namespace versomas.net.services.syndication.caching
{
    public class WCFSerialCachingInvoker : IOperationInvoker
    {
        private readonly IOperationInvoker invoker;        

        public WCFSerialCachingInvoker(IOperationInvoker invoker)
        {
            this.invoker = invoker;            
        }

        public object[] AllocateInputs()
        {
            return invoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            var key = new StringBuilder(OperationContext.Current.IncomingMessageProperties.Via.ToString());            
            WCFCacheItem cacheItem = null;
            object retval = null;
            outputs = null;
            if (!string.IsNullOrEmpty(key.ToString()))
            {
                cacheItem = WCFCacheManager.GetItemFromCache(key.ToString()) as WCFCacheItem;
                if (cacheItem != null)
                {
                    retval = CloneStream((MemoryStream)cacheItem.ReturnValue);
                    outputs = cacheItem.Outputs;
                    WebOperationContext.Current.OutgoingResponse.ContentType = cacheItem.ContentType;
#if DEBUG
                    Debug.Print(MemoryStreamToText((MemoryStream)retval));
#endif
                }
                else
                {
                    retval = invoker.Invoke(instance, inputs, out outputs);
#if DEBUG
                    Debug.Print(MemoryStreamToText((MemoryStream)retval));
#endif                    
                    MemoryStream streamCopy = CloneStream((MemoryStream)retval);
                    cacheItem = new WCFCacheItem();
                    cacheItem.Outputs = outputs;
                    cacheItem.ReturnValue = streamCopy;
                    cacheItem.ContentType = WebOperationContext.Current.OutgoingResponse.ContentType;
                    WCFCacheManager.SetItemInCache(key.ToString(), cacheItem);
                }
            }
            return retval;                        
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return invoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            return invoker.InvokeEnd(instance, out outputs, result);
        }

        public bool IsSynchronous
        {
            get { return invoker.IsSynchronous; }
        }

        private string MemoryStreamToText(MemoryStream ms)
        {
            var stream = (MemoryStream)ms;            
            return Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);            
        }

        public MemoryStream CloneStream(MemoryStream src)
        {
            var dest = new MemoryStream();
            src.WriteTo(dest);
            dest.Position = 0;
            return dest;
        }

    }
}
