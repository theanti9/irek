using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace irek.Configuration
{
    public class ConfigReader
    {
        /// <summary>
        /// Reads the specified configfile.
        /// </summary>
        /// <param name="configfile">The configfile.</param>
        /// <returns>Hashtable</returns>
        public static Hashtable Read(string configfile)
        {
            if (!File.Exists(configfile))
            {
                throw new Errors.NoSuchConfigurationFileException("configuration file is missing.");
            }
            Regex rx = new Regex(@"([a-zA-Z]+)=(.*?)\;", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            StreamReader sr = File.OpenText(configfile);
            string text = sr.ReadToEnd();
            sr.Close();

            MatchCollection matches = rx.Matches(text);

            Hashtable h = new Hashtable();

            foreach (Match match in matches) 
            {
                GroupCollection groups = match.Groups;
                h.Add(groups[1].Value, groups[2].Value);
            }
            return h;
        }
    }
}
