using CfStreamUploader.Core.Models;
using System.Collections.Generic;
using Xunit;

namespace CfStreamUploader.Core.Test.ModelTests
{
    public class AccessRulesTest
    {
        #region Any tests

        [Fact]
        public void AnyBlockTest()
        {
            #region Assign

            var any = new Any();

            #endregion

            any.Block();
            var isBlockes = any.IsBlocked();

            #region Act

            #endregion

            Assert.Equal(any.Action, "block");
            Assert.Equal(isBlockes, true);

            #region Assert

            #endregion
        }

        [Fact]
        public void AnyAllowTest()
        {
            #region Assign

            var any = new Any();

            #endregion

            #region Act

            any.Allow();
            var isBlockes = any.IsBlocked();

            #endregion

            #region Assert

            Assert.Equal(any.Action, "allow");
            Assert.Equal(isBlockes, false);

            #endregion
        }

        [Fact]
        public void AnyPrintTest()
        {
            #region Assign

            var any = new Any();

            #endregion

            #region Act

            var result = any.PrintRestriction();

            #endregion

            #region Assert

            Assert.Equal(result, "allow any");

            #endregion
        }

        #endregion

        #region Ip tests

        [Fact]
        public void IpBlockTest()
        {
            #region Assign

            var ip = new Ip();

            #endregion

            #region Act

            ip.Block();
            var isBlockes = ip.IsBlocked();

            #endregion

            #region Assert

            Assert.Equal(ip.Action, "block");
            Assert.Equal(isBlockes, true);

            #endregion
        }

        [Fact]
        public void IpAllowTest()
        {
            #region Assign

            var ip = new Ip();

            #endregion

            #region Act

            ip.Allow();
            var isBlockes = ip.IsBlocked();

            #endregion

            #region Assert

            Assert.Equal(ip.Action, "allow");
            Assert.Equal(isBlockes, false);

            #endregion
        }

        [Fact]
        public void IpPrintTest()
        {
            #region Assign

            var ip = new Ip
            {
                Ips = new List<string>() {"MyIp1", "MyIp2"}
            };

            #endregion

            #region Act

            var printedRestriction = ip.PrintRestriction();
            var printedIps = ip.PrintIps();

            #endregion

            #region Assert

            Assert.Equal(printedRestriction, "allow MyIp1, MyIp2");
            Assert.Equal(printedIps, "MyIp1, MyIp2");

            #endregion
        }

        [Fact]
        public void SetIpListTest()
        {
            #region Assign

            var ip = new Ip();
            var ipList = new List<string>() {"MyIp", "MyIp1", "MyIp2"};

            #endregion

            #region Act

            ip.SetIpList(ipList);

            #endregion

            #region Assert

            Assert.Equal(ip.Ips, ipList);

            #endregion
        }

        #endregion

        #region Country tests

        [Fact]
        public void CountryBlockTest()
        {
            #region Assign

            var country = new Country();

            #endregion

            #region Act

            country.Block();
            var isBlockes = country.IsBlocked();

            #endregion

            #region Assert

            Assert.Equal(country.Action, "block");
            Assert.Equal(isBlockes, true);

            #endregion
        }

        [Fact]
        public void CountryAllowTest()
        {
            #region Assign

            var country = new Country();

            #endregion

            #region Act

            country.Allow();
            var isBlockes = country.IsBlocked();

            #endregion

            #region Assert

            Assert.Equal(country.Action, "allow");
            Assert.Equal(isBlockes, false);

            #endregion
        }

        [Fact]
        public void CountryPrintTest()
        {
            #region Assign

            var country = new Country
            {
                Countries = new List<string>() {"DE", "ES"}
            };

            #endregion

            #region Act

            var printedRestriction = country.PrintRestriction();
            var printedIps = country.PrintCounties();

            #endregion

            #region Assert

            Assert.Equal(printedRestriction, "allow DE, ES");
            Assert.Equal(printedIps, "DE, ES");

            #endregion
        }

        [Fact]
        public void SetCountryListTest()
        {
            #region Assign

            var ip = new Country();
            var countryList = new List<string>() {"DE", "ES", "US"};

            #endregion

            #region Act

            ip.SetCountryList(countryList);

            #endregion

            #region Assert

            Assert.Equal(ip.Countries, countryList);

            #endregion

            #endregion
        }
    }
}