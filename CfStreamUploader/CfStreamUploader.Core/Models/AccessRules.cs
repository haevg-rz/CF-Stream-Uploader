namespace CfStreamUploader.Core.Models
{
    public class AccessRules
    {
        #region props

        public string Action { get; set; }
        public string Type { get; set; }
        public string[] IPs { get; set; }

        #endregion
    }
}