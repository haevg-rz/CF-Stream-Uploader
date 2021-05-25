﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CfStreamUploader.Core.Models
{
    public class AccessRules
    {
        [JsonPropertyName("any")]public Any Any { get; }
        [JsonPropertyName("ip")] public Ip Ip { get; }
        [JsonPropertyName("country")] public Country Country { get; }

        public AccessRules()
        {
            this.Any = new Any();
            this.Ip = new Ip();
            this.Country = new Country();
        }

        public AccessRules(AccessRules accessRules)
        {
            this.Any = accessRules.Any;
            this.Ip = accessRules.Ip;
            this.Country = accessRules.Country;
        }
    }

    #region restrictions

    public class Any
    {
        [JsonPropertyName("action")] public string Action { get; set; } = "allow";
        [JsonPropertyName("type")] public string Type { get; } = "any";

        public void Allow()
        {
            this.Action = "allow";
        }

        public void Block()
        {
            this.Action = "block";
        }

        public bool IsBlocked()
        {
            return this.Action == "block";
        }

        public string PrintRestriction()
        {
            return $"{this.Action} {this.Type}";
        }
    }

    public class Ip
    {
        [JsonPropertyName("action")] public string Action { get; set; }
        [JsonPropertyName("type")] public string Type { get; }
        [JsonPropertyName("ip")] public List<string> Ips { get; set; }

        public Ip()
        {
            this.Action = "allow";
            this.Type = "ip.src";
            this.Ips = new List<string>();
        }
        
        public Ip(Ip ip)
        {
            this.Action = ip.Action;
            this.Type = ip.Type;
            this.Ips = ip.Ips;
        }
        
        public Ip(string action, string type, List<string> ips)
        {
            this.Action = action;
            this.Type = type;
            this.Ips = ips;
        }

        public void Allow()
        {
            this.Action = "allow";
        }

        public void Block()
        {
            this.Action = "block";
        }

        public bool IsBlocked()
        {
            return this.Action == "block";
        }

        public void SetIpList(List<string> ipList)
        {
            this.Ips = ipList;
        }

        public string PrintRestriction()
        {
            return $"{this.Action} {string.Join(",", this.Ips.ToArray())}";
        }

        public string PrintIps()
        {
            return $"{string.Join(",", this.Ips.ToArray())}";
        }
    }

    public class Country
    {
        [JsonPropertyName("action")] public string Action { get; set; }
        [JsonPropertyName("type")] public string Type { get; }
        [JsonPropertyName("country")] public List<string> Countries { get; set; }

        public Country()
        {
            this.Action = "allow";
            this.Type = "ip.geoip.country";
            this.Countries = new List<string>();
        }
        
        public Country(Country country)
        {
            this.Action = country.Action;
            this.Type = country.Type;
            this.Countries = country.Countries;
        }
        
        public Country(string action, string type, List<string> countries)
        {
            this.Action = action;
            this.Type = type;
            this.Countries = countries;
        }

        public void SetCountryList(List<string> country)
        {
            this.Countries = country;
        }

        public void Allow()
        {
            this.Action = "allow";
        }

        public void Block()
        {
            this.Action = "block";
        }

        public bool IsBlocked()
        {
            return this.Action == "block";
        }

        public string PrintRestriction()
        {
            return $"{this.Action} {string.Join(", ", this.Countries.ToArray())}";
        }

        public string PrintCounties()
        {
            return $"{string.Join(", ", this.Countries.ToArray())}";
        }

        #endregion
    }
}