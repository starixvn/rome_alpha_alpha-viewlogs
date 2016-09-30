namespace Alpha.ViewLogs.Tests.Services
{
    using System.Collections.Generic;
    using Criteria;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using NSubstitute;
    using NUnit.Framework;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Models;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class IPServiceImplTests
    {
        private const string VietnamIPListCacheKey = "Alpha.ViewLogs.VietnamIPList";
        private const string ValidCountry = "validCountry";

        private IUserSession userSession;
        private IDynamicRepository dynamicRepository;
        private IPServiceImpl ipService;

        private Cache cacheForTesting;

        private List<IPRange> VNIPs;

        [SetUp]
        public void SetUp()
        {
            cacheForTesting = Substitute.For<Cache>();
            CacheService.SwitchCacheStorage(cacheForTesting);

            dynamicRepository = Substitute.For<IDynamicRepository>();
            userSession = Substitute.For<IUserSession>();

            ipService = new IPServiceImpl(userSession, dynamicRepository);

            VNIPs = new List<IPRange>()
            {
                new IPRange(
                    16843008, // 1.1.1.0 = 256^3 + 256^2 + 256^1
                    16843263, // 1.1.1.255 = 256^3 + 256^2 + 256^1 + 255
                    false)
            };
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("#")]
        [TestCase("1.1.1.1.1")]
        public void FormatIP_InvalidIP_ReturnsInputIP(string ip)
        {
            // Arrange
            var expectedIP = ip;

            // Action
            var actualIP = ipService.FormatIP(ip);

            // Assert
            Assert.AreEqual(expectedIP, actualIP);
        }

        [TestCase("1")]
        [TestCase("1.1")]
        [TestCase("1.1.1")]
        [TestCase("::1")]
        public void FormatIP_ValidIPButNotMatchRegex_ReturnsIPLinkWithoutOpenIPInfo(string ip)
        {
            // Arrange
            var expectedIP = $"<a class=\"iplink\" href=\"javascript:;\">{ip}</a>";

            // Action
            var actualIP = ipService.FormatIP(ip);

            // Assert
            Assert.AreEqual(expectedIP, actualIP);
        }

        [TestCase("1.1.1.1", true, "<a class=\"iplink\" href=\"javascript:OpenIPInfo('1.1.1.1');\">1.1.1.1</a>")]
        [TestCase("1.1.1.1", false, "")]
        public void FormatIP_IsVNIP_ReturnsCorrectIP(string ip, bool canViewVNIP, string expectedIP)
        {
            // Arrange
            userSession.IsAdminCanViewVNIP.Returns(canViewVNIP);

            List<IPRange> ipVNList;
            cacheForTesting.TryGet(VietnamIPListCacheKey, out ipVNList).Returns(x =>
            {
                x[1] = VNIPs;

                return true;
            });

            // Action
            var actualIP = ipService.FormatIP(ip);

            // Assert
            Assert.AreEqual(expectedIP, actualIP);
        }

        [TestCase("1.1.1.1", true, "<a class=\"iplink\" href=\"javascript:OpenIPInfo('1.1.1.1');\">1.1.1.1</a>")]
        [TestCase("1.1.1.1", false, "<a class=\"iplink\" href=\"javascript:OpenIPInfo('1.1.1.1');\">1.1.1.1</a>")]
        public void FormatIP_MatchIPValidationAndNotVNIP_ReturnsCorrectIP(string ip, bool canViewVNIP, string expectedIP)
        {  // Arrange
            userSession.IsAdminCanViewVNIP.Returns(canViewVNIP);

            var ipVNList = new List<IPRange>();
            cacheForTesting.TryGet(VietnamIPListCacheKey, out ipVNList).Returns(false);
            dynamicRepository.Fetch<IPRange>(Arg.Any<GetIPVNCriteria>()).Returns(ipVNList);

            // Action
            var actualIP = ipService.FormatIP(ip);

            // Assert
            Assert.AreEqual(expectedIP, actualIP);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void ConvertIPToNumber_InvalidIP_ReturnsZero(string ip)
        {
            // Arrange
            var expectedValue = 0;

            // Action
            var actualValue = ipService.ConvertIPToNumber(ip);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("1.1.1.0", 16843008)]
        [TestCase("1.1.1.1", 16843009)]
        [TestCase("1.1.1.255", 16843263)]
        public void ConvertIPToNumber_ValidIP_ReturnsNumberOfIP(string ip, double expectedValue)
        {
            // Action
            var actualValue = ipService.ConvertIPToNumber(ip);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void IsVNIP_NullOrEmptyIP_ReturnsFalse(string ip)
        {
            // Arrange
            var expectedValue = false;

            // Action
            var isVNIP = ipService.IsVNIP(ip);

            // Assert
            Assert.AreEqual(expectedValue, isVNIP);
        }

        [TestCase("#")]
        [TestCase("1.1.1.1.1")]
        [TestCase("1")]
        [TestCase("1.1")]
        [TestCase("1.1.1")]
        [TestCase("::1")]
        [TestCase("1.1.0.1")]
        [TestCase("1.1.2.1")]
        public void IsVNIP_NotVNIP_ReturnsFalse(string ip)
        {
            // Arrange
            var expectedValue = false;

            List<IPRange> ipVNList;
            cacheForTesting.TryGet(VietnamIPListCacheKey, out ipVNList).Returns(x =>
            {
                x[1] = VNIPs;

                return true;
            });

            // Action
            var isVNIP = ipService.IsVNIP(ip);

            // Assert
            Assert.AreEqual(expectedValue, isVNIP);
        }

        [TestCase("1.1.1.0")]
        [TestCase("1.1.1.1")]
        [TestCase("1.1.1.255")]
        public void IsVNIP_VNIP_ReturnsTrue(string ip)
        {
            // Arrange
            var expectedValue = true;

            List<IPRange> ipVNList;
            cacheForTesting.TryGet(VietnamIPListCacheKey, out ipVNList).Returns(x =>
            {
                x[1] = VNIPs;

                return true;
            });

            // Action
            var isVNIP = ipService.IsVNIP(ip);

            // Assert
            Assert.AreEqual(expectedValue, isVNIP);
        }

        [TestCase("1.1.1.0")]
        [TestCase("1.1.1.1")]
        [TestCase("1.1.1.255")]
        public void IsVNIP_CheckValidIPWithNoDataForVNIPInCache_GetVNIPsFromDatabase(string ip)
        {
            // Arrange
            List<IPRange> ipVNList;
            cacheForTesting.TryGet(VietnamIPListCacheKey, out ipVNList).Returns(x =>
            {
                x[1] = new List<IPRange>();

                return true;
            });

            // Action 
            ipService.IsVNIP(ip);

            // Assert
            dynamicRepository.Received().Fetch<IPRange>(Arg.Any<GetIPVNCriteria>());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("#")]
        [TestCase("1.1.1.1.1")]
        public void FormatCountryCode_InvalidIP_ReturnsEmpty(string ip)
        {
            // Arrange
            var country = ValidCountry;

            // Action
            var actualCountry = ipService.FormatCountryCode(ip, country);

            // Assert
            Assert.IsEmpty(actualCountry);
        }

        [TestCase("1.1.1.0")]
        [TestCase("1.1.1.1")]
        [TestCase("1.1.1.255")]
        public void FormatCountryCode_VNIPAndUserCannotViewVNIP_ReturnsEmpty(string ip)
        {
            // Arrange
            var country = ValidCountry;

            List<IPRange> ipVNList;
            cacheForTesting.TryGet(VietnamIPListCacheKey, out ipVNList).Returns(x =>
            {
                x[1] = VNIPs;

                return true;
            });

            userSession.IsAdminCanViewVNIP.Returns(false);

            // Action
            var actualCountry = ipService.FormatCountryCode(ip, country);

            // Assert
            Assert.IsEmpty(actualCountry);
        }

        [TestCase("1.1.1.0", ValidCountry)]
        [TestCase("1.1.1.1", ValidCountry)]
        [TestCase("1.1.1.255", ValidCountry)]
        public void FormatCountryCode_VNIPAndUserCanViewVNIP_ReturnsCountry(string ip, string country)
        {
            // Arrange
            var expectedCountry = country;

            List<IPRange> ipVNList;
            cacheForTesting.TryGet(VietnamIPListCacheKey, out ipVNList).Returns(x =>
            {
                x[1] = VNIPs;

                return true;
            });

            userSession.IsAdminCanViewVNIP.Returns(true);

            // Action
            var actualCountry = ipService.FormatCountryCode(ip, country);

            // Assert
            Assert.AreEqual(expectedCountry, actualCountry);
        }

        [TestCase("2.1.1.0", true, ValidCountry)]
        [TestCase("2.1.1.1", true, ValidCountry)]
        [TestCase("2.1.1.255", true, ValidCountry)]
        [TestCase("2.1.1.0", false, ValidCountry)]
        [TestCase("2.1.1.1", false, ValidCountry)]
        [TestCase("2.1.1.255", false, ValidCountry)]
        public void FormatCountryCode_ValidIPButNotVNIP_ReturnsCountry(string ip, bool canViewVNIP, string country)
        {
            // Arrange
            var expectedCountry = country;

            List<IPRange> ipVNList;
            cacheForTesting.TryGet(VietnamIPListCacheKey, out ipVNList).Returns(x =>
            {
                x[1] = VNIPs;

                return true;
            });

            userSession.IsAdminCanViewVNIP.Returns(canViewVNIP);

            // Action
            var actualCountry = ipService.FormatCountryCode(ip, country);

            // Assert
            Assert.AreEqual(expectedCountry, actualCountry);
        }
    }
}
