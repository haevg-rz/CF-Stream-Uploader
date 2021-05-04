using System;
using System.Collections.Generic;
using System.Text;

namespace CfStreamUploader.Core.Models
{
    public class Config
    {
        public string Token { get; set; }

        public Config(string token)
        {
            this.Token = token;
        }
    }
}
