using CfStreamUploader.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("CfStreamUploader.Core.Test")]

namespace CfStreamUploader.Core
{
    public class VideoManager
    {
        #region fields

        private readonly string videoUploadScript =
            "curl -X POST -H \"Authorization: Bearer {0}\" -F file=@{1} https://api.cloudflare.com/client/v4/accounts/{2}/stream";

        private readonly string signedUrlScript =
            "curl -X POST -H \"Authorization: Bearer {0}\"  \"https://api.cloudflare.com/client/v4/accounts/{1}/stream/{2}\" -H \"Content-Type: application/json\" -d \"{3}\\\"uid\\\": \\\"{2}\\\", \\\"requireSignedURLs\\\": true {4}\"";

        #endregion

        #region props

        public string VideoPath { get; set; } = string.Empty;

        #endregion

        #region public

        public string SetRestrictions(Config config, string videoId)
        {
            var bytesToDecrypt = Convert.FromBase64String(config.UserSettings.PrivateKey);

            var str = Encoding.UTF8.GetString(bytesToDecrypt);

            var header = new Dictionary<string, object>()
            {
                {"alg", "RS256"},
                {"kid", config.UserSettings.KeyId}
            };
            var payload = new Dictionary<string, object>()
            {
                {"sub", videoId},
                {"kid", config.UserSettings.KeyId},
                {"exp", DateTime.Now.AddDays(10).ToString()}
                // + accessRules
            };
            // var handler = new JsonWebTokenHandler();

            var rsa = RSA.Create();
            rsa.ImportFromPem(str.ToCharArray());

            // var jwt = JWT.Encode(payload, rsa, JweAlgorithm.RSA_OAEP_256, JweEncryption.A256GCM, null, header);
            return videoId;
        }


        public async Task<(VideoUploadResult videoUploadResult, string VideoUrl)> UploadVideoAsync(Config config)
        {
            return (new VideoUploadResult(true, null), "3ef444818f6b481084841355d7af5f82");

            //Video Upload
            var cmdVideoUploadScript = this.GetCmdVideoUploadScript(config);
            var videoUploadResult = await this.RunCmdAsync(cmdVideoUploadScript);

            if (!videoUploadResult.videoUploadResult.Success)
                return (new VideoUploadResult(false, new Exception("Please check your Settings")), String.Empty);

            var json = JsonConvert.DeserializeObject<HttpResponse>(videoUploadResult.cmdOutput);
            var videoId = json.result.uid;
            

            //SetSignedURLs
            var cmdSignedUrlScript = this.GetSignedUrlScript(config, videoId);
            var signedUrlResult = await this.RunCmdAsync(cmdSignedUrlScript);

            if (!signedUrlResult.videoUploadResult.Success)
                return (new VideoUploadResult(false, new Exception("Making a video require signed URLs failed")), videoId);


            return (new VideoUploadResult(true, null), videoId);
        }


        #endregion

        #region private

        internal string GetCmdVideoUploadScript(Config config)
        {
            return string.Format(this.videoUploadScript, config.UserSettings.CfToken, this.VideoPath.Replace("\\", "/"),
                config.UserSettings.CfAccount);
        }

        internal string GetSignedUrlScript(Config config, string videoId)
        {
            return string.Format(this.signedUrlScript, config.UserSettings.CfToken, config.UserSettings.CfAccount,
                videoId, "{", "}");
        }

        private async Task<(string cmdOutput, VideoUploadResult videoUploadResult)> RunCmdAsync(string cmdCommand)
        {
            var output = string.Empty;
            try
            {
                using (var myProcess = new Process())
                {
                    myProcess.StartInfo.FileName = "cmd";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.RedirectStandardOutput = true;
                    myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    myProcess.StartInfo.Arguments = $"/c {cmdCommand}";

                    myProcess.Start();

                    var outputStreamReader = myProcess.StandardOutput;
                    output = await outputStreamReader.ReadToEndAsync();
                }
            }
            catch (Exception e)
            {
                return (string.Empty, new VideoUploadResult(false, e));
            }

            return (output, new VideoUploadResult(true, null));
        }

        #endregion
    }
}