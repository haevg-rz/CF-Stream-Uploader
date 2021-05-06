using System.IO;
using NUnit.Framework;

namespace CfStreamUploader.Core.Test
{
    [TestFixture]
    public class ConfigManagerTest
    {
        private ConfigManager ConfigManager { get; } = new ConfigManager();

        private readonly string solutionDir =
            Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory));

        private const string txtPath = "\\Config.txt";

        [Test]
        public void ReadConfigTest()
        {

        }

        [Test]
        public void UpdateConfigTest()
        {

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
            this.DeletePath();
        }

        [TearDown]
        public void TearDown()
        {
            this.DeletePath();
        }

        public void DeletePath()
        {
            if (File.Exists(Path.Combine(Path.Combine(this.solutionDir + txtPath))))
                File.Delete(Path.Combine(Path.Combine(this.solutionDir + txtPath)));
        }
    }
}
