using CfStreamUploader.Presentation.Resources.Colors;
using CfStreamUploader.Presentation.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

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
        private string restrictionExpireIn = string.Empty;
        private bool checkboxRestrictionIP;
        private bool checkboxRestrictionCountry;
        private bool checkboxRestrictionAny = true;
        private bool checkboxRestrictionExpireIn;
        private bool setSignedUrl = true;

        //VideoPogressBar
        private bool checkImage1IsVisible;
        private bool checkImage2IsVisible;
        private bool checkImage3IsVisible;
        private bool checkImage4IsVisible;
        private bool checkImage5IsVisible;

        private bool loadingAnimation1IsVisible;
        private bool loadingAnimation2IsVisible;
        private bool loadingAnimation3IsVisible;
        private bool loadingAnimation4IsVisible;

        private bool uloadingisDone;
        private bool setPrivateIsDone;
        private bool addRestrictionIsDone;
        private bool generateHtmlIsDone;
        private bool allProcessesAreDone;

        //VideoPogressBar

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

        public string RestrictionExpireIn
        {
            get => this.restrictionExpireIn;
            set => this.Set(ref this.restrictionExpireIn, value);
        }

        public bool CheckboxRestrictionIP
        {
            get => this.checkboxRestrictionIP;
            set => this.Set(ref this.checkboxRestrictionIP, value);
        }

        public bool CheckboxRestrictionCountry
        {
            get => this.checkboxRestrictionCountry;
            set => this.Set(ref this.checkboxRestrictionCountry, value);
        }

        public bool CheckboxRestrictionAny
        {
            get => this.checkboxRestrictionAny;
            set => this.Set(ref this.checkboxRestrictionAny, value);
        }

        public bool CheckboxRestrictionExpireIn
        {
            get => this.checkboxRestrictionExpireIn;
            set => this.Set(ref this.checkboxRestrictionExpireIn, value);
        }

        public bool SetSignedUrl
        {
            get => this.setSignedUrl;
            set => this.Set(ref this.setSignedUrl, value);
        }

        //VideoPogressBar
        public bool CheckImage1IsVisible
        {
            get => this.checkImage1IsVisible;
            set => this.Set(ref this.checkImage1IsVisible, value);
        }

        public bool CheckImage2IsVisible
        {
            get => this.checkImage2IsVisible;
            set => this.Set(ref this.checkImage2IsVisible, value);
        }

        public bool CheckImage3IsVisible
        {
            get => this.checkImage3IsVisible;
            set => this.Set(ref this.checkImage3IsVisible, value);
        }

        public bool CheckImage4IsVisible
        {
            get => this.checkImage4IsVisible;
            set => this.Set(ref this.checkImage4IsVisible, value);
        }

        public bool CheckImage5IsVisible
        {
            get => this.checkImage5IsVisible;
            set => this.Set(ref this.checkImage5IsVisible, value);
        }

        public bool LoadingAnimation1IsVisible
        {
            get => this.loadingAnimation1IsVisible;
            set => this.Set(ref this.loadingAnimation1IsVisible, value);
        }

        public bool LoadingAnimation2IsVisible
        {
            get => this.loadingAnimation2IsVisible;
            set => this.Set(ref this.loadingAnimation2IsVisible, value);
        }

        public bool LoadingAnimation3IsVisible
        {
            get => this.loadingAnimation3IsVisible;
            set => this.Set(ref this.loadingAnimation3IsVisible, value);
        }

        public bool LoadingAnimation4IsVisible
        {
            get => this.loadingAnimation4IsVisible;
            set => this.Set(ref this.loadingAnimation4IsVisible, value);
        }

        public bool UloadingisDone
        {
            get => this.uloadingisDone;
            set => this.Set(ref this.uloadingisDone, value);
        }

        public bool SetPrivateIsDone
        {
            get => this.setPrivateIsDone;
            set => this.Set(ref this.setPrivateIsDone, value);
        }

        public bool AddRestrictionIsDone
        {
            get => this.addRestrictionIsDone;
            set => this.Set(ref this.addRestrictionIsDone, value);
        }

        public bool GenerateHtmlIsDone
        {
            get => this.generateHtmlIsDone;
            set => this.Set(ref this.generateHtmlIsDone, value);
        }

        public bool AllProcessesAreDone
        {
            get => this.allProcessesAreDone;
            set => this.Set(ref this.allProcessesAreDone, value);
        }
        //VideoPogressBar

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

            this.SetRestrictions();

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

            this.Core.VideoManager.VideoPath = ((DataObject) dropInfo.Data).GetFileDropList().Cast<string>().First();
            this.VideoTitel = this.Core.VideoManager.VideoPath.Split("\\").Last();
        }

        #endregion

        #region private

        private async void UploadVideoAsync()
        {
            if (this.Core.VideoManager.VideoPath == string.Empty)
            {
                MessageBox.Show("Please select a Video.", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            this.VideoUploadPogresBarSetUpStart();

            if (!this.IsConfigSolid()) return;

            var videoUploadResult = await this.Core.VideoManager.UploadVideoAsync(this.Core.ConfigManager.Config);

            this.VideoUploadPogresBarStepp1();

            if (videoUploadResult.videoUploadResult.Success)
            {
                if (this.SetSignedUrl)
                {
                    var signedUrlResult = await this.Core.VideoManager.SetSignedUrl(this.Core.ConfigManager.Config,
                        videoUploadResult.VideoUrl);

                    if (signedUrlResult.Success)
                    {
                        this.VideoUploadPogresBarStepp2();

                        var videoToken = this.Core.VideoManager.SetRestrictions(this.Core.ConfigManager.Config,
                            videoUploadResult.VideoUrl, this.CheckboxRestrictionIP, this.CheckboxRestrictionCountry,
                            this.CheckboxRestrictionAny, this.CheckboxRestrictionExpireIn);

                        this.VideoUploadPogresBarStepp3();

                        this.HtmlOutput = string.Format(this.Core.HtmlLayout.GetHtmlLayout(), videoToken);
                        this.videoUrl = string.Format(this.defaultUri, videoToken);

                        this.VideoUploadPogresBarStepp4();

                        await Task.Delay(TimeSpan.FromSeconds(3));
                        this.VideoUploadPogressBarFinish();

                        return;
                    }
                }

                this.HtmlOutput = string.Format(this.Core.HtmlLayout.GetHtmlLayout(), videoUploadResult.VideoUrl);
                this.videoUrl = string.Format(this.defaultUri, videoUploadResult.VideoUrl);

                await Task.Delay(TimeSpan.FromSeconds(5));
                this.VideoUploadPogresBarStepp4();
                this.LoadingAnimation2IsVisible = false;
                this.VideoUploadPogressBarFinish();
            }
            else
            {
                MessageBox.Show(videoUploadResult.videoUploadResult.Exception.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        internal bool IsConfigSolid()
        {
            if (this.Core.ConfigManager.Config.UserSettings.CfToken != string.Empty &&
                this.Core.ConfigManager.Config.UserSettings.CfAccount != string.Empty) return true;

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
                this.Core.VideoManager.VideoPath = fileDialog.FileName;
                this.VideoTitel = this.Core.VideoManager.VideoPath.Split("\\").Last();
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

            this.SetRestrictions();
        }

        internal void SetRestrictions()
        {
            this.RestrictionCountry = this.Core.ConfigManager.Config.AccessRules.Country.PrintRestriction();
            this.RestrictionAny = this.Core.ConfigManager.Config.AccessRules.Any.PrintRestriction();
            this.RestrictionIP = this.Core.ConfigManager.Config.AccessRules.Ip.PrintRestriction();
            this.RestrictionExpireIn =
                string.Format($"expiry date in {this.Core.ConfigManager.Config.AccessRules.ExpiresIn} days");
        }

        //VideoPogressBar
        private void VideoUploadPogresBarSetUpStart()
        {
            this.CheckImage1IsVisible = false;
            this.CheckImage2IsVisible = false;
            this.CheckImage3IsVisible = false;
            this.CheckImage4IsVisible = false;
            this.CheckImage5IsVisible = false;
            this.LoadingAnimation1IsVisible = true;
            this.LoadingAnimation2IsVisible = false;
            this.LoadingAnimation3IsVisible = false;
            this.LoadingAnimation4IsVisible = false;
            this.UloadingisDone = false;
            this.SetPrivateIsDone = false;
            this.AddRestrictionIsDone = false;
            this.GenerateHtmlIsDone = false;
            this.AllProcessesAreDone = false;
        }

        private void VideoUploadPogresBarStepp1()
        {
            this.LoadingAnimation1IsVisible = false;
            this.CheckImage1IsVisible = true;
            this.LoadingAnimation2IsVisible = true;
            this.UloadingisDone = true;
        }

        private void VideoUploadPogresBarStepp2()
        {
            this.LoadingAnimation2IsVisible = false;
            this.CheckImage2IsVisible = true;
            this.LoadingAnimation3IsVisible = true;
            this.SetPrivateIsDone = true;
        }

        private void VideoUploadPogresBarStepp3()
        {
            this.LoadingAnimation3IsVisible = false;
            this.CheckImage3IsVisible = true;
            this.LoadingAnimation4IsVisible = true;
            this.AddRestrictionIsDone = true;
        }

        private void VideoUploadPogresBarStepp4()
        {
            this.LoadingAnimation4IsVisible = false;
            this.CheckImage4IsVisible = true;
            this.GenerateHtmlIsDone = true;
        }

        private void VideoUploadPogressBarFinish()
        {
            this.CheckImage5IsVisible = true;
            this.AllProcessesAreDone = true;
        }
        //VideoPogressBar

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