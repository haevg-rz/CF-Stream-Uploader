using CfStreamUploader.Core.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CfStreamUploader.Core
{
    public class VideoUploader
    {
        #region fields

        private readonly string script = "curl -X POST -H \"Authorization: Bearer {0}\" -F file=@{1} https://api.cloudflare.com/client/v4/accounts/{2}/stream";

        #endregion

        #region props

        public string VideoId { get; set; }
        private Claims Claim { get; } = new Claims();
        public RSA Rsa { get; set; } = RSA.Create();
        public string VideoPath { get; set; }

        #endregion

        #region public

        public string GetToken(Config config)
        {
            return "TestTokenABC";
        }

        public async Task<VideoUploadResult> UploadVideo(Config config)
        {
            // var cmdCommand = this.GetCmdScript(config);
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.CfToken);
                // client.DefaultRequestHeaders.Add("content-type", this.VideoPath.Replace("\\", "/"));
                // client.DefaultRequestHeaders.Add("Content-Type", this.VideoPath);
                // client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", this.VideoPath.Replace("\\", "/"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.VideoPath));
                var url = new Uri(string.Format("https://api.cloudflare.com/client/v4/accounts/{2}/stream", config.CfAccount));

                var reponse = await client.PostAsync(url, null);


                 //---------------------

                // var request = new HttpRequestMessage
                // {
                //     RequestUri = url,
                //     Method = HttpMethod.Post
                // };
                // request.Content.Headers.ContentType = new MediaTypeHeaderValue(this.VideoPath.Replace("\\", "/"));


                var aa = "hello";


                // using (var myProcess = new Process())
                // {
                //     myProcess.StartInfo.FileName = "cmd";
                //     myProcess.StartInfo.CreateNoWindow = true;
                //     myProcess.StartInfo.UseShellExecute = false;
                //     myProcess.StartInfo.RedirectStandardOutput = true;
                //     myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //     myProcess.StartInfo.Arguments = $"/c {cmdCommand}";
                //
                //     myProcess.Start();
                //
                //     var outputStreamReader = myProcess.StandardOutput;
                //     var output = outputStreamReader.ReadToEnd();
                //     var b = "hello";
                // }
            }
            catch (Exception e)
            {
                return new VideoUploadResult(false, e);
            }

            return new VideoUploadResult(true, null);
        }

        private string GetCmdScript(Config config)
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