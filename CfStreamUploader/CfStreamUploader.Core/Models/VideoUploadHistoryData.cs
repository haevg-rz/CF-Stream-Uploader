using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfStreamUploader.Core.Models
{
    public class VideoUploadHistoryData
    {
        public string VideoTitle { get; set; }
        public DateTime UploadDate { get; set; }
        public string VideoUrl { get; set; }
        public Object SetAccesRules { get; set; }
        public string VideoToken { get; set; }
        public string HtmlCode { get; set; }

        public VideoUploadHistoryData(string videoTitle, DateTime uploadDate, string videoUrl, object setAccessRules, string videoToken, string htmlCode)
        {
            this.VideoTitle = videoTitle;
            this.UploadDate = uploadDate;
            this.VideoUrl = videoUrl;
            this.SetAccesRules = setAccessRules;
            this.VideoToken = videoToken;
            this.HtmlCode = htmlCode;
        }
    }
}
