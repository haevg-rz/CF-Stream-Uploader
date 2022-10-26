using System;
using System.IO;
using System.Threading.Tasks;

namespace CfStreamUploader.Core
{
    public class CfLogger
    {
        private string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CfStreamUploader", "Logger");

        public async Task WriteLogFileAsync(Exception e)
        {
            if (!File.Exists(folderpath))
                Directory.CreateDirectory(folderpath);

            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CfStreamUploader", "Logger", DateTime.Now.ToString("yyyyMMddHHmmssfff") + "log.txt");
            var stream = File.CreateText(filePath);
            await stream.WriteLineAsync(DateTime.Now.ToString("o")+ e.Message + e.StackTrace);
            stream.Close();
        }
    }
}
