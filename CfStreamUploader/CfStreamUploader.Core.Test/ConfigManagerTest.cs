using CfStreamUploader.Core.Models;
using CfStreamUploader.Core.Test.TestSamples;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace CfStreamUploader.Core.Test
{
    public class ConfigManagerTest
    {
        #region fields

        private readonly string solutionDir =
            Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory));

        private const string configPath = "Config.json";

        #endregion

        #region props

        private ConfigManager ConfigManager { get; } = new ConfigManager();

        #endregion

        #region tests

        [Fact]
        public void ReadConfigTest()
        {
            #region Assign

            this.SetUp();

            var config = new Config(Samples.ConfigSample);

            var jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(Path.Combine(this.solutionDir, configPath), jsonString);

            #endregion

            #region Act

            this.ConfigManager.ReadConfig();

            #endregion

            #region Assert

            Assert.Equal(this.ConfigManager.Config.AccessRules.Ip.Type, Samples.ConfigSample.AccessRules.Ip.Type);
            Assert.Equal(this.ConfigManager.Config.AccessRules.Ip.Action, Samples.ConfigSample.AccessRules.Ip.Action);
            Assert.Equal(this.ConfigManager.Config.AccessRules.Country.Type,
                Samples.ConfigSample.AccessRules.Country.Type);
            Assert.Equal(this.ConfigManager.Config.AccessRules.Country.Action,
                Samples.ConfigSample.AccessRules.Country.Action);
            Assert.Equal(this.ConfigManager.Config.AccessRules.Any.Type, Samples.ConfigSample.AccessRules.Any.Type);
            Assert.Equal(this.ConfigManager.Config.AccessRules.Any.Action, Samples.ConfigSample.AccessRules.Any.Action);

            Assert.Equal(this.ConfigManager.Config.UserSettings.KeyId, Samples.ConfigSample.UserSettings.KeyId);
            Assert.Equal(this.ConfigManager.Config.UserSettings.CfAccount, Samples.ConfigSample.UserSettings.CfAccount);
            Assert.Equal(this.ConfigManager.Config.UserSettings.PrivateKey,
                Samples.ConfigSample.UserSettings.PrivateKey);
            Assert.Equal(this.ConfigManager.Config.UserSettings.CfToken, Samples.ConfigSample.UserSettings.CfToken);

            Assert.Equal(this.ConfigManager.Config.IsDarkmode, Samples.ConfigSample.IsDarkmode);

            this.TearDown();

            #endregion
        }

        [Fact]
        public void ReadConfigNotExistsTest()
        {
            #region Assign

            this.SetUp();

            #endregion

            #region Act

            this.ConfigManager.ReadConfig();

            #endregion

            #region Assert

            Assert.Equal(this.ConfigManager.Config.AccessRules.Ip.Type,
                Samples.DefaultConfigSample.AccessRules.Ip.Type);
            Assert.Equal(this.ConfigManager.Config.AccessRules.Ip.Action,
                Samples.DefaultConfigSample.AccessRules.Ip.Action);
            Assert.Equal(this.ConfigManager.Config.AccessRules.Country.Type,
                Samples.DefaultConfigSample.AccessRules.Country.Type);
            Assert.Equal(this.ConfigManager.Config.AccessRules.Country.Action,
                Samples.DefaultConfigSample.AccessRules.Country.Action);
            Assert.Equal(this.ConfigManager.Config.AccessRules.Any.Type,
                Samples.DefaultConfigSample.AccessRules.Any.Type);
            Assert.Equal(this.ConfigManager.Config.AccessRules.Any.Action,
                Samples.DefaultConfigSample.AccessRules.Any.Action);

            Assert.Equal(this.ConfigManager.Config.UserSettings.KeyId, Samples.DefaultConfigSample.UserSettings.KeyId);
            Assert.Equal(this.ConfigManager.Config.UserSettings.CfAccount,
                Samples.DefaultConfigSample.UserSettings.CfAccount);
            Assert.Equal(this.ConfigManager.Config.UserSettings.PrivateKey,
                Samples.DefaultConfigSample.UserSettings.PrivateKey);
            Assert.Equal(this.ConfigManager.Config.UserSettings.CfToken,
                Samples.DefaultConfigSample.UserSettings.CfToken);

            Assert.Equal(this.ConfigManager.Config.IsDarkmode, Samples.DefaultConfigSample.IsDarkmode);

            this.TearDown();

            #endregion
        }

        [Fact]
        public void UpdateConfigTest()
        {
            #region Assign

            this.SetUp();

            #endregion

            #region Act

            this.ConfigManager.UpdateConfig(Samples.DefaultConfigSample);

            #endregion

            #region Assert

            Assert.Equal(File.Exists(Path.Combine(this.solutionDir + configPath)), this.Equals(true));
            this.TearDown();

            #endregion
        }

        #endregion

        #region SetUp

        private void SetUp()
        {
            this.ConfigManager.CfStreamUploaderPath = this.solutionDir;
            this.DeletePath();
        }

        private void TearDown()
        {
            this.DeletePath();
        }

        private void DeletePath()
        {
            if (File.Exists(Path.Combine(this.solutionDir, configPath)))
                File.Delete(Path.Combine(this.solutionDir, configPath));
        }

        #endregion
    }
}