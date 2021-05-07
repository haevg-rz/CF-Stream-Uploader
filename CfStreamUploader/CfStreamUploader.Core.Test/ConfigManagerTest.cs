using CfStreamUploader.Core.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;

namespace CfStreamUploader.Core.Test
{
    [TestFixture]
    public class ConfigManagerTest
    {
        private ConfigManager ConfigManager { get; } = new ConfigManager();

        private readonly string solutionDir =
            Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory));

        private const string txtPath = "Config.json";

        [TestCase("ReadConfig TestToken", true, "ReadConfig TestToken")]
        [TestCase("ReadConfig TestToken", false, "")]
        public void ReadConfigTest(string token, bool writeFile, string result)
        {
            #region Assign

            var config = new Config(token);

            if(writeFile)
            {
                var jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(Path.Combine(solutionDir,txtPath), jsonString);
            }

            #endregion

            #region Act

            this.ConfigManager.ReadConfig();

            #endregion

            #region Assert

            Assert.AreEqual(result, this.ConfigManager.Config.CfToken);
            #endregion
        }

        [Test]
        public void UpdateConfigTest()
        {
            #region Assign

            #endregion

            #region Act

            this.ConfigManager.UpdateConfig("testToken");

            #endregion

            #region Assert

            Assert.That(File.Exists(Path.Combine(this.solutionDir + txtPath)), Is.EqualTo(true));

            #endregion
        }

        [SetUp]
        public void SetUp()
        {
            this.ConfigManager.ConfigPath = this.solutionDir;
            DeletePath();
        }

        [TearDown]
        public void TearDown()
        {
            DeletePath();
        }

        public void DeletePath()
        {
            var a = Path.Combine(this.solutionDir, txtPath);

            if (File.Exists(Path.Combine(this.solutionDir, txtPath)))
                File.Delete(Path.Combine(this.solutionDir, txtPath));

        }
    }
}
