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

            Equals(any.Action, "block");
            Equals(isBlockes, true);

            #region Assert

            #endregion
        }

        [Fact]
        public void AnyAllowTest()
        {
            #region Assign

            var any = new Any();

            #endregion

            any.Allow();
            var isBlockes = any.IsBlocked();

            #region Act

            #endregion

            Equals(any.Action, "allow");
            Equals(isBlockes, false);

            #region Assert

            #endregion
        }

        [Fact]
        public void AnyPrintTest()
        {
            #region Assign

            var any = new Any();

            #endregion

            var result = any.PrintRestriction();

            #region Act

            #endregion

            Equals(result, "allow any");

            #region Assert

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

            ip.Block();
            var isBlockes = ip.IsBlocked();

            #region Act

            #endregion

            Equals(ip.Action, "block");
            Equals(isBlockes, true);

            #region Assert

            #endregion
        }

        [Fact]
        public void IpAllowTest()
        {
            #region Assign

            var ip = new Ip();

            #endregion

            ip.Allow();
            var isBlockes = ip.IsBlocked();

            #region Act

            #endregion

            Equals(ip.Action, "allow");
            Equals(isBlockes, false);

            #region Assert

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

            var printedRestriction = ip.PrintRestriction();
            var printedIps = ip.PrintIps();

            #region Act

            #endregion

            #region Assert

            Equals(printedRestriction, "allow MyIp1, MyIp2");
            Equals(printedIps, "MyIp1, MyIp2");

            #endregion
        }

        [Fact]
        public void SetIpListTest()
        {
            #region Assign

            var ip = new Ip();
            var ipList = new List<string>() {"MyIp", "MyIp1", "MyIp2"};

            #endregion

            ip.SetIpList(ipList);

            #region Act

            #endregion

            #region Assert

            Equals(ip.Ips, ipList);

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

            country.Block();
            var isBlockes = country.IsBlocked();

            #region Act

            #endregion

            Equals(country.Action, "block");
            Equals(isBlockes, true);

            #region Assert

            #endregion
        }

        [Fact]
        public void CountryAllowTest()
        {
            #region Assign

            var country = new Country();

            #endregion

            country.Allow();
            var isBlockes = country.IsBlocked();

            #region Act

            #endregion

            Equals(country.Action, "allow");
            Equals(isBlockes, false);

            #region Assert

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

            var printedRestriction = country.PrintRestriction();
            var printedIps = country.PrintCounties();

            #region Act

            #endregion

            Equals(printedRestriction, "allow DE, ES");
            Equals(printedIps, "DE, ES");

            #region Assert

            #endregion
        }

        [Fact]
        public void SetCountryListTest()
        {
            #region Assign

            var ip = new Country();
            var countryList = new List<string>() {"DE", "ES", "US"};

            #endregion

            ip.SetCountryList(countryList);

            #region Act

            #endregion

            #region Assert

            Equals(ip.Countries, countryList);

            #endregion

            #endregion
        }
    }
}