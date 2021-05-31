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

            #region Assert

            Assert.Equal(config.AccessRules.Ip.Type, Samples.DefaultConfigSample.AccessRules.Ip.Type);
            Assert.Equal(config.AccessRules.Ip.Action, Samples.DefaultConfigSample.AccessRules.Ip.Action);
            Assert.Equal(config.AccessRules.Country.Type, Samples.DefaultConfigSample.AccessRules.Country.Type);
            Assert.Equal(config.AccessRules.Country.Action, Samples.DefaultConfigSample.AccessRules.Country.Action);
            Assert.Equal(config.AccessRules.Any.Type, Samples.DefaultConfigSample.AccessRules.Any.Type);
            Assert.Equal(config.AccessRules.Any.Action, Samples.DefaultConfigSample.AccessRules.Any.Action);

            Assert.Equal(config.UserSettings.KeyId, Samples.DefaultConfigSample.UserSettings.KeyId);
            Assert.Equal(config.UserSettings.PrivateKey, Samples.DefaultConfigSample.UserSettings.PrivateKey);
            Assert.Equal(config.UserSettings.CfToken, Samples.DefaultConfigSample.UserSettings.CfToken);
            Assert.Equal(config.UserSettings.CfAccount, Samples.DefaultConfigSample.UserSettings.CfAccount);

            Assert.Equal(config.IsDarkmode, false);

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

            #region Assert

            Assert.Equal(config.AccessRules.Ip.Type, Samples.ConfigSample.AccessRules.Ip.Type);
            Assert.Equal(config.AccessRules.Ip.Action, Samples.ConfigSample.AccessRules.Ip.Action);
            Assert.Equal(config.AccessRules.Country.Type, Samples.ConfigSample.AccessRules.Country.Type);
            Assert.Equal(config.AccessRules.Country.Action, Samples.ConfigSample.AccessRules.Country.Action);
            Assert.Equal(config.AccessRules.Any.Type, Samples.ConfigSample.AccessRules.Any.Type);
            Assert.Equal(config.AccessRules.Any.Action, Samples.ConfigSample.AccessRules.Any.Action);

            Assert.Equal(config.UserSettings.KeyId, Samples.ConfigSample.UserSettings.KeyId);
            Assert.Equal(config.UserSettings.PrivateKey, Samples.ConfigSample.UserSettings.PrivateKey);
            Assert.Equal(config.UserSettings.CfToken, Samples.ConfigSample.UserSettings.CfToken);
            Assert.Equal(config.UserSettings.CfAccount, Samples.ConfigSample.UserSettings.CfAccount);

            Assert.Equal(config.IsDarkmode, false);

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

            #region Assert

            Assert.Equal(config.AccessRules.Ip.Type, Samples.ConfigSample.AccessRules.Ip.Type);
            Assert.Equal(config.AccessRules.Ip.Action, Samples.ConfigSample.AccessRules.Ip.Action);
            Assert.Equal(config.AccessRules.Country.Type, Samples.ConfigSample.AccessRules.Country.Type);
            Assert.Equal(config.AccessRules.Country.Action, Samples.ConfigSample.AccessRules.Country.Action);
            Assert.Equal(config.AccessRules.Any.Type, Samples.ConfigSample.AccessRules.Any.Type);
            Assert.Equal(config.AccessRules.Any.Action, Samples.ConfigSample.AccessRules.Any.Action);

            Assert.Equal(config.UserSettings.KeyId, Samples.ConfigSample.UserSettings.KeyId);
            Assert.Equal(config.UserSettings.PrivateKey, Samples.ConfigSample.UserSettings.PrivateKey);
            Assert.Equal(config.UserSettings.CfToken, Samples.ConfigSample.UserSettings.CfToken);
            Assert.Equal(config.UserSettings.CfAccount, Samples.ConfigSample.UserSettings.CfAccount);

            Assert.Equal(config.IsDarkmode, false);

            #endregion
        }

        #endregion
    }
}