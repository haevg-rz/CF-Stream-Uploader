using CfStreamUploader.Core.Models;
using CfStreamUploader.Core.Test.TestSamples;
using Xunit;

namespace CfStreamUploader.Core.Test.ModelTests
{
    public class UserSettingsTest
    {
        #region tests

        [Fact]
        public void DefaultConstructorUserSettingsTest()
        {
            #region Assign

            var userSettings = new UserSettings();

            #endregion

            #region Act

            #endregion

            #region Assert

            Assert.Equal(userSettings.KeyId, Samples.DefaultConfigSample.UserSettings.KeyId);
            Assert.Equal(userSettings.PrivateKey, Samples.DefaultConfigSample.UserSettings.PrivateKey);
            Assert.Equal(userSettings.CfAccount, Samples.DefaultConfigSample.UserSettings.CfAccount);
            Assert.Equal(userSettings.CfToken, Samples.DefaultConfigSample.UserSettings.CfToken);

            #endregion
        }

        [Fact]
        public void ParameterizedConstructorUserSetingsTest()
        {
            #region Assign

            var userSettings = new UserSettings(Samples.ConfigSample.UserSettings.CfToken,
                Samples.ConfigSample.UserSettings.CfAccount, Samples.ConfigSample.UserSettings.KeyId,
                Samples.ConfigSample.UserSettings.PrivateKey);

            #endregion

            #region Act

            #endregion

            #region Assert

            Assert.Equal(userSettings.KeyId, Samples.ConfigSample.UserSettings.KeyId);
            Assert.Equal(userSettings.PrivateKey, Samples.ConfigSample.UserSettings.PrivateKey);
            Assert.Equal(userSettings.CfAccount, Samples.ConfigSample.UserSettings.CfAccount);
            Assert.Equal(userSettings.CfToken, Samples.ConfigSample.UserSettings.CfToken);

            #endregion
        }

        #endregion
    }
}