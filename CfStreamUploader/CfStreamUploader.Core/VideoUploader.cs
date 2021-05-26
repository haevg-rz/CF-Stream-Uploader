using CfStreamUploader.Core.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("CfStreamUploader.Core.Test")]

namespace CfStreamUploader.Core
{
    public class VideoUploader
    {
        #region fields

        private readonly string script =
            "curl -X POST -H \"Authorization: Bearer {0}\" -F file=@{1} https://api.cloudflare.com/client/v4/accounts/{2}/stream";

        #endregion

        #region props

        public string VideoId { get; set; }
        public RSA Rsa { get; set; } = RSA.Create();
        public string VideoPath { get; set; } = string.Empty;

        #endregion

        #region public

        public string GetToken(Config config)
        {
            //Set Restrictions
            return this.VideoId;
        }

        public async Task<VideoUploadResult> UploadVideoAsync(Config config)
        {
            var cmdCommand = this.GetCmdScript(config);
            try
            {
                using (var myProcess = new Process())
                {
                    myProcess.StartInfo.FileName = "cmd";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.RedirectStandardOutput = true;
                    myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    myProcess.StartInfo.Arguments = $"/c {cmdCommand}";

                    myProcess.Start();

                    var outputStreamReader = myProcess.StandardOutput;
                    var output = await outputStreamReader.ReadToEndAsync();

                    if (output == null)
                        return new VideoUploadResult(false, new Exception("Please check your Settings"));

                    var json = JsonConvert.DeserializeObject<HttpResponse>(output);
                    this.VideoId = json.result.uid;
                }
            }
            catch (Exception e)
            {
                return new VideoUploadResult(false, e);
            }

            return new VideoUploadResult(true, null);
        }


        internal string GetCmdScript(Config config)
        {
            return string.Format(this.script, config.UserSettings.CfToken, this.VideoPath.Replace("\\", "/"), config.UserSettings.CfAccount);
        }

        #endregion

        #region private


        #endregion
    }
}