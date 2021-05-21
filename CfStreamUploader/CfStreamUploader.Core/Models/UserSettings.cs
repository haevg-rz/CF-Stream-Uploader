namespace CfStreamUploader.Core.Models
{
    public class UserSettings
    {
        public string CfToken { get; set; }
        public string CfAccount { get; set; }
        public string KeyId { get; set; }
        public string PrivateKey { get; set; }

        public UserSettings()
        {
            this.CfToken = string.Empty;
            this.CfAccount = string.Empty;
            this.KeyId = string.Empty;
            this.PrivateKey = string.Empty;
        }

        public UserSettings(UserSettings userSettings)
        {
            this.CfToken = userSettings.CfToken;
            this.CfAccount = userSettings.CfAccount;
            this.KeyId = userSettings.KeyId;
            this.PrivateKey = userSettings.PrivateKey;
        }
    }
}