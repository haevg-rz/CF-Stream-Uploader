﻿namespace CfStreamUploader.Core
{
    public class Core
    {
        public ConfigManager ConfigManager { get; } = new ConfigManager();
        public HtmlLayout HtmlLayout { get; } = new HtmlLayout();
        public VideoManager VideoManager { get; } = new VideoManager();
        public UploadHistoryManager UploadHistoryManager { get; } = new UploadHistoryManager();
        public CfLogger Logger { get;} = new CfLogger();
    }
}