using CfStreamUploader.Core.Test.TestSamples;
using Xunit;

namespace CfStreamUploader.Core.Test
{
    public class VideoUploaderTest
    {
        #region props

        public VideoManager VideoManager { get; } = new VideoManager();

        #endregion

        #region tests

        [Fact]
        public void GetCmdScriptsTest()
        {
            #region Assign

            const string expectedResult =
                "curl -X POST -H \"Authorization: Bearer TestCfToken\" -F file=@TestVideoPath https://api.cloudflare.com/client/v4/accounts/TestCfAccount/stream";
            this.VideoManager.VideoPath = "TestVideoPath";

            #endregion

            #region Act

            var result = this.VideoManager.GetCmdVideoUploadScript(Samples.ConfigSample);

            #endregion

            #region Assert

            Assert.Equal(expectedResult, result);

            #endregion
        }

        #endregion
    }
}