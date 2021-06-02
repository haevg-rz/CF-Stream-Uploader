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

        public string SetRestrictions(Config config,string videoId, bool checkboxRestrictionIP, bool checkboxRestrictionCountry, bool checkboxRestrictionAny)
        {
            var bytesToDecrypt = Convert.FromBase64String(config.UserSettings.PrivateKey);

            var str = Encoding.UTF8.GetString(bytesToDecrypt);

            var header = new Dictionary<string, object>()
            {
                {"kid", config.UserSettings.KeyId }
            };
            var payload = new Dictionary<string, object>()
            {
                {"sub", videoId },
                {"kid", config.UserSettings.KeyId },
                {"exp", DateTime.Now.AddDays(10).ToString() },
                {"accessRules",  this.AccesRulesManager(config, checkboxRestrictionIP, checkboxRestrictionCountry, checkboxRestrictionAny)}
            };

            var rsa = RSA.Create();
            rsa.ImportFromPem(str.ToCharArray());
          
            return JWT.Encode(payload, rsa, JwsAlgorithm.RS256, header);
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

        #endregion

        #region private

        internal string GetSignedUrlScript(Config config)
        {
            return string.Format(this.signedUrlScript, config.UserSettings.CfToken, config.UserSettings.CfAccount,
                this.VideoId, "{", "}");
        }
          
        internal string GetCmdVideoUploadScript(Config config)
        {
            return string.Format(this.videoUploadScript, config.UserSettings.CfToken, this.VideoPath.Replace("\\", "/"),
                config.UserSettings.CfAccount);
        }


        private string AccesRulesManager(Config config, bool checkboxRestrictionIP, bool checkboxRestrictionCountry, bool checkboxRestrictionAny)
        {
            var jsonStringList = new List<string>();

            if (checkboxRestrictionIP)
                jsonStringList.Add(JsonConvert.SerializeObject(config.AccessRules.Ip, Formatting.Indented));

            if (checkboxRestrictionCountry)
                jsonStringList.Add(JsonConvert.SerializeObject(config.AccessRules.Country, Formatting.Indented));

            if (checkboxRestrictionAny)
                jsonStringList.Add(JsonConvert.SerializeObject(config.AccessRules.Any, Formatting.Indented));

            if (!checkboxRestrictionIP && !checkboxRestrictionCountry && !checkboxRestrictionAny)
                jsonStringList.Add(JsonConvert.SerializeObject(new Any()));

            var accesruleJson = String.Empty;

            switch (jsonStringList.Count)
            {
                case 1:
                    accesruleJson = jsonStringList[0];
                    break;
                case 2:
                    accesruleJson = JsonConvert.SerializeObject(new[] { JsonConvert.DeserializeObject(jsonStringList[0]), JsonConvert.DeserializeObject(jsonStringList[1]) });
                    break;
                case 3:
                    accesruleJson = JsonConvert.SerializeObject(new[] { JsonConvert.DeserializeObject(jsonStringList[0]), JsonConvert.DeserializeObject(jsonStringList[1]), JsonConvert.DeserializeObject(jsonStringList[2]) });
                    break;
            }

            return accesruleJson;

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