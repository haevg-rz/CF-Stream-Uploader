﻿using CfStreamUploader.Presentation.Resources.Colors;
using CfStreamUploader.Presentation.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using CfStreamUploader.Core.Models;

[assembly: InternalsVisibleTo("CfStreamUploader.Presentation.Test")]

namespace CfStreamUploader.Presentation.ViewModels
{
    public class MainViewModel : ViewModelBase, IDropTarget
    {
        #region Fields

        private string htmlOutput = "HTML";
        private string videoTitel = "No video found";
        private string videoUrl = string.Empty;
        private readonly string defaultUri = "https://iframe.videodelivery.net/{0}?preload=true";
        private string dragAndDropInfo = "Drop video here";
        private string restrictionIP = string.Empty;
        private string restrictionCountry = string.Empty;
        private string restrictionAny = string.Empty;

        private string CfStreamUploaderPath =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\CfStreamUploader\Config.json";

        #endregion

        #region props

        public Core.Core Core { get; set; } = new Core.Core();

        public string HtmlOutput
        {
            get => this.htmlOutput;
            set => this.Set(ref this.htmlOutput, value);
        }

        public string VideoTitel
        {
            get => this.videoTitel;
            set => this.Set(ref this.videoTitel, value);
        }

        public string DragAndDropInfo
        {
            get => this.dragAndDropInfo;
            set => this.Set(ref this.dragAndDropInfo, value);
        }

        public string RestrictionIP
        {
            get => this.restrictionIP;
            set => this.Set(ref this.restrictionIP, value);
        }

        public string RestrictionCountry
        {
            get => this.restrictionCountry;
            set => this.Set(ref this.restrictionCountry, value);
        }

        public string RestrictionAny
        {
            get => this.restrictionAny;
            set => this.Set(ref this.restrictionAny, value);
        }

        #endregion

        #region RelayCommands

        public RelayCommand CopyToClipbordCommad { get; set; }
        public RelayCommand UploadViedeoCommand { get; set; }
        public RelayCommand SelectVideoCommand { get; set; }
        public RelayCommand CopyVideoUrlCommand { get; set; }
        public RelayCommand EditRestrictionsCommand { get; set; }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            this.SetDarkmodeCommand = new RelayCommand(this.SetDarkmode);
            this.UploadViedeoCommand = new RelayCommand(this.UploadVideoAsync);
            this.CopyToClipbordCommad = new RelayCommand(this.CopyToClipbord);
            this.SelectVideoCommand = new RelayCommand(this.SelectVideo);
            this.CopyVideoUrlCommand = new RelayCommand(this.CopyVideoUrl);
            this.EditRestrictionsCommand = new RelayCommand(this.EditRestrictions);

            this.SetRestrictions(this.Core.ConfigManager.Config);

            this.isDarkmode = this.Core.ConfigManager.Config.IsDarkmode;
            if (this.isDarkmode)
                this.Darkmode();
            else
                this.Lightmode();
        }

        #endregion

        #region public

        public void DragOver(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject) dropInfo.Data).GetFileDropList().Cast<string>();
            dropInfo.Effects = dragFileList.Any(item =>
            {
                var extension = Path.GetExtension(item);
                return extension != null && extension.Equals(".mp4");
            })
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject) dropInfo.Data).GetFileDropList().Cast<string>();
            dropInfo.Effects = dragFileList.Any(item =>
            {
                var extension = Path.GetExtension(item);
                return extension != null && extension.Equals(".mp4");
            })
                ? DragDropEffects.Copy
                : DragDropEffects.None;

            this.Core.VideoUploader.VideoPath = ((DataObject) dropInfo.Data).GetFileDropList().Cast<string>().First();
            this.VideoTitel = this.Core.VideoUploader.VideoPath.Split("\\").Last();
        }

        #endregion

        #region private

        private async void UploadVideoAsync()
        {
            if (this.Core.VideoUploader.VideoPath == string.Empty)
            {
                MessageBox.Show("Please select a Video.", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            if (!this.IsConfigSolid(this.Core.ConfigManager.Config)) return;

            var result = await this.Core.VideoUploader.UploadVideoAsync(this.Core.ConfigManager.Config);

            if (result.Success)
            {
                var videoToken = this.Core.VideoUploader.GetToken(this.Core.ConfigManager.Config);

                this.HtmlOutput = string.Format(this.Core.HtmlLayout.GetHtmlLayout(), videoToken);
                this.videoUrl = string.Format(this.defaultUri, videoToken);
            }
            else
            {
                MessageBox.Show(result.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal bool IsConfigSolid(Config config)
        {
            if (config.UserSettings.CfToken != string.Empty &&
                config.UserSettings.CfAccount != string.Empty) return true;

            var openConfig = MessageBox.Show(
                "There are missing attribute in the config.\nYou can open your config here",
                "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (openConfig != MessageBoxResult.Yes) return false;

            this.Core.ConfigManager.OpenConfig();
            return false;
        }

        private void CopyToClipbord()
        {
            Clipboard.SetText(this.HtmlOutput);
        }

        private void CopyVideoUrl()
        {
            Clipboard.SetText(this.videoUrl);
        }

        private void SelectVideo()
        {
            var fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() != true) return;

            if (fileDialog.FileName.Split(".").Last() == "mp4")
            {
                this.Core.VideoUploader.VideoPath = fileDialog.FileName;
                this.VideoTitel = this.Core.VideoUploader.VideoPath.Split("\\").Last();
            }
            else
            {
                MessageBox.Show("You selected a file with a not supported format", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void UpdateConfig()
        {
            this.Core.ConfigManager.Config.IsDarkmode = this.isDarkmode;

            var config = this.Core.ConfigManager.Config;
            config.IsDarkmode = this.isDarkmode;
            this.Core.ConfigManager.UpdateConfig(config);
        }

        private void EditRestrictions()
        {
            WindowManager.OpenEditRestrictionWindow();

            this.Core.ConfigManager.ReadConfig();

            this.SetRestrictions(Core.ConfigManager.Config);
        }

        internal void SetRestrictions(Config config)
        {
            this.RestrictionCountry = config.AccessRules.Country.PrintRestriction();
            this.RestrictionAny = config.AccessRules.Any.PrintRestriction();
            this.RestrictionIP = config.AccessRules.Ip.PrintRestriction();
        }

        #endregion

        #region ColorChange

        internal bool isDarkmode = false;
        private string themeText = "Lightmode";

        private string baseColor = "Transparent";
        private string contrastColor = "Transparent";
        private string textColor = "Black";
        private string borderBrush = "#6497e8";
        private string button1Bg = "#6497e8";
        private string button1Fg = "White";
        private string button2Bg = "#6497e8";
        private string button2Fg = "#6497e8";
        private string button2FgMouseOver = "White";
        private string progressColor = "Green";

        public string ThemeText
        {
            get => this.themeText;
            set => this.Set(ref this.themeText, value);
        }

        public string BaseColor
        {
            get => this.baseColor;
            set => this.Set(ref this.baseColor, value);
        }

        public string ContrastColor
        {
            get => this.contrastColor;
            set => this.Set(ref this.contrastColor, value);
        }

        public string TextColor
        {
            get => this.textColor;
            set => this.Set(ref this.textColor, value);
        }

        public string BorderBrush
        {
            get => this.borderBrush;
            set => this.Set(ref this.borderBrush, value);
        }

        public string Button1Bg
        {
            get => this.button1Bg;
            set => this.Set(ref this.button1Bg, value);
        }

        public string Button1Fg
        {
            get => this.button1Fg;
            set => this.Set(ref this.button1Fg, value);
        }

        public string Button2Bg
        {
            get => this.button2Bg;
            set => this.Set(ref this.button2Bg, value);
        }

        public string Button2Fg
        {
            get => this.button2Fg;
            set => this.Set(ref this.button2Fg, value);
        }

        public string Button2FgMouseOver
        {
            get => this.button2FgMouseOver;
            set => this.Set(ref this.button2FgMouseOver, value);
        }

        public string ProgressColor
        {
            get => this.progressColor;
            set => this.Set(ref this.progressColor, value);
        }

        public RelayCommand SetDarkmodeCommand { get; }

        private void SetDarkmode()
        {
            if (this.isDarkmode)
            {
                this.Lightmode();
                this.ThemeText = "Lightmode";

                this.isDarkmode = false;
                this.UpdateConfig();
            }
            else
            {
                this.Darkmode();
                this.ThemeText = "Darkmode";

                this.isDarkmode = true;
                this.UpdateConfig();
            }
        }

        private void Darkmode()
        {
            this.BaseColor = Colors.DarkmodeBaseColor;
            this.ContrastColor = Colors.DarkmodeContrastColor;
            this.TextColor = "White";
            this.BorderBrush = "White";
            this.Button1Bg = Colors.DarkmodeContrastColor;
            this.Button1Fg = "White";
            this.Button2Bg = "White";
            this.Button2Fg = "White";
            this.Button2FgMouseOver = Colors.DarkmodeContrastColor;
            this.ProgressColor = "LawnGreen";
        }

        private void Lightmode()
        {
            this.BaseColor = Colors.LightmodeBaseColor;
            this.ContrastColor = "Transparent";
            this.TextColor = "Black";
            this.BorderBrush = Colors.LightmodeContrastColor;
            this.Button1Bg = Colors.LightmodeContrastColor;
            this.Button1Fg = "White";
            this.Button2Bg = Colors.LightmodeContrastColor;
            this.Button2Fg = Colors.LightmodeContrastColor;
            this.Button2FgMouseOver = "White";
            this.ProgressColor = "Green";
        }

        #endregion
    }
}