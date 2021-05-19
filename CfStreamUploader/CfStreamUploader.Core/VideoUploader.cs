using CfStreamUploader.Core.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
        private Claims Claim { get; } = new Claims();
        public RSA Rsa { get; set; } = RSA.Create();
        public string VideoPath { get; set; } = String.Empty;

        #endregion

        #region public

        public string GetToken(Config config)
        {
            //Set Restrictions
            return this.VideoId;
        }

        public async Task<VideoUploadResult> UploadVideo(Config config)
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
                    var output = outputStreamReader.ReadToEnd();

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
            return string.Format(this.script, config.CfToken, this.VideoPath.Replace("\\", "/"), config.CfAccount);
        }

        #endregion

        #region private

        private int DecodePrivateKey(string privateKey, int expiresIn)
        {
            this.Claim.Expiry.AddYears(expiresIn);


            var decryptedBytes = this.Rsa.Decrypt(
                Convert.FromBase64String(privateKey),
                RSAEncryptionPadding.Pkcs1
            );

            var rsaPrivateKey = new X509Certificate(decryptedBytes);

            return 0;
        }

        #endregion
    }
}