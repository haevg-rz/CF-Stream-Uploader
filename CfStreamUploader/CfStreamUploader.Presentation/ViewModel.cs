using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace CfStreamUploader.Presentation
{
    public class ViewModel : ViewModelBase
    {
        #region Fields

        private string htmlOutput = "HTML";

        #endregion

        #region props

        public string HtmlOutput
        {
            get => this.htmlOutput;
            set => this.Set(ref this.htmlOutput, value);
        }

        public Core.Core Core { get; set; } = new Core.Core();


        #endregion

        #region RelayCommands

        public RelayCommand CopyToClipbordCommad { get; set; }
        public RelayCommand UploadViedeoCommand { get; set; }

        #endregion

        #region Constructor

        public ViewModel()
        {
            this.SetDarkmodeCommand = new RelayCommand(this.SetDarkmode);
            this.UploadViedeoCommand = new RelayCommand(this.UploadVideo);
            this.CopyToClipbordCommad = new RelayCommand(this.CopyToClipbord);
        }

        #endregion

        #region Methods

        private void UploadVideo()
        {
            
            this.Core.VideoUploader.UploadVideo();
            //Step1

            var videoToken = this.Core.VideoUploader.GetToken(this.Core.ConfigManager.Config);
            //Step2

            this.HtmlOutput = this.Core.HtmlLayout.GetHtmlLayout();
            //Step3

            //finished
            //Step4
        }

        private void CopyToClipbord()
        {
            Clipboard.SetText(this.HtmlOutput);
        }
        
        #endregion

        #region ColorChange

        private bool isDarkmode = false;
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
                this.BaseColor = "Transparent";
                this.ContrastColor = "Transparent";
                this.TextColor = "Black";
                this.BorderBrush = "#6497e8";
                this.Button1Bg = "#6497e8";
                this.Button1Fg = "White";
                this.Button2Bg = "#6497e8";
                this.Button2Fg = "#6497e8";
                this.Button2FgMouseOver = "White";
                this.ProgressColor = "Green";

                this.ThemeText = "Lightmode";
                this.isDarkmode = false;
            }
            else
            {
                this.BaseColor = "#1b2867";
                this.ContrastColor = "#223075";
                this.TextColor = "White";
                this.BorderBrush = "White";
                this.Button1Bg = "#223075";
                this.Button1Fg = "White";
                this.Button2Bg = "White";
                this.Button2Fg = "White";
                this.Button2FgMouseOver = "#223075";
                this.ProgressColor = "LawnGreen";

                this.ThemeText = "Darkmode";
                this.isDarkmode = true;
            }
        }

        #endregion
    }
}