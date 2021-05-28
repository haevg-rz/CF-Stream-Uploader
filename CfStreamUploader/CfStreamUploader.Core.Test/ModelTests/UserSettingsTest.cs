using CfStreamUploader.Core.Models;
using CfStreamUploader.Core.Test.TestSamples;
using Xunit;

namespace CfStreamUploader.Core.Test.ModelTests
{
    public class UserSettingsTest
    {
        [Fact]
        public void DefaultConstructorUserSettingsTest()
        {
            #region Assign

            var userSetting = new UserSettings();

            #endregion

            #region Act

            #endregion

            #region Assert

            Equals(userSetting, Samples.ConfigSample.UserSettings);

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

            Equals(userSettings, Samples.ConfigSample.UserSettings);

            #endregion
        }
    }
}