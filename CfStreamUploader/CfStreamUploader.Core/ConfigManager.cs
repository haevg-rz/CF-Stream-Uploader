using CfStreamUploader.Core.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

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
                this.SetDefaultConfigItems();
                this.WriteConfig();
                return;
            }

            var jsonString = File.ReadAllText(Path.Combine(this.CfStreamUploaderPath, configFile));
            this.Config = JsonConvert.DeserializeObject<Config>(jsonString);

            this.SetDefaultConfigItems();
        }

        public void UpdateConfig(Config config)
        {
            this.SetConfig(config);
            this.WriteConfig();
        }

        public void OpenConfig()
        {
            if (!File.Exists(this.CfStreamUploaderPath))
                this.WriteConfig();

            var psi = new ProcessStartInfo
            {
                FileName = Path.Combine(this.CfStreamUploaderPath, "Config.json"),
                UseShellExecute = true,
                Verb = "open"
            };
            var p = Process.Start(psi);
            p.WaitForInputIdle();
            p.WaitForExit();

            if (p.HasExited) this.ReadConfig();
        }

        #endregion

        #region private

        private void SetConfig(Config config)
        {
            this.Config = new Config(config);
        }

        private void WriteConfig()
        {
            if (!Directory.Exists(this.CfStreamUploaderPath))
                Directory.CreateDirectory(this.CfStreamUploaderPath);

            var jsonString = JsonConvert.SerializeObject(this.Config, Formatting.Indented);
            File.WriteAllText(Path.Combine(this.CfStreamUploaderPath, configFile), jsonString);
        }

        private void SetDefaultConfigItems()
        {
            if (this.Config.AccessRules.Country.Countries.Count == 0)
                this.Config.AccessRules.Country.Countries.Add("DE");

            if (this.Config.AccessRules.Ip.Ips.Count == 0)
                this.Config.AccessRules.Ip.Ips.Add("127.0.0.1");

            if (this.Config.AccessRules.ExpiresIn == 0)
                this.Config.AccessRules.ExpiresIn = 365;
        }

        #endregion
    }
}