using CfStreamUploader.Core.Models;
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

        #endregion

        #region tests

        [Theory]
        [InlineData("ReadConfig TestToken", true, "ReadConfig TestToken")]
        [InlineData("ReadConfig TestToken", false, "")]
        public void ReadConfigTest(string token, bool writeFile, string result)
        {
            #region Assign

            this.SetUp();

            var config = new Config(token, string.Empty, string.Empty, string.Empty, 0);

            if (writeFile)
            {
                var jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(Path.Combine(this.solutionDir, configPath), jsonString);
            }

            #endregion

            #region Act

            this.ConfigManager.ReadConfig();

            #endregion

            #region Assert

            Equals(result, this.ConfigManager.Config.CfToken);
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

            this.ConfigManager.UpdateConfig("testToken", string.Empty, string.Empty, string.Empty, 0);

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