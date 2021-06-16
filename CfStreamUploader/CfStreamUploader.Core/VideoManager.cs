using CfStreamUploader.Core.Models;
using Jose;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
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

        public string SetRestrictions(Config config, string videoId, object setAccessRules,
            bool checkboxRestrictionExpireIn)
        {
            var header = new Dictionary<string, object>()
            {
                {"kid", config.UserSettings.KeyId},
                {"typ", "JWT"}
            };
            var payload = new Dictionary<string, object>()
            {
                {"sub", videoId},
                {"kid", config.UserSettings.KeyId},
                {"exp", this.GetExpireDate(checkboxRestrictionExpireIn, config)},
                {"accessRules", setAccessRules}
            };

            var bytesToDecrypt = Convert.FromBase64String(config.UserSettings.PrivateKey);
            var str = Encoding.UTF8.GetString(bytesToDecrypt);
            var rsa = RSA.Create();
            rsa.ImportFromPem(str.ToCharArray());

            return JWT.Encode(payload, rsa, JwsAlgorithm.RS256, header);
        }


        public async Task<(VideoUploadResult videoUploadResult, string VideoUrl)> UploadVideoAsync(Config config)
        {
            var file = new FileInfo(this.VideoPath);

            if (file.Length <= 200 * 1024 * 1024) // 200mb
            {
                var cmdVideoUploadScript = this.GetCmdVideoUploadScript(config);
                var videoUploadResult = await this.RunCmdAsync(cmdVideoUploadScript);

                if (!videoUploadResult.videoUploadResult.Success)
                    return (new VideoUploadResult(false, new Exception("Please check your Settings")), string.Empty);

                var json = JsonConvert.DeserializeObject<HttpResponse>(videoUploadResult.cmdOutput);

                return (new VideoUploadResult(true, null), json.result.uid);
            }

            try
            {
                var tusClient = new TusClient.TusClient();
                var header = new Dictionary<string, string>
                {
                    {"name", file.Name},
                    {"thumbnailtimestamppct", "0.0"}
                };

                var fileUrl = await Task.Run(() => tusClient.Create(
                    $"https://api.cloudflare.com/client/v4/accounts/{config.UserSettings.CfAccount}/stream",
                    file, config.UserSettings.CfToken, header));

                await Task.Run(() => tusClient.Upload(fileUrl, file, config.UserSettings.CfToken));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.UserSettings.CfToken}");
                var videoUploadResult = await client.GetAsync(fileUrl);
                var json = JsonConvert.DeserializeObject<HttpResponse>(videoUploadResult.ToString());

                return (new VideoUploadResult(true, null), json.result.uid);
            }
            catch (Exception e)
            {
                return (new VideoUploadResult(true, e), string.Empty);
            }
        }

        public async Task<VideoUploadResult> SetSignedUrl(Config config, string videoId)
        {
            var cmdSignedUrlScript = this.GetSignedUrlScript(config, videoId);
            var signedUrlResult = await this.RunCmdAsync(cmdSignedUrlScript);

            if (!signedUrlResult.videoUploadResult.Success)
                return new VideoUploadResult(false, new Exception("Making a video require signed URLs failed"));

            return new VideoUploadResult(true, null);
        }

        public object GetAccesRules(Config config, bool checkboxRestrictionIP, bool checkboxRestrictionCountry)
        {
            var jsonStringList = new List<string>();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            if (checkboxRestrictionIP)
                jsonStringList.Add(
                    System.Text.Json.JsonSerializer.Serialize(config.AccessRules.Ip, serializeOptions));

            if (checkboxRestrictionCountry)
                jsonStringList.Add(
                    System.Text.Json.JsonSerializer.Serialize(config.AccessRules.Country, serializeOptions));

            jsonStringList.Add(System.Text.Json.JsonSerializer.Serialize(config.AccessRules.Any, serializeOptions));

            var accesruleJson = jsonStringList.Count switch
            {
                1 => new[] {JsonConvert.DeserializeObject(jsonStringList[0])},
                2 => new[]
                {
                    JsonConvert.DeserializeObject(jsonStringList[0]),
                    JsonConvert.DeserializeObject(jsonStringList[1])
                },
                3 => new[]
                {
                    JsonConvert.DeserializeObject(jsonStringList[0]),
                    JsonConvert.DeserializeObject(jsonStringList[1]),
                    JsonConvert.DeserializeObject(jsonStringList[2])
                },
            };

            return accesruleJson;
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
            return string.Format(this.videoUploadScript, config.UserSettings.CfToken,
                this.VideoPath.Replace("\\", "/"),
                config.UserSettings.CfAccount);
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
                    myProcess.StartInfo.RedirectStandardError = true;
                    myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    myProcess.StartInfo.Arguments = $"/c {cmdCommand}";

                    myProcess.Start();

                    var outputStreamReader = myProcess.StandardOutput;
                    var error = myProcess.StandardError;
                    var err = await error.ReadToEndAsync();
                    Trace.WriteLine(err);
                    output = await outputStreamReader.ReadToEndAsync();
                }
            }
            catch (Exception e)
            {
                return (string.Empty, new VideoUploadResult(false, e));
            }

            return (output, new VideoUploadResult(true, null));
        }

        private long GetExpireDate(bool checkboxRestrictionExpireIn, Config config)
        {
            var now = DateTime.Now;

            if (checkboxRestrictionExpireIn)
            {
                long seconds = config.AccessRules.ExpiresIn * 24 * 60 * 60; //ExpiresIn in seconds
                return ((DateTimeOffset) now).ToUnixTimeSeconds() + seconds;
            }
            else
            {
                long seconds = 356 * 10 * 24 * 60 * 60; //10 Years in seconds
                return ((DateTimeOffset) now).ToUnixTimeSeconds() + seconds;
            }
        }

        #endregion
    }
}