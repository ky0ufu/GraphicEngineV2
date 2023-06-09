﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Configuration
    {

        private string filePath;

        private Dictionary<string, dynamic> configuration;

        public Configuration() 
        {
            configuration = new Dictionary<string, dynamic>();
        }


        public Configuration(string filePath)
        {
            this.filePath = filePath;
        }

        public void SetVariable(string variable, dynamic value)
        {
            if (configuration.ContainsKey(variable))
                configuration[variable] = value;
        }

        public dynamic GetVariable(string variable)
        {
            return configuration[variable];
        }

        public void ExecuteFile(string filePath) 
        {
            this.filePath = filePath;
        }

        public void Save(string filePath) 
        {
            string Path = filePath;
            if (filePath == null)
                Path = this.filePath;
            

        }
    }
}
