using CfStreamUploader.Core.Models;

namespace CfStreamUploader.Core.Test.TestSamples
{
    public class Samples
    {
        public readonly string defaultHtmlLayoutSample =
            "<div style=\"position: relative; padding-top: 56.25%;\">\r\n        <iframe src=\"https://iframe.videodelivery.net/{0}?preload=true\"\r\n                style=\"border: none;\r\n                position: absolute;\r\n                top: 0;\r\n                height: 100%;\r\n                width: 100%;\"\r\n                allow=\"accelerometer;\r\n                gyroscope;\r\n                autoplay;\r\n                encrypted-media;\r\n                picture-in-picture;\"\r\n                allowfullscreen=\"true\">\r\n        </iframe>\r\n</div>";

        public readonly string htmlLayoutSample = "TestSample123";

        public Config ConfigSample { get; } =
            new Config()
            { 
                AccessRules = new AccessRules(),
                UserSettings = new UserSettings("TestCfToken", "TestCfAccount", "TestKeyId", "TestPrivateKey"),
                IsDarkmode = false
            };

        public Config DefaultConfigSample { get; } = new Config();
    }
}