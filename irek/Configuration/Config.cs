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

        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        /// <param name="configfile">The configfile.</param>
        public Config(string configfile)
        {
            Map = ConfigReader.Read(configfile);
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>string value</returns>
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

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="val">The val.</param>
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
