using System;
using System.Collections.Generic;
using System.Text;

namespace CfStreamUploader.Core.Models
{
    public class Config
    {
        public string CfToken { get; set; }

        public Config(string cfToken)
        {
            this.CfToken = cfToken;
        }
    }
}
