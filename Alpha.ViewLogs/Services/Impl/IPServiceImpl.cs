namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Sunplus.Agent.Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    public class IPServiceImpl : IPService
    {
        private const string CacheKeyVietnamIPList = "Alpha.ViewLogs.VietnamIPList";
        private const string IPValidateRegex = @"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)";

        private readonly CachingManager cacheManager;
        private readonly IUserSession userSession;
        private readonly IDynamicRepository dynamicRepository;
        private const int HourToCacheVietnamIPList = 1;

        public IPServiceImpl(
            CachingManager cacheManager,
            IUserSession userSession,
            IDynamicRepository dynamicRepository)
        {
            this.cacheManager = cacheManager;
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

            if (IsVNIP(ip) && !userSession.IsAdminCanViewVNIP)
            {
                return string.Empty;
            }

            var result = $"<a class=\"iplink\" href=\"javascript:OpenIPInfo('{ip}');\">{ip}</a>";
            return result;
        }

        public string FormatCountryCode(string ip, string country)
        {
            Regex regex = new Regex(IPValidateRegex);

            if (!IsValidIP(ip) || !regex.IsMatch(ip, 0))
            {
                return string.Empty;
            }

            if (IsVNIP(ip) && !userSession.IsAdminCanViewVNIP)
            {
                return string.Empty;
            }

            return country;
        }

        private double ConvertIPToNumber(string ip)
        {
            double num = 0.0;

            if (!IsValidIP(ip))
            {
                return num;
            }

            string[] strArray = ip.Split(new char[] { '.' });
            var ipRange = 256;
            var decimalNumbersCount = 4;

            for (var i = 0; i < decimalNumbersCount; i++)
            {
                num += double.Parse(strArray[i]) * Math.Pow(ipRange, decimalNumbersCount - i);
            }

            return num;
        }

        private bool IsValidIP(string ipAddress)
        {
            IPAddress ip;
            return !string.IsNullOrEmpty(ipAddress) && IPAddress.TryParse(ipAddress, out ip);
        }

        private bool IsVNIP(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }

            double ipNumber = ConvertIPToNumber(ip);
            var listIPVN = GetIPVNList();

            if (listIPVN == null)
            {
                return false;
            }

            var isVNIP = listIPVN.Where(ipVN => ipVN.FromAddress <= ipNumber && ipNumber <= ipVN.ToAddress)
                .Any();

            return isVNIP;
        }

        private List<IPRangeEntity> GetIPVNList()
        {
            var listIPVN = cacheManager.Get<List<IPRangeEntity>>(CacheKeyVietnamIPList);

            if (listIPVN != null && listIPVN.Any())
            {
                return listIPVN;
            }

            listIPVN = dynamicRepository.Fetch<IPRangeEntity>(new GetIPVNCriteria()).ToList();
            cacheManager.Add(CacheKeyVietnamIPList, listIPVN, TimeSpan.FromHours(HourToCacheVietnamIPList));

            return listIPVN;
        }
    }
}
