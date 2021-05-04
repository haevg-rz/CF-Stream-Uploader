using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CfStreamUploader.Core.Models;
using Newtonsoft.Json;

namespace CfStreamUploader.Core
{
    public class ConfigManager
    {
        private static string ConfigPath { get; } =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\CfStreamUploader\Config.JSON";

        private readonly string cfStreamUploaderGuiPath =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\CfStreamUploader";

        public Config ReadConfig()
        {
            if (!File.Exists(ConfigPath))
                return null;

            var jsonString = File.ReadAllText(ConfigPath);
            var config = JsonConvert.DeserializeObject<Config>(jsonString);
            return config;
        }

        public void WriteConfig(Config config)
        {
            if (!Directory.Exists(cfStreamUploaderGuiPath))
                Directory.CreateDirectory(cfStreamUploaderGuiPath);

            var jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(ConfigPath, jsonString);

        }

    }
}
