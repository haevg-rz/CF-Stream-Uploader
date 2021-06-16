using System;
using System.IO;
using System.Reflection;

namespace CfStreamUploader.Core
{
    public class HtmlLayout
    {
        #region fields

        public string CfStreamUploaderPath =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\CfStreamUploader";

        private string htmlLayoutFile = "HtmlLayout.txt";

        #endregion

        #region puplic

        public string GetHtmlLayout()
        {
            if (File.Exists(Path.Combine(this.CfStreamUploaderPath, this.htmlLayoutFile)))
            {
                try
                {
                    var test = string.Format(this.ReadHtmlLayout(), "test");
                }
                catch (Exception e)
                {
                    var htmlLayout = this.GetDefaultHtmlLayout();
                    this.WriteDefaultHtmlLayout(htmlLayout);
                    return htmlLayout;
                }

                return this.ReadHtmlLayout();
            }

            var defaultHtmlLayout = this.GetDefaultHtmlLayout();
            this.WriteDefaultHtmlLayout(defaultHtmlLayout);
            return defaultHtmlLayout;
        }

        #endregion

        #region private

        private void WriteDefaultHtmlLayout(string defaultHtmlLayout)
        {
            if (!Directory.Exists(this.CfStreamUploaderPath))
                Directory.CreateDirectory(this.CfStreamUploaderPath);

            File.WriteAllText(Path.Combine(this.CfStreamUploaderPath, this.htmlLayoutFile), defaultHtmlLayout);
        }

        private string GetDefaultHtmlLayout()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("CfStreamUploader.Core.Resources.defaultHtmlLayout.txt");
            var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }

        private string ReadHtmlLayout()
        {
            return File.ReadAllText(Path.Combine(this.CfStreamUploaderPath, this.htmlLayoutFile));
        }

        #endregion
    }
}