using Colso.Xrm.DataAttributeUpdater.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Colso.Xrm.DataAttributeUpdater.AppCode
{
    public class SettingFileHandler
    {
        public static bool GetConfigData(out Settings config)
        {
            var allok = SettingsManager.Instance.TryLoad<Settings>(typeof(DataUpdater), out config);

            if (config == null)  config = new Settings();

            return allok;
        }

        public static bool SaveConfigData(Settings config)
        {
            try
            {
                SettingsManager.Instance.Save(typeof(DataUpdater), config);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class Settings
    {
        public Settings()
        {
            this.Sortcolumns = new List<Item<string, int>>();
            this.Entities = new List<Item<string, EntitySettings>>();
        }

        private List<Item<string, int>> _Sortcolumns;
        public List<Item<string, int>> Sortcolumns
        {
            get { return _Sortcolumns; }
            set { _Sortcolumns = value; }
        }


        private List<Item<string, EntitySettings>> _Entities;
        public List<Item<string, EntitySettings>> Entities
        {
            get { return _Entities; }
            set { _Entities = value; }
        }

        public EntitySettings this[string logicalname]
        {
            get
            {
                if (_Entities == null)
                    _Entities = new List<Item<string, EntitySettings>>();

                if (!_Entities.Any(o => o.Key == logicalname))
                    _Entities.Add(new Item<string, EntitySettings>(logicalname, new EntitySettings()));

                return _Entities.Where(o => o.Key == logicalname).Select(o => o.Value).FirstOrDefault();
            }
        }
    }

    public class EntitySettings
    {
        public EntitySettings()
        {
            this.AttributeValues = new Dictionary<string, string>();
            this.Filter = string.Empty;
        }

        public Dictionary<string, string> AttributeValues { get; set; }

        public string Filter { get; set; }
    }
}
