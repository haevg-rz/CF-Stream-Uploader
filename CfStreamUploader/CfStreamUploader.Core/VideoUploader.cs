using CfStreamUploader.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TusDotNetClient;

namespace CfStreamUploader.Core
{
    public class VideoUploader
    {
        #region fields

        private readonly string getRequestCfUrl = "https://api.cloudflare.com/client/v4/accounts/{0}/stream?search={1}";
        private readonly string postCfUrl = "https://api.cloudflare.com/client/v4/accounts/{0}/stream";

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
            var videoId = this.GetVideoId();
            return "TestTokenABC";

            var result = this.DecodePrivateKey(config.PrivateKey, config.ExpiresIn);
            this.SignJwt();

            return string.Empty;
        }

        public async Task<VideoUploadResult> UploadVideo(Config config)
        {
            var postUrl = string.Format(this.postCfUrl, config.CfAccount);

            try
            {
                var tusClient = new TusClient();
                var fileInfo = new FileInfo(this.VideoPath);

                var metadata = new (string key, string value)[1];
                metadata[0].key = "Authorization";
                metadata[0].value = config.CfToken;

                var fileUrl = await tusClient.CreateAsync(postUrl, fileInfo.Length,metadata);
                var response = tusClient.UploadAsync(fileUrl, fileInfo);
            }
            catch (Exception e)
            {
                return new VideoUploadResult(false, e);
            }

            return new VideoUploadResult(true, null);
        }

        #endregion

        #region private

        private string GetVideoId()
        {
            return null;
            // try
            // {
            //     using (var client = new WebClient())
            //     {
            //         client.Headers[HttpRequestHeader.ContentType] = config.CfToken;
            //         var postResult = client.UploadFile(cfHeader, "Get", this.VideoPath);
            //     }
            //
            // }
            // catch (Exception e)
            // {
            //     
            // }
            // return null;
        }

        private void PrepareToSign(string privateKey)
        {
            return;
        }

        private void SignJwt()
        {
            return;
        }

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