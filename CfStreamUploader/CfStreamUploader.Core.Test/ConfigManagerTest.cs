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
        #region filds

        private readonly string solutionDir =
            Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory));

        private const string configPath = "Config.json";

        #endregion

        #region props

        private ConfigManager ConfigManager { get; } = new ConfigManager();
        private Samples Samples { get; } = new Samples();

        #endregion

        #region tests

        [Fact]
        public void ReadConfigTest()
        {
            #region Assign

            this.SetUp();

            var config = new Config(this.Samples.ConfigSample);

            var jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(Path.Combine(this.solutionDir, configPath), jsonString);

            #endregion

            #region Act

            this.ConfigManager.ReadConfig();

            #endregion

            #region Assert

            Equals(this.Samples.ConfigSample, this.ConfigManager.Config);
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

            Equals(this.Samples.DefaultConfigSample, this.ConfigManager.Config);
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

            this.ConfigManager.UpdateConfig(this.Samples.DefaultConfigSample);

            #endregion

            #region Assert

            Equals(File.Exists(Path.Combine(this.solutionDir + configPath)), this.Equals(true));
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