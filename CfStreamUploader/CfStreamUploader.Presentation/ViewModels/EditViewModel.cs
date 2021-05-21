using CfStreamUploader.Core;
using CfStreamUploader.Core.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace CfStreamUploader.Presentation.ViewModels
{
    public class EditViewModel : ViewModelBase
    {
        #region fields

        private bool isDarkmode;

        private ObservableCollection<string> countries = new ObservableCollection<string>()
            {"DE", "FR", "ES", "US", "EN"};

        #endregion

        #region props

        public RelayCommand SaveButtonCommand { get; set; }
        public ConfigManager ConfigManager { get; set; } = new ConfigManager();

        public ObservableCollection<string> Countries
        {
            get => this.countries;
            set => this.Set(ref this.countries, value);
        }

        #endregion

        #region constructor

        public EditViewModel(Restrictions restrictions, bool isDarkmode)
        {
            this.SaveButtonCommand = new RelayCommand(this.SaveButton);

            this.ConfigManager.ReadConfig();

            this.isDarkmode = isDarkmode;
            if (this.isDarkmode)
                this.Darkmode();
            else
                this.Lightmode();
        }

        private void SaveButton()
        {
            //new Config with given props
            var newConfig = this.ConfigManager.Config;

            // this.ConfigManager.UpdateConfig(newConfig);
        }

        public EditViewModel()
        {
        }

        #endregion

        #region colorChange

        private string baseColor = "Transparent";
        private string textColor = "Black";
        private string button1Bg = "#6497e8";
        private string button1Fg = "White";

        public string BaseColor
        {
            get => this.baseColor;
            set => this.Set(ref this.baseColor, value);
        }

        public string TextColor
        {
            get => this.textColor;
            set => this.Set(ref this.textColor, value);
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

        private void Darkmode()
        {
            // this.BaseColor = "#1b2867";
            this.BaseColor = "#162770";
            this.TextColor = "White";
            this.Button1Bg = "#20328a";
            this.Button1Fg = "White";
        }

        private void Lightmode()
        {
            this.BaseColor = "Transparent";
            this.TextColor = "Black";
            this.Button1Bg = "#6497e8";
            this.Button1Fg = "White";
        }

        #endregion
    }
}