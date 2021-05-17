using System;

namespace CfStreamUploader.Core.Models
{
    public class VideoUploadResult
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }

        public VideoUploadResult(bool success, Exception exception)
        {
            this.Success = success;
            this.Exception = exception;
        }
    }
}