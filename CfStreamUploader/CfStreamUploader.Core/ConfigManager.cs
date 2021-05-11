using CfStreamUploader.Core.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CfStreamUploader.Core
{
    public class ConfigManager
    {
        public Config Config { get; set; }
        public string CfStreamUploaderPath =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\CfStreamUploader";

        private const string configFile = "Config.json";

        public void ReadConfig()
        {
            if (!File.Exists(Path.Combine(CfStreamUploaderPath, configFile)))
            {
                this.Config = new Config();
                return;
            }

            var jsonString = File.ReadAllText(Path.Combine(CfStreamUploaderPath, configFile));
            this.Config = JsonConvert.DeserializeObject<Config>(jsonString);
        }

        public void UpdateConfig(string cfToken)
        {
            SetConfig(cfToken);
            WriteConfig();
        }

        private void SetConfig(string cfToken)
        {
            this.Config = new Config(cfToken);
        }

        private void WriteConfig()
        {
            if (!Directory.Exists(CfStreamUploaderPath))
                Directory.CreateDirectory(CfStreamUploaderPath);

            var jsonString = JsonConvert.SerializeObject(this.Config, Formatting.Indented);
            File.WriteAllText(Path.Combine(CfStreamUploaderPath , configFile), jsonString);

        }

    }
}
