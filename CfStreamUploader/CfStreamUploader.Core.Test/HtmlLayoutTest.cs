using CfStreamUploader.Core.Test.TestSamples;
using System;
using System.IO;
using Xunit;

namespace CfStreamUploader.Core.Test
{
    public class HtmlLayoutTest
    {
        private HtmlLayout HtmlLayout { get; } = new HtmlLayout();

        private readonly string solutionDir =
            Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory));

        private string htmlLayoutFile = "HtmlLayout.txt";

        [Fact]
        public void HtmlLayoutDoesNotExistTest()
        {
            #region Assign

            this.SetUp();

            #endregion

            #region Act

            var result = this.HtmlLayout.GetHtmlLayout();

            #endregion

            #region Assert

            Equals(result, Samples.defaultHtmlLayoutSample);
            this.TearDown();

            #endregion
        }

        [Fact]
        public void HtmlLayoutExistsTest()
        {
            #region Assign

            this.SetUp();
            File.WriteAllText(Path.Combine(this.solutionDir, this.htmlLayoutFile), Samples.htmlLayoutSample);

            #endregion

            #region Act

            var result = this.HtmlLayout.GetHtmlLayout();

            #endregion

            #region Assert

            Equals(result, Samples.htmlLayoutSample);
            this.TearDown();

            #endregion
        }

        private void SetUp()
        {
            this.HtmlLayout.CfStreamUploaderPath = this.solutionDir;
            this.DeletePath();
        }

        private void TearDown()
        {
            this.DeletePath();
        }

        private void DeletePath()
        {
            if (File.Exists(Path.Combine(this.solutionDir, this.htmlLayoutFile)))
                File.Delete(Path.Combine(this.solutionDir, this.htmlLayoutFile));
        }
    }
}