using CfStreamUploader.Core;
using CfStreamUploader.Presentation.Resources.Colors;
using CfStreamUploader.Presentation.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Linq;

namespace CfStreamUploader.Presentation.ViewModels
{
    public class EditViewModel : ViewModelBase
    {
        #region fields

        private bool isDarkmode;

        private string blockAndAllowIp = "allow";
        private string blockAndAllowCountry = "allow";
        private string blockAndAllowAll = "allow";

        private string ipTextBox = string.Empty;
        private string countryTextBox = string.Empty;

        #endregion

        #region props

        public RelayCommand SaveButtonCommand { get; set; }
        public RelayCommand AllowIpsCommand { get; set; }
        public RelayCommand AllowCountriesCommand { get; set; }
        public RelayCommand AllowAllCommand { get; set; }
        public ConfigManager ConfigManager { get; set; } = new ConfigManager();

        public string BlockAndAllowIp
        {
            get => this.blockAndAllowIp;
            set => this.Set(ref this.blockAndAllowIp, value);
        }

        public string BlockAndAllowCountry
        {
            get => this.blockAndAllowCountry;
            set => this.Set(ref this.blockAndAllowCountry, value);
        }

        public string BlockAndAllowAll
        {
            get => this.blockAndAllowAll;
            set => this.Set(ref this.blockAndAllowAll, value);
        }

        public string IpTextBox
        {
            get => this.ipTextBox;
            set => this.Set(ref this.ipTextBox, value);
        }

        public string CountryTextBox
        {
            get => this.countryTextBox;
            set => this.Set(ref this.countryTextBox, value);
        }

        #endregion

        #region constructor

        public EditViewModel()
        {
            this.SaveButtonCommand = new RelayCommand(this.SaveButton);
            this.AllowIpsCommand = new RelayCommand(this.AllowIps);
            this.AllowCountriesCommand = new RelayCommand(this.AllowCountries);
            this.AllowAllCommand = new RelayCommand(this.AllowAll);

            this.ConfigManager.ReadConfig();

            this.IpTextBox = this.ConfigManager.Config.AccessRules.Ip.PrintIps();
            if (!this.ConfigManager.Config.AccessRules.Ip.IsBlocked())
                this.BlockAndAllowAll = "block";

            this.CountryTextBox = this.ConfigManager.Config.AccessRules.Country.PrintCounties();
            if (!this.ConfigManager.Config.AccessRules.Country.IsBlocked())
                this.BlockAndAllowCountry = "block";

            this.ConfigManager.Config.IsDarkmode = this.isDarkmode;
            if (this.isDarkmode)
                this.Darkmode();
            else
                this.Lightmode();
        }

        #endregion

        #region private

        private void AllowAll()
        {
            if (this.ConfigManager.Config.AccessRules.Any.IsBlocked())
            {
                this.BlockAndAllowAll = "block";
                this.ConfigManager.Config.AccessRules.Any.Block();
            }
            else
            {
                this.BlockAndAllowAll = "allow";
                this.ConfigManager.Config.AccessRules.Any.Allow();
            }
        }

        private void AllowCountries()
        {
            if (this.ConfigManager.Config.AccessRules.Country.IsBlocked())
            {
                this.BlockAndAllowCountry = "block";
                this.ConfigManager.Config.AccessRules.Country.Block();
            }
            else
            {
                this.BlockAndAllowCountry = "allow";
                this.ConfigManager.Config.AccessRules.Country.Allow();
            }
        }

        private void AllowIps()
        {
            if (this.ConfigManager.Config.AccessRules.Ip.IsBlocked())
            {
                this.BlockAndAllowIp = "block";
                this.ConfigManager.Config.AccessRules.Ip.Block();
            }
            else
            {
                this.BlockAndAllowIp = "allow";
                this.ConfigManager.Config.AccessRules.Ip.Allow();
            }
        }

        private void SaveButton()
        {
            var ipString = this.IpTextBox.Trim();
            this.ConfigManager.Config.AccessRules.Ip.SetIpList(ipString.Split(",").ToList());

            var countryString = this.CountryTextBox.Trim();
            this.ConfigManager.Config.AccessRules.Country.SetCountryList(countryString.Split(",").ToList());

            this.ConfigManager.UpdateConfig(this.ConfigManager.Config);

            WindowManager.CloseEditWindow();
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
            this.BaseColor = Colors.BlackmodeBaseColor;
            this.TextColor = "White";
            this.Button1Bg = Colors.BlackmodeContrastColor;
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