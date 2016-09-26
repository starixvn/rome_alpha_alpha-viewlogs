namespace Alpha.ViewLogs.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using Constants;
    using Criteria;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models;
    using Sunplus.Agent.Model.Entities;

    public class IPServiceImpl : IPService
    {
        private const string VietnamIPListCacheKey = "Alpha.ViewLogs.VietnamIPList";
        private const string IPValidateRegex = @"(?:(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d\d?)";
        private const int OneHour = 1;

        private readonly IUserSession userSession;
        private readonly IDynamicRepository dynamicRepository;

        public IPServiceImpl(IUserSession userSession, IDynamicRepository dynamicRepository)
        {
            this.userSession = userSession;
            this.dynamicRepository = dynamicRepository;
        }

        public string FormatIP(string ip)
        {
            if (!IsValidIP(ip))
            {
                return ip;
            }

            Regex regex = new Regex(IPValidateRegex);
            if (!regex.IsMatch(ip, 0))
            {
                return $"<a class=\"iplink\" href=\"javascript:;\">{ip}</a>";
            }

            if (IsVNIP(ip)
                && !userSession.IsAdminCanViewVNIP)
            {
                return string.Empty;
            }

            return $"<a class=\"iplink\" href=\"javascript:OpenIPInfo('{ip}');\">{ip}</a>";
        }

        public string FormatCountryCode(string ip, string country)
        {
            if (!IsValidIP(ip)
                || (IsVNIP(ip) && !userSession.IsAdminCanViewVNIP))
            {
                return string.Empty;
            }

            return country;
        }

        public double ConvertIPToNumber(string ip)
        {
            double num = 0.0;

            Regex regex = new Regex(IPValidateRegex);

            if (string.IsNullOrWhiteSpace(ip) || !string.Equals(regex.Match(ip).Value, ip))
            {
                return num;
            }

            string[] strArray = ip.Split(new char[] { '.' });
            var ipRange = 256;
            var decimalNumbersCount = 4;

            for (var i = 0; i < decimalNumbersCount; i++)
            {
                num += double.Parse(strArray[i]) * Math.Pow(ipRange, decimalNumbersCount - i - 1);
            }

            return num;
        }

        public bool IsVNIP(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
                return false;
            }

            double ipNumber = ConvertIPToNumber(ip);
            var listIPVN = GetIPVNList();

            if (listIPVN == null || !listIPVN.Any())
            {
                return false;
            }

            var isVNIP = listIPVN.Where(ipVN => ipVN.From <= ipNumber && ipNumber <= ipVN.To)
                .Any();

            return isVNIP;
        }

        private bool IsValidIP(string ipAddress)
        {
            IPAddress ip;
            return !string.IsNullOrWhiteSpace(ipAddress)
                && IPAddress.TryParse(ipAddress, out ip);
        }

        private List<IPRange> GetIPVNList()
        {
            var listIPVN = CacheService.Get<List<IPRange>>(VietnamIPListCacheKey);

            if (listIPVN != null
                && listIPVN.Any())
            {
                return listIPVN;
            }

            listIPVN = dynamicRepository.Fetch<IPRange>(new GetIPVNCriteria()).ToList();
            CacheService.Set(VietnamIPListCacheKey, listIPVN, DefaultVariables.CacheItemOptions);

            return listIPVN;
        }
    }
}