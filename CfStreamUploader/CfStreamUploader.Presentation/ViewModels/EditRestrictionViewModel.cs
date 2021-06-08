using CfStreamUploader.Core;
using CfStreamUploader.Presentation.Resources.Colors;
using CfStreamUploader.Presentation.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;

namespace CfStreamUploader.Presentation.ViewModels
{
    public class EditRestrictionViewModel : ViewModelBase
    {
        #region fields

        private string currentCodesUrl = "https://en.wikipedia.org/wiki/ISO_3166-1#Current_codes";

        private string ipAction = "allow";
        private string countryAction = "allow";
        private string anyAction = "allow";

        private string ipTextBox = string.Empty;
        private string countryTextBox = string.Empty;
        private string expiresInTextBox = string.Empty;

        #endregion

        #region props

        public RelayCommand OpenCurrentCodeWebpageCommand { get; set; }
        public RelayCommand SaveButtonCommand { get; set; }
        public RelayCommand AllowIpsCommand { get; set; }
        public RelayCommand AllowCountriesCommand { get; set; }
        public RelayCommand AllowAllCommand { get; set; }
        public ConfigManager ConfigManager { get; set; } = new ConfigManager();

        public string IpAction
        {
            get => this.ipAction;
            set => this.Set(ref this.ipAction, value);
        }

        public string CountryAction
        {
            get => this.countryAction;
            set => this.Set(ref this.countryAction, value);
        }

        public string AnyAction
        {
            get => this.anyAction;
            set => this.Set(ref this.anyAction, value);
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

        public string ExpiresInTextBox
        {
            get => this.expiresInTextBox;
            set => this.Set(ref this.expiresInTextBox, value);
        }

        #endregion

        #region constructor

        public EditRestrictionViewModel()
        {
            this.OpenCurrentCodeWebpageCommand = new RelayCommand(this.OpenCurrentCodeWebpage);
            this.SaveButtonCommand = new RelayCommand(this.SaveButton);
            this.AllowIpsCommand = new RelayCommand(this.SetIpAction);
            this.AllowCountriesCommand = new RelayCommand(this.SetCountryAction);
            this.AllowAllCommand = new RelayCommand(this.SetAnyAction);

            this.ConfigManager.ReadConfig();

            this.IpTextBox = this.ConfigManager.Config.AccessRules.Ip.PrintIps();
            if (this.ConfigManager.Config.AccessRules.Ip.IsBlocked())
                this.IpAction = "block";

            this.CountryTextBox = this.ConfigManager.Config.AccessRules.Country.PrintCounties();
            if (this.ConfigManager.Config.AccessRules.Country.IsBlocked())
                this.CountryAction = "block";

            if (this.ConfigManager.Config.AccessRules.Any.IsBlocked())
                this.AnyAction = "block";

            this.expiresInTextBox = this.ConfigManager.Config.AccessRules.ExpiresIn.ToString();

            if (this.ConfigManager.Config.IsDarkmode)
                this.Darkmode();
            else
                this.Lightmode();
        }

        #endregion

        #region private

        private void OpenCurrentCodeWebpage()
        {
            var psi = new ProcessStartInfo()
            {
                FileName = "cmd",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = $"/c start {this.currentCodesUrl}"
            };
            Process.Start(psi);
        }

        private void SetAnyAction()
        {
            if (this.ConfigManager.Config.AccessRules.Any.IsBlocked())
            {
                this.AnyAction = "allow";
                this.ConfigManager.Config.AccessRules.Any.Allow();
            }
            else
            {
                this.AnyAction = "block";
                this.ConfigManager.Config.AccessRules.Any.Block();
            }
        }

        private void SetCountryAction()
        {
            if (this.ConfigManager.Config.AccessRules.Country.IsBlocked())
            {
                this.CountryAction = "allow";
                this.ConfigManager.Config.AccessRules.Country.Allow();
            }
            else
            {
                this.CountryAction = "block";
                this.ConfigManager.Config.AccessRules.Country.Block();
            }
        }

        private void SetIpAction()
        {
            if (this.ConfigManager.Config.AccessRules.Ip.IsBlocked())
            {
                this.IpAction = "allow";
                this.ConfigManager.Config.AccessRules.Ip.Allow();
            }
            else
            {
                this.IpAction = "block";
                this.ConfigManager.Config.AccessRules.Ip.Block();
            }
        }

        private void SaveButton()
        {
            var ipStrings = this.IpTextBox.Replace(" ", "").Split(",").ToList();
            if (!this.IsValidId(ipStrings))
            {
                MessageBox.Show("Make shure your IpAdresses are valid", "Warning", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            this.ConfigManager.Config.AccessRules.Ip.SetIpList(ipStrings);

            var countryString = this.CountryTextBox.Replace(" ", "");
            this.ConfigManager.Config.AccessRules.Country.SetCountryList(countryString.Split(",").ToList());

            if (!this.ExpiresInTextBox.All(char.IsDigit))
            {
                MessageBox.Show("The expiry  time is not a solid number", "Warning", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            this.ConfigManager.Config.AccessRules.ExpiresIn = Convert.ToInt32(this.ExpiresInTextBox);

            this.ConfigManager.UpdateConfig(this.ConfigManager.Config);

            WindowManager.CloseEditWindow();
        }

        private bool IsValidId(List<string> ipStrings)
        {
            return true; //TODO
            foreach (var ipString in ipStrings)
            {
                var result = IPAddress.TryParse(ipString, out var ipAdress);
                if (!result)
                    return false;
            }

            return true;
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
            this.BaseColor = Colors.DarkmodeBaseColor;
            this.TextColor = "White";
            this.Button1Bg = Colors.DarkmodeContrastColor;
            this.Button1Fg = "White";
        }

        private void Lightmode()
        {
            this.BaseColor = Colors.LightmodeBaseColor;
            this.TextColor = "Black";
            this.Button1Bg = Colors.LightmodeContrastColor;
            this.Button1Fg = "White";
        }

        #endregion
    }
}