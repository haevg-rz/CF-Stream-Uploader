using CfStreamUploader.Core.Models;

namespace CfStreamUploader.Presentation.Test.TestSamples
{
    public static class Samples
    {
        public static Config ConfigSample { get; } =
            new Config()
            {
                AccessRules = new AccessRules(),
                UserSettings = new UserSettings("TestCfToken", "TestCfAccount", "TestKeyId", "TestPrivateKey"),
                IsDarkmode = false
            };

        public static Config AccesRulesConfigSample { get; } =
            new Config()
            {
                AccessRules = new AccessRules()
                {
                    Any = {Action = "allow"},
                    Ip = {Action = "allow", Ips = {"TestIp"}},
                    Country = {Action = "allow", Countries = {"DE", "FR"}},
                    ExpiresIn = 10
                },
                UserSettings = new UserSettings(),
                IsDarkmode = false
            };
    }
}