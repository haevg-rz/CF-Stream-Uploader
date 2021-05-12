using CfStreamUploader.Core.Models;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CfStreamUploader.Core
{
    public class VideoUploader
    {
        #region fields

        #endregion

        #region props

        private Claims Claim { get; } = new Claims();
        public RSA Rsa { get; set; } = RSA.Create();
        public string VideoPath { get; set; }

        #endregion

        #region public

        public string GetToken(Config config)
        {
            return "TestTokenABC";

            var result = this.DecodePrivateKey(config.PrivateKey, config.ExpiresIn);
            this.SignJwt();

            return string.Empty;
        }

        public void UploadVideo()
        {
            return;
        }

        #endregion

        #region private

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