using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace versomas.net.services.syndication.caching
{
    public class ServiceCachingConfiguration : ConfigurationSection
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
        public CachingRuleElementCollection CachingRules
        {
            get
            {
                CachingRuleElementCollection cachingRulesCollection = (CachingRuleElementCollection)base["cachingRules"];
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

    public class CachingRuleElementCollection : ConfigurationElementCollection
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
            return new CachingRuleElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new CachingRuleElement(elementName);
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((CachingRuleElement)element).Name;
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


        public CachingRuleElement this[int index]
        {
            get
            {
                return (CachingRuleElement)BaseGet(index);
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

        new public CachingRuleElement this[string Name]
        {
            get
            {
                return (CachingRuleElement)BaseGet(Name);
            }
        }

        public int IndexOf(CachingRuleElement cachingRule)
        {
            return BaseIndexOf(cachingRule);
        }

        public void Add(CachingRuleElement cachingRule)
        {
            BaseAdd(cachingRule);
            // Add custom code here.
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(CachingRuleElement cachingRule)
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

    public class CachingRuleElement : ConfigurationElement
    {
        public CachingRuleElement() { }

        public CachingRuleElement(string elementName)
        {
            this.Name = elementName;
        }

        public CachingRuleElement(string Name, string Pattern, int CacheTime)
        {
            this.Name = Name;
            this.Pattern = Pattern;
            this.CacheTime = CacheTime;
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
        [IntegerValidator(ExcludeRange= false)]
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
