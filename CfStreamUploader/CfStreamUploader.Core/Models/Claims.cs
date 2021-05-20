using System;

namespace CfStreamUploader.Core.Models
{
    public class Claims
    {
        #region props

        public string KeyID { get; set; }
        public string VideoID { get; set; }
        public DateTime Expiry { get; set; }
        public DateTime NotBefore { get; set; }
        public AccessRules[] AccessRuleses { get; set; }

        #endregion

        #region constructor

        public Claims()
        {
            this.KeyID = string.Empty;
            this.VideoID = string.Empty;
            this.Expiry = new DateTime();
            this.NotBefore = new DateTime();
            this.AccessRuleses = null;
        }

        public Claims(string keyID, string videoId, DateTime expiry, DateTime notBefore, AccessRules[] accessRuleses)
        {
            this.KeyID = keyID;
            this.VideoID = videoId;
            this.Expiry = expiry;
            this.NotBefore = notBefore;
            this.AccessRuleses = accessRuleses;
        }

        #endregion
    }
}