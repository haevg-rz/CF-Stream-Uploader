using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CfStreamUploader.Core.Models
{
    public class HttpResponse
    {
        public Result result { get; set; }

    }
    public class Result
    {
        public string uid { get; set; }
    }
}
