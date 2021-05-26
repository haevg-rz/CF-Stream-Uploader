using System.Text.Json.Serialization;

namespace CfStreamUploader.Core.Models
{
    public class UserSettings
    {
        [JsonPropertyName("cfToken")] public string CfToken { get; set; }
        [JsonPropertyName("cfAccount")] public string CfAccount { get; set; }
        [JsonPropertyName("keyId")] public string KeyId { get; set; }
        [JsonPropertyName("privateKey")] public string PrivateKey { get; set; }

        public UserSettings()
        {
            this.CfToken = string.Empty;
            this.CfAccount = string.Empty;
            this.KeyId = string.Empty;
            this.PrivateKey = string.Empty;
        }

        public UserSettings(string cfToken, string cfAccount, string keyId, string privateKey)
        {
            this.CfToken = cfToken;
            this.CfAccount = cfAccount;
            this.KeyId = keyId;
            this.PrivateKey = privateKey;
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