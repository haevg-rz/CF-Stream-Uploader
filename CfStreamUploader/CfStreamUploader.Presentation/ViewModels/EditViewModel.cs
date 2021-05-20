using System.Collections.Generic;
using System.Collections.ObjectModel;
using CfStreamUploader.Core.Models;
using GalaSoft.MvvmLight;

namespace CfStreamUploader.Presentation.ViewModels
{
    public class EditViewModel : ViewModelBase
    {
        #region fields

        private bool isDarkmode;

        private ObservableCollection<string> countries = new ObservableCollection<string>(){"DE", "FR", "ES", "US", "EN"};

        #endregion

        #region props

        public ObservableCollection<string> Countries
        {
            get => this.countries;
            set => this.Set(ref this.countries, value);
        }

        #endregion

        #region constructor
        public EditViewModel(Config config)
        {
            this.isDarkmode = config.IsDarkmode;
            if (this.isDarkmode)
                this.Darkmode();
            else
                this.Lightmode();
        }

        public EditViewModel()
        {

        }
        
        #endregion
       
        #region colorChange

        private string baseColor = "Transparent";
       

        public string BaseColor
        {
            get => this.baseColor;
            set => this.Set(ref this.baseColor, value);
        }

        private void Darkmode()
        {
            this.BaseColor = "#1b2867";

        }

        private void Lightmode()
        {
            this.BaseColor = "Transparent";
        }

        #endregion
    }
}