using System.Text.Json.Serialization;

namespace CfStreamUploader.Core.Models
{
    public class Config
    {
        #region props

        [JsonPropertyName("userSettings")] public UserSettings UserSettings { get; set; }
        [JsonPropertyName("accessRules")] public AccessRules AccessRules { get; set; }
        [JsonPropertyName("isDarkmode")] public bool IsDarkmode { get; set; }

        #endregion

        #region constructor

        public Config()
        {
            this.UserSettings = new UserSettings();
            this.AccessRules = new AccessRules();
            this.IsDarkmode = false;
        }

        public Config(UserSettings userSettings, AccessRules accessRules, bool isDarkmode)
        {
            this.UserSettings = userSettings;
            this.AccessRules = accessRules;
            this.IsDarkmode = isDarkmode;
        }

        public Config(Config config)
        {
            this.UserSettings = config.UserSettings;
            this.AccessRules = config.AccessRules;
            this.IsDarkmode = config.IsDarkmode;
        }

        #endregion
    }
}