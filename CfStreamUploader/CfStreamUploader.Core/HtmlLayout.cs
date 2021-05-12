using System;
using System.IO;
using System.Reflection;

namespace CfStreamUploader.Core
{
    public class HtmlLayout
    {
        public string CfStreamUploaderPath =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\CfStreamUploader";

        private string htmlLayoutFile = "HtmlLayout.txt";

        public string GetHtmlLayout()
        {
            if (File.Exists(Path.Combine(this.CfStreamUploaderPath, this.htmlLayoutFile))) return this.ReadHtmlLayout();

            var defaultHtmlLayout = this.GetDefaultHtmlLayout();
            this.WriteDefaultHtmlLayout(defaultHtmlLayout);
            return defaultHtmlLayout;
        }

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
    }
}