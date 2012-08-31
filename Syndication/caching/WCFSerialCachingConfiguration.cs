using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Caching;

namespace versomas.net.services.syndication.caching
{
    public class WCFSerialCachingConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("enabled", DefaultValue = "false", IsRequired = false)]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
            set
            {
                this["enabled"] = value;
            }

        }

        [ConfigurationProperty("cachingRules", IsDefaultCollection = false)]
        public WCFSerialCachingRuleElementCollection CachingRules
        {
            get
            {
                WCFSerialCachingRuleElementCollection cachingRulesCollection = (WCFSerialCachingRuleElementCollection)base["cachingRules"];
                return cachingRulesCollection;
            }
        }


        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            base.DeserializeSection(reader);
            // You can add custom processing code here.
        }

        protected override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
        {
            string s =  base.SerializeSection(parentElement, name, saveMode);
            // You can add custom processing code here.
            return s;
        }

    }

    public class WCFSerialCachingRuleElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new WCFSerialCachingRuleElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new WCFSerialCachingRuleElement(elementName);
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((WCFSerialCachingRuleElement)element).Name;
        }
        
        public new string AddElementName
        {
            get
            { return base.AddElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string ClearElementName
        {
            get
            { return base.ClearElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string RemoveElementName
        {
            get
            { return base.RemoveElementName; }
        }

        public new int Count
        {
            get { return base.Count; }
        }


        public WCFSerialCachingRuleElement this[int index]
        {
            get
            {
                return (WCFSerialCachingRuleElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public WCFSerialCachingRuleElement this[string Name]
        {
            get
            {
                return (WCFSerialCachingRuleElement)BaseGet(Name);
            }
        }

        public int IndexOf(WCFSerialCachingRuleElement cachingRule)
        {
            return BaseIndexOf(cachingRule);
        }

        public void Add(WCFSerialCachingRuleElement cachingRule)
        {
            BaseAdd(cachingRule);
            // Add custom code here.
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(WCFSerialCachingRuleElement cachingRule)
        {
            if (BaseIndexOf(cachingRule) >= 0)
                BaseRemove(cachingRule.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }

    }

    public class WCFSerialCachingRuleElement : ConfigurationElement
    {
        public WCFSerialCachingRuleElement() { }

        public WCFSerialCachingRuleElement(string elementName)
        {
            this.Name = elementName;
        }

        public WCFSerialCachingRuleElement(string Name, string Pattern, int CacheTime, string ItemPriority)
        {
            this.Name = Name;
            this.Pattern = Pattern;
            this.CacheTime = CacheTime;
            this.ItemPriority = ItemPriority;
        }

        [ConfigurationProperty("name", DefaultValue = "main", IsKey = true, IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("pattern", DefaultValue = "", IsRequired = true)]        
        public String Pattern
        {
            get
            {
                return (string)this["pattern"];
            }
            set
            {
                this["pattern"] = value;
            }
        }

        [ConfigurationProperty("cacheTime", DefaultValue = "0", IsRequired = true)]
        [IntegerValidator(ExcludeRange = false)]
        public int CacheTime
        {
            get
            {
                return int.Parse(this["cacheTime"].ToString());
            }
            set
            {
                this["cacheTime"] = value;
            }
        }

        [ConfigurationProperty("itemPriority", DefaultValue = "Normal", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public string ItemPriority
        {
            get
            {
                return this["itemPriority"].ToString();
            }
            set
            {
                this["itemPriority"] = value;
            }
        }

        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            base.DeserializeElement(reader, serializeCollectionKey);
            // You can your custom processing code here.
        }


        protected override bool SerializeElement(System.Xml.XmlWriter writer, bool serializeCollectionKey)
        {
            bool ret = base.SerializeElement(writer, serializeCollectionKey);
            // You can enter your custom processing code here.
            return ret;
        }


        protected override bool IsModified()
        {
            bool ret = base.IsModified();
            // You can enter your custom processing code here.
            return ret;
        }

    }



}
