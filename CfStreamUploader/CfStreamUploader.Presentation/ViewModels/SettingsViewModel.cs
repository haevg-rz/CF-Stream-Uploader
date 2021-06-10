using CfStreamUploader.Core;
using CfStreamUploader.Presentation.Resources.Colors;
using CfStreamUploader.Presentation.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CfStreamUploader.Presentation.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region fields

        private string cfToken;
        private string cfAccount;
        private string keyId;
        private string privateKey;

        #endregion

        #region props

        public string CfToken
        {
            get => this.cfToken;
            set => this.Set(ref this.cfToken, value);
        }

        public string CfAccount
        {
            get => this.cfAccount;
            set => this.Set(ref this.cfAccount, value);
        }

        public string KeyId
        {
            get => this.keyId;
            set => this.Set(ref this.keyId, value);
        }

        public string PrivateKey
        {
            get => this.privateKey;
            set => this.Set(ref this.privateKey, value);
        }

        public ConfigManager ConfigManager { get; } = new ConfigManager();
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand UndoCommand { get; set; }

        #endregion

        #region constructors

        public SettingsViewModel()
        {
            this.SaveCommand = new RelayCommand(this.Save);
            this.UndoCommand = new RelayCommand(this.Undo);

            this.CfAccount = this.ConfigManager.Config.UserSettings.CfAccount;
            this.CfToken = this.ConfigManager.Config.UserSettings.CfToken;
            this.KeyId = this.ConfigManager.Config.UserSettings.KeyId;
            this.PrivateKey = this.ConfigManager.Config.UserSettings.PrivateKey;

            if (this.ConfigManager.Config.IsDarkmode)
                this.Darkmode();
            else
                this.Lightmode();
        }

        #endregion

        #region methods

        private void Undo()
        {
            this.CfAccount = this.ConfigManager.Config.UserSettings.CfAccount;
            this.CfToken = this.ConfigManager.Config.UserSettings.CfToken;
            this.KeyId = this.ConfigManager.Config.UserSettings.KeyId;
            this.PrivateKey = this.ConfigManager.Config.UserSettings.PrivateKey;
        }

        private void Save()
        {
            this.SetNewConfig();
            this.ConfigManager.UpdateConfig(this.ConfigManager.Config);

            WindowManager.CloseSettingsWindow();
        }

        private void SetNewConfig()
        {
            this.ConfigManager.Config.UserSettings.CfAccount = this.CfAccount;
            this.ConfigManager.Config.UserSettings.CfToken = this.CfToken;
            this.ConfigManager.Config.UserSettings.KeyId = this.KeyId;
            this.ConfigManager.Config.UserSettings.PrivateKey = this.PrivateKey;
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