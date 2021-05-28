using CfStreamUploader.Core.Models;
using CfStreamUploader.Core.Test.TestSamples;
using Xunit;

namespace CfStreamUploader.Core.Test.ModelTests
{
    public class ConfigTest
    {
        #region tests

        [Fact]
        public void DefaultConstructorTest()
        {
            #region Assign

            var config = new Config();

            #endregion

            #region Act

            #endregion

            Equals(config.AccessRules, new AccessRules());
            Equals(config.UserSettings, new UserSettings());
            Equals(config.IsDarkmode, false);

            #region Assert

            #endregion
        }

        [Fact]
        public void ParameterizedConstructorConfigTest()
        {
            #region Assign

            var config = new Config(Samples.ConfigSample.UserSettings, Samples.ConfigSample.AccessRules,
                Samples.ConfigSample.IsDarkmode);

            #endregion

            #region Act

            #endregion

            Equals(config, Samples.ConfigSample);

            #region Assert

            #endregion
        }

        [Fact]
        public void CopyConstructorConfigTest()
        {
            #region Assign

            var config = new Config(Samples.ConfigSample);

            #endregion

            #region Act

            #endregion

            Equals(config, Samples.ConfigSample);

            #region Assert

            #endregion
        }

        #endregion
    }
}