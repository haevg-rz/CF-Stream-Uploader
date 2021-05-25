using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CfStreamUploader.Core.Models
{
    public class Restrictions
    {
        public RestrictionAny RestrictionAny { get; }
        public RestrictionIp RestrictionIp { get; }
        public RestrictionCountry RestrictionCountry { get; }

        public Restrictions()
        {
            this.RestrictionAny = new RestrictionAny();
            this.RestrictionIp = new RestrictionIp();
            this.RestrictionCountry = new RestrictionCountry();
        }

        public Restrictions(Restrictions restrictions)
        {
            this.RestrictionAny = restrictions.RestrictionAny;
            this.RestrictionIp = restrictions.RestrictionIp;
            this.RestrictionCountry = restrictions.RestrictionCountry;
        }
    }

    #region restrictions

    public class RestrictionAny
    {
        [JsonPropertyName("action")] public string Action { get; private set; } = "allow";
        [JsonPropertyName("type")] public string Type { get; } = "any";

        public void Allow()
        {
            this.Action = "allow";
        }

        public void Block()
        {
            this.Action = "block";
        }

        public string GetRestrictionAny()
        {
            return $"{this.Action} {this.Type}";
        }
    }

    public class RestrictionIp
    {
        [JsonPropertyName("action")] public string Action { get; private set; }
        [JsonPropertyName("type")] public string Type { get; }
        [JsonPropertyName("ip")] public List<string> Ip { get; private set; }

        public RestrictionIp()
        {
            this.Action = "allow";
            this.Type = "ip.src";
            this.Ip = new List<string>();
        }

        public RestrictionIp(RestrictionIp restrictionIp)
        {
            this.Action = restrictionIp.Action;
            this.Type = restrictionIp.Type;
            this.Ip = restrictionIp.Ip;
        }

        public RestrictionIp(string action, string type, List<string> ip)
        {
            this.Action = action;
            this.Type = type;
            this.Ip = ip;
        }


        public void Allow()
        {
            this.Action = "allow";
        }

        public void Block()
        {
            this.Action = "block";
        }

        public void SetIpList(List<string> ipList)
        {
            this.Ip = ipList;
        }

        public void Delete(string ip)
        {
            this.Ip.Remove(ip);
        }

        public string GetRestrictionIp()
        {
            return $"{this.Action} {string.Join(",", this.Ip.ToArray())}";
        }
        public string PrintIps()
        {
            return $"{string.Join(",", this.Ip.ToArray())}";
        }
    }

    public class RestrictionCountry
    {
        [JsonPropertyName("action")] public string Action { get; private set; }
        [JsonPropertyName("type")] public string Type { get; }
        [JsonPropertyName("country")] public List<string> Country { get ; private set; }
        
        public RestrictionCountry()
        {
            this.Action = "allow";
            this.Type = "ip.geoip.country";
            this.Country = new List<string>();
        }

        public RestrictionCountry(RestrictionCountry restrictionCountry)
        {
            this.Action = restrictionCountry.Action;
            this.Type = restrictionCountry.Type;
            this.Country = restrictionCountry.Country;
        }

        public RestrictionCountry(string action, string type, List<string> country)
        {
            this.Action = action;
            this.Type = type;
            this.Country = country;
        }

        public void SetCountryList(List<string> country)
        {
            this.Country = country;
        }

        public void Remove(string country)
        {
            this.Country.Remove(country);
        }

        public void Allow()
        {
            this.Action = "allow";
        }

        public void Block()
        {
            this.Action = "block";
        }

        public string GetRestrictionCountry()
        {
            return $"{this.Action} {string.Join(",", this.Country.ToArray())}";
        }
        public string PrintCounties()
        {
            return $"{string.Join(",", this.Country.ToArray())}";
        }

        #endregion

    }
}