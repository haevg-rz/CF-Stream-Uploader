namespace CfStreamUploader.Core.Models
{
    public class Config
    {
        #region props

        public string CfToken { get; set; }
        public string VideoId { get; set; }
        public string KeyId { get; set; }
        public string PrivateKey { get; set; }
        public int ExpiresIn { get; set; }

        #endregion

        #region constructor

        public Config()
        {
            this.CfToken = string.Empty;
            this.VideoId = string.Empty;
            this.KeyId = string.Empty;
            this.PrivateKey = string.Empty;
            this.ExpiresIn = 0;
        }

        public Config(string cfToken, string videoId, string keyId, string privateKey, int expiresIn)
        {
            this.CfToken = cfToken;
            this.VideoId = videoId;
            this.KeyId = keyId;
            this.PrivateKey = privateKey;
            this.ExpiresIn = expiresIn;
        }

        #endregion
    }
}