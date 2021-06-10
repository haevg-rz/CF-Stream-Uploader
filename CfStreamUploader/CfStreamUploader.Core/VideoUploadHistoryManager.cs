using CfStreamUploader.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfStreamUploader.Core
{
    public class VideoUploadHistoryManager
    {
        #region fields

        public string CfStreamUploaderHistoryPath =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\CfStreamUploader\VideoUploadHistory";

        #endregion

        #region public

        public void OpenVideoUploadHistory()
        {
            this.CreateVideoUploadHistory();

            var psi = new ProcessStartInfo
            {
                FileName = this.CfStreamUploaderHistoryPath,
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(psi);
        }

        public void WriteVideoUploadFile(string videotitle, string videoUrl, object setAccessRules, string videoTokenWithRestrictions, string htmlCode)
        {
            this.CreateVideoUploadHistory();

            var uploadDate = DateTime.Now;
            var videoUploadData = new VideoUploadHistoryData(videotitle,uploadDate,videoUrl,setAccessRules,videoTokenWithRestrictions,htmlCode);

            var filename = this.CreateFileName(videotitle,uploadDate);

            var jsonString = JsonConvert.SerializeObject(videoUploadData, Formatting.Indented);
            File.WriteAllText(Path.Combine(this.CfStreamUploaderHistoryPath, filename), jsonString);
        }

        public void CreateVideoUploadHistory()
        {
            if (!Directory.Exists(this.CfStreamUploaderHistoryPath))
                Directory.CreateDirectory(this.CfStreamUploaderHistoryPath);
        }

        #endregion

        #region private 

        private string CreateFileName(string videoTitle, DateTime uploadDate)
        {
            var uploadDateInFileFormat = uploadDate.ToString("MMM_dd_yyyy");

            var deleteFormat = videoTitle.Split('.');
            return $"{deleteFormat[0]}_VideoUploadAt_{uploadDateInFileFormat}.json";
        }

        #endregion
    }
}
