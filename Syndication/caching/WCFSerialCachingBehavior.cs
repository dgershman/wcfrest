using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.ServiceModel.Dispatcher;

namespace versomas.net.services.syndication.caching
{
    public class WCFSerialCachingBehavior : BehaviorExtensionElement, IOperationBehavior, IEndpointBehavior
    {
        #region IOperationBehavior Members        

        public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
            throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            IOperationInvoker invoker = dispatchOperation.Invoker;
            dispatchOperation.Invoker = new WCFSerialCachingInvoker(invoker);
        }

        public void Validate(OperationDescription operationDescription)
        {
            return;
        }

        #endregion

        public override Type BehaviorType
        {
            get { return typeof(WCFSerialCachingBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new WCFSerialCachingBehavior();
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            return;
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            foreach (var op in endpoint.Contract.Operations)
            {
                op.Behaviors.Add(this);
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }
    }
}
