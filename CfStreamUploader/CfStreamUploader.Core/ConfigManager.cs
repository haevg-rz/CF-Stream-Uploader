using CfStreamUploader.Core.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CfStreamUploader.Core
{
    public class ConfigManager
    {
        #region fields

        public string CfStreamUploaderPath =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\CfStreamUploader";

        private const string configFile = "Config.json";

        #endregion

        #region props

        public Config Config { get; set; }

        #endregion

        #region constructor

        public ConfigManager()
        {
            this.ReadConfig();
        }

        #endregion

        #region public

        public void ReadConfig()
        {
            if (!File.Exists(Path.Combine(this.CfStreamUploaderPath, configFile)))
            {
                this.Config = new Config();
                this.WriteConfig();
                return;
            }

            var jsonString = File.ReadAllText(Path.Combine(this.CfStreamUploaderPath, configFile));
            this.Config = JsonConvert.DeserializeObject<Config>(jsonString);
        }

        public void UpdateConfig(string cfToken, string videoId, string keyId, string privateKey, int expiresIn)
        {
            this.SetConfig(cfToken, videoId, keyId, privateKey, expiresIn);
            this.WriteConfig();
        }

        #endregion

        #region private

        private void SetConfig(string cfToken, string videoId, string keyId, string privateKey, int expiresIn)
        {
            this.Config = new Config(cfToken, videoId, keyId, privateKey, expiresIn);
        }

        private void WriteConfig()
        {
            if (!Directory.Exists(this.CfStreamUploaderPath))
                Directory.CreateDirectory(this.CfStreamUploaderPath);

            var jsonString = JsonConvert.SerializeObject(this.Config, Formatting.Indented);
            File.WriteAllText(Path.Combine(this.CfStreamUploaderPath, configFile), jsonString);
        }

        #endregion
    }
}