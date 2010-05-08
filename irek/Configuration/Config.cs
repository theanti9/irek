using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace irek.Configuration
{
    public class Config
    {
        private Hashtable Map;

        public Config(string configfile)
        {
            Map = ConfigReader.Read(configfile);
        }

        public string Get(string key) 
        {
            if (Map.ContainsKey((object)key))
            {
                string val = Map[key].ToString();
                return val;
            }
            else
            {
                throw new Errors.InvalidConfigurationKeyException("No key '" + key + "' was found in your configuration.");
            }
        }

        public void Set(string key, string val)
        {
            if (Map.ContainsKey((object)key))
            {
                Map[key] = (object)val;
            }
            else
            {
                throw new Errors.InvalidConfigurationKeyException("No key '" + key + "' was found in your configuration.");
            }
        }
    }
}
