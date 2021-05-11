namespace CfStreamUploader.Core.Models
{
    public class Config
    {
        public string CfToken { get; set; }

        public Config(string cfToken)
        {
            this.CfToken = cfToken;
        }

        public Config()
        {
            this.CfToken = string.Empty;
        }
    }
}