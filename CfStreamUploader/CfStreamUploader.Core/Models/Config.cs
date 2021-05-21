using System;

namespace CfStreamUploader.Core.Models
{
    public class Config
    {
        #region props

        public UserSettings UserSettings { get; set; }
        public Restrictions Restrictions { get; set; }
        public bool IsDarkmode { get; set; }

        #endregion

        #region constructor

        public Config()
        {
            this.UserSettings = new UserSettings();
            this.Restrictions = new Restrictions();
            this.IsDarkmode = false;
        }

        public Config(UserSettings userSettings, int expiresIn, Restrictions restrictions, bool isDarkmode)
        {
            this.UserSettings = userSettings;
            this.Restrictions = restrictions;
            this.IsDarkmode = isDarkmode;
        }

        public Config(Config config)
        {
            this.UserSettings = config.UserSettings;
            this.Restrictions = config.Restrictions;
            this.IsDarkmode = config.IsDarkmode;
        }

        #endregion
    }
}