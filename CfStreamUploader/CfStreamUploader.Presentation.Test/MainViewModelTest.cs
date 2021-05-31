using CfStreamUploader.Presentation.ViewModels;
using Xunit;

namespace CfStreamUploader.Presentation.Test
{
    public class MainViewModelTest
    {
        #region Prop

        private MainViewModel MainViewModel { get; set; } = new MainViewModel();

        #endregion

        [Fact]
        public void IsConfigSolidTest()
        {
            #region Assign

            var config = TestSamples.Samples.ConfigSample;
            #endregion

            #region Act

            var result = this.MainViewModel.IsConfigSolid(config);

            #endregion

            #region Assert

            Assert.True(result);

            #endregion
        }

        [Fact]
        public void SetRestrictionsTest()
        {
            #region Assign

            var config = TestSamples.Samples.AccesRulesConfigSample;

            #endregion

            #region Act

            this.MainViewModel.SetRestrictions(config);

            #endregion

            #region Assert

            Assert.Equal("allow DE, FR", this.MainViewModel.RestrictionCountry);
            Assert.Equal("allow any", this.MainViewModel.RestrictionAny);
            Assert.Equal("allow TestIp", this.MainViewModel.RestrictionIP);

            #endregion
        }

    }
}
