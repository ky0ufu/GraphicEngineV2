using System;
using System.Collections.Generic;
using System.Configuration;

namespace Engine
{
    public class EngineConfiguration
    {

        private string filePath;

        private Dictionary<string, dynamic> configuration;

        public dynamic this[string key]
        {
            get { return configuration[key]; }
        }

        public EngineConfiguration() 
        {
            filePath = "..\\..\\config\\Default.config";
            ChangeFilePath(filePath);
            configuration = new Dictionary<string, dynamic>();
            ReadConfig();
        }


        public EngineConfiguration(string filePath)
        {
            this.filePath = filePath;
            ChangeFilePath(filePath);
            ReadConfig();
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
            ChangeFilePath(filePath);
            ReadConfig();
        }

        private void ReadConfig()
        {

            var applicationSettings = ConfigurationManager.AppSettings.AllKeys;
            foreach (var key in applicationSettings)
            {
                configuration[key] = ConfigurationManager.AppSettings[key];
            }
        }
        private static void ChangeFilePath(string newFilePath)
        {
            if (newFilePath == "App.config")
                throw new Exception("banned path");

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            AppSettingsSection appSettings = config.AppSettings;

            appSettings.File = newFilePath;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(appSettings.SectionInformation.Name);
        }


    }
}
