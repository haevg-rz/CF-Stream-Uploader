using System;

namespace CfStreamUploader.Core.Models
{
    public class Config
    {
        #region props

        public string CfToken { get; set; }
        public string CfAccount { get; set; }
        public string KeyId { get; set; }
        public string PrivateKey { get; set; }
        public int ExpiresIn { get; set; }
        public bool IsDarkmode { get; set; }

        #endregion

        #region constructor

        public Config()
        {
            this.CfToken = string.Empty;
            this.CfAccount = string.Empty;
            this.KeyId = string.Empty;
            this.PrivateKey = string.Empty;
            this.ExpiresIn = 0;
            this.IsDarkmode = false;
        }

        public Config(string cfToken, string cfAccount, string keyId, string privateKey, int expiresIn, bool isDarkmode)
        {
            this.CfToken = cfToken;
            this.CfAccount = cfAccount;
            this.KeyId = keyId;
            this.PrivateKey = privateKey;
            this.ExpiresIn = expiresIn;
            this.IsDarkmode = isDarkmode;
        }

        public Config(Config config)
        {
            this.CfToken = config.CfToken;
            this.CfAccount = config.CfAccount;
            this.KeyId = config.KeyId;
            this.PrivateKey = config.PrivateKey;
            this.ExpiresIn = config.ExpiresIn;
            this.IsDarkmode = config.IsDarkmode;
        }

        #endregion
    }
}