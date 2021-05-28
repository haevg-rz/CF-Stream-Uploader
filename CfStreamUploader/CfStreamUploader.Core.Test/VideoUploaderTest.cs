using CfStreamUploader.Core.Test.TestSamples;
using Xunit;

namespace CfStreamUploader.Core.Test
{
    public class VideoUploaderTest
    {
        #region props

        public VideoUploader VideoUploader { get; } = new VideoUploader();

        #endregion

        #region tests

        [Fact]
        public void GetCmdScriptsTest()
        {
            #region Assign

            const string expectedResult =
                "curl -X POST -H \"Authorization: Bearer TestCfToken\" -F file=@TestVideoPath https://api.cloudflare.com/client/v4/accounts/TestCfAccount/stream";
            this.VideoUploader.VideoPath = "TestVideoPath";

            #endregion

            #region Act

            var result = this.VideoUploader.GetCmdScript(Samples.ConfigSample);

            #endregion

            #region Assert

            Equals(expectedResult, result);

            #endregion
        }

        #endregion
    }
}