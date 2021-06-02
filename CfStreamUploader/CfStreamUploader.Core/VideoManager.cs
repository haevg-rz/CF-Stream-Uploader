using CfStreamUploader.Core.Models;
using Jose;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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
            "curl -X POST -H \"Authorization: Bearer {0}\"  \"https://api.cloudflare.com/client/v4/accounts/{1}/stream/{2}\" -H \"Content-Type: application/json\" -d \"{3}\\\"uid\\\": \\{2}\\\", \\\"requireSignedURLs\\\": true {4}\"";

        #endregion

        #region props

        public string VideoId { get; set; } = "d4aad179d1784a5ba0b7572b4890ae9a";  //TODO: Delete Prop
        public string VideoPath { get; set; } = string.Empty;
        private JwtSecurityTokenHandler Handler { get; set; } = new JwtSecurityTokenHandler();

        #endregion

        #region public

        public string SetRestrictions(Config config,bool checkboxRestrictionIP, bool checkboxRestrictionCountry, bool checkboxRestrictionAny)
        {
            var bytesToDecrypt = Convert.FromBase64String(config.UserSettings.PrivateKey);

            AsymmetricCipherKeyPair keyPair;
            var str = Encoding.UTF8.GetString(bytesToDecrypt);
            using (TextReader sr = new StringReader(str))
            {
                keyPair = (AsymmetricCipherKeyPair)new PemReader(sr).ReadObject();
            }

            var header = new Dictionary<string, object>()
            {
                {"kid", config.UserSettings.KeyId }
            };
            var payload = new Dictionary<string, object>()
            {
                {"sub", this.VideoId },
                {"kid", config.UserSettings.KeyId },
                {"exp", DateTime.Now.AddDays(10).ToString() },
                {"accessRules",  this.AccesRulesManager(config, checkboxRestrictionIP, checkboxRestrictionCountry, checkboxRestrictionAny)}
            };
            var handler = new JsonWebTokenHandler();

            var rsa = RSA.Create();
            rsa.ImportFromPem(str.ToCharArray());

            var jwt = JWT.Encode(payload, rsa, JwsAlgorithm.RS256, header);
            return jwt;
        }


        public async Task<VideoUploadResult> UploadVideoAsync(Config config)
        {
            //Video Upload  TODO: Create Method
            // var cmdVideoUploadScript = this.GetCmdVideoUploadScript(config);
            // var videoUploadResult = await this.RunCmdAsync(cmdVideoUploadScript);
            //
            // if (!videoUploadResult.videoUploadResult.Success)
            //     return new VideoUploadResult(false, new Exception("Please check your Settings"));
            //
            // var json = JsonConvert.DeserializeObject<HttpResponse>(videoUploadResult.cmdOutput);
            // this.VideoId = json.result.uid;

            //SetRestrictions TODO: Create Method
            var cmdSignedUrlScript = this.GetSignedUrlScript(config);
            var signedUrlResult = await this.RunCmdAsync(cmdSignedUrlScript);
            if (!signedUrlResult.videoUploadResult.Success)
                return new VideoUploadResult(false, new Exception("Making a video require signed URLs failed"));
            
            return new VideoUploadResult(true, null);
        }

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

        private async Task<(string cmdOutput, VideoUploadResult videoUploadResult)> RunCmdAsync(string cmdCommand)
        {
            var output = string.Empty;
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