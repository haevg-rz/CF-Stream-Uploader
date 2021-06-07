using CfStreamUploader.Core.Models;
using Jose;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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

        public string SetRestrictions(Config config, string videoId, bool checkboxRestrictionIp,
            bool checkboxRestrictionCountry, bool checkboxRestrictionAny)
        {
            var datetimenow = DateTime.Now;
            long time = 10 * 365 * 24 * 60 * 60;

            var header = new Dictionary<string, object>()
            {
                {"kid", config.UserSettings.KeyId},
                {"typ", "JWT"}
            };
            var payload = new Dictionary<string, object>()
            {
                {"sub", videoId},
                {"kid", config.UserSettings.KeyId},
                {"exp", ((DateTimeOffset)datetimenow).ToUnixTimeSeconds() + time},
                {
                    "accessRules", new [] {JsonConvert.DeserializeObject(this.AccesRulesManager(config, checkboxRestrictionIp, checkboxRestrictionCountry,
                        checkboxRestrictionAny))}
                }
            };


            var bytesToDecrypt = Convert.FromBase64String(config.UserSettings.PrivateKey);
            var str = Encoding.UTF8.GetString(bytesToDecrypt);
            var rsa = RSA.Create();
            rsa.ImportFromPem(str.ToCharArray());

            var a = JWT.Encode(payload, rsa, JwsAlgorithm.RS256, header);
            return a;
        }


        public async Task<(VideoUploadResult videoUploadResult, string VideoUrl)> UploadVideoAsync(Config config)
        {
            return (new VideoUploadResult(true, null), "3aa02dd85c374958967f950b8e18e703");

            //Video Upload
            var cmdVideoUploadScript = this.GetCmdVideoUploadScript(config);
            var videoUploadResult = await this.RunCmdAsync(cmdVideoUploadScript);

            if (!videoUploadResult.videoUploadResult.Success)
                return (new VideoUploadResult(false, new Exception("Please check your Settings")), string.Empty);

            var json = JsonConvert.DeserializeObject<HttpResponse>(videoUploadResult.cmdOutput);
            var videoId = json.result.uid;


            //SetSignedURLs
            var cmdSignedUrlScript = this.GetSignedUrlScript(config, videoId);
            var signedUrlResult = await this.RunCmdAsync(cmdSignedUrlScript);

            if (!signedUrlResult.videoUploadResult.Success)
                return (new VideoUploadResult(false, new Exception("Making a video require signed URLs failed")),
                    videoId);

            return (new VideoUploadResult(true, null), videoId);
        }

        #endregion

        #region private

        internal string GetSignedUrlScript(Config config, string videoId)
        {
            return string.Format(this.signedUrlScript, config.UserSettings.CfToken, config.UserSettings.CfAccount,
                videoId, "{", "}");
        }

        internal string GetCmdVideoUploadScript(Config config)
        {
            return string.Format(this.videoUploadScript, config.UserSettings.CfToken, this.VideoPath.Replace("\\", "/"),
                config.UserSettings.CfAccount);
        }


        private string AccesRulesManager(Config config, bool checkboxRestrictionIP, bool checkboxRestrictionCountry,
            bool checkboxRestrictionAny)
        {
            var jsonStringList = new List<string>();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            if (checkboxRestrictionIP)
                jsonStringList.Add(System.Text.Json.JsonSerializer.Serialize(config.AccessRules.Ip, serializeOptions));

            if (checkboxRestrictionCountry)
                jsonStringList.Add(
                    System.Text.Json.JsonSerializer.Serialize(config.AccessRules.Country, serializeOptions));

            if (checkboxRestrictionAny)
                jsonStringList.Add(System.Text.Json.JsonSerializer.Serialize(config.AccessRules.Any, serializeOptions));

            if (!checkboxRestrictionIP && !checkboxRestrictionCountry && !checkboxRestrictionAny)
                jsonStringList.Add(System.Text.Json.JsonSerializer.Serialize(new Any(), serializeOptions));

            var accesruleJson = jsonStringList.Count switch
            {
                1 => jsonStringList[0],
                2 => JsonConvert.SerializeObject(new[]
                {
                    JsonConvert.DeserializeObject(jsonStringList[0]),
                    JsonConvert.DeserializeObject(jsonStringList[1])
                }),
                3 => JsonConvert.SerializeObject(new[]
                {
                    JsonConvert.DeserializeObject(jsonStringList[0]),
                    JsonConvert.DeserializeObject(jsonStringList[1]),
                    JsonConvert.DeserializeObject(jsonStringList[2])
                }),
                _ => string.Empty
            };

            return accesruleJson;
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