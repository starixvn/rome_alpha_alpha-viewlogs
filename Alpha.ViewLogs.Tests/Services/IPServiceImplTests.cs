namespace Alpha.ViewLogs.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class IPServiceImplTests
    {
        private const string VietnamIPListCacheKey = "Alpha.ViewLogs.VietnamIPList";

        private IUserSession userSession;
        private IDynamicRepository dynamicRepository;
        private IPServiceImpl ipService;

        private Cache cacheForTesting;

        [SetUp]
        public void SetUp()
        {
            cacheForTesting = Substitute.For<Cache>();
            CacheService.SwitchCacheStorage(cacheForTesting);

            dynamicRepository = Substitute.For<IDynamicRepository>();
            userSession = Substitute.For<IUserSession>();

            ipService = new IPServiceImpl(userSession, dynamicRepository);
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
                x[1] = new List<IPRange>()
                {
                    new IPRange
                    {
                        From = Math.Pow(256,3) + Math.Pow(256,2) + Math.Pow(256,1),
                        To =  Math.Pow(256,3) + Math.Pow(256,2) + Math.Pow(256,1) + 255
                    }
                };

                return true;
            });

            // Action
            var actualIP = ipService.FormatIP(ip);

            // Assert
            Assert.AreEqual(expectedIP, actualIP);
        }
    }
}
