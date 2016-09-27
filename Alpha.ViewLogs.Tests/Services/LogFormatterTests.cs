namespace Alpha.ViewLogs.Tests.Services
{
    using System.Collections.Generic;
    using System.Resources;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using ViewLogs.Services;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class LogFormatterTests
    {
        private const string ValidCountry = "validCountry";

        private IPService ipService;
        private LogFormatterImpl logFormatter;

        private Dictionary<string, bool> sportBetTypeVisibilities;
        [SetUp]
        public void SetUp()
        {
            ipService = Substitute.For<IPService>();
            logFormatter = new LogFormatterImpl(ipService);

            sportBetTypeVisibilities = new Dictionary<string, bool>();
            sportBetTypeVisibilities.Add("validKey", true);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("#")]
        [TestCase("1.1.1.1.1")]
        [TestCase("1")]
        [TestCase("1.1")]
        [TestCase("1.1.1")]
        [TestCase("::1")]
        [TestCase("1.1.1.1")]
        public void FormatIP_AnyIP_CallsIPService(string ip)
        {
            // Action
            logFormatter.FormatIP(ip);

            // Assert
            ipService.Received().FormatIP(Arg.Is<string>(s => s == ip));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("#")]
        [TestCase("1.1.1.1.1")]
        [TestCase("1")]
        [TestCase("1.1")]
        [TestCase("1.1.1")]
        [TestCase("::1")]
        [TestCase("1.1.1.1")]
        public void FormatIP_AnyIP_ReturnsValueFromIPService(string ip)
        {
            // Arrange
            var expectedIP = "validFormattedIP";
            ipService.FormatIP(ip).Returns(expectedIP);

            // Action
            var actualIP = logFormatter.FormatIP(ip);

            // Assert
            Assert.AreEqual(expectedIP, actualIP);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("#")]
        [TestCase("1.1.1.1.1")]
        [TestCase("1")]
        [TestCase("1.1")]
        [TestCase("1.1.1")]
        [TestCase("::1")]
        [TestCase("1.1.1.1")]
        public void FormatCountryCode_AnyIP_CallsIPService(string ip)
        {
            // Action
            logFormatter.FormatCountryCode(ip, ValidCountry);

            // Assert
            ipService.Received().FormatCountryCode(Arg.Is<string>(s => s == ip), Arg.Is<string>(s => s == ValidCountry));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("#")]
        [TestCase("1.1.1.1.1")]
        [TestCase("1")]
        [TestCase("1.1")]
        [TestCase("1.1.1")]
        [TestCase("::1")]
        [TestCase("1.1.1.1")]
        public void FormatCountryCode_AnyIP_ReturnsValueFromIPService(string ip)
        {
            // Arrange
            var inputCountry = ValidCountry;
            var expectedCountry = ValidCountry;
            ipService.FormatCountryCode(ip, ValidCountry).Returns(expectedCountry);

            // Action
            var actualCountry = logFormatter.FormatCountryCode(ip, inputCountry);

            // Assert
            Assert.AreEqual(expectedCountry, actualCountry);
        }

        [TestCase("validKey", true)]
        [TestCase("invalidKey", false)]
        public void IsVisible_AnyKey_ReturnsCorrectIsVisible(string key, bool expectedIsVisible)
        {
            // Arrange
            logFormatter.Init(new TransformParam
            {
                CustStatuses = sportBetTypeVisibilities
            });

            // Action
            var actualIsVisible = logFormatter.IsVisible(key);

            // Assert
            Assert.AreEqual(expectedIsVisible, actualIsVisible);
        }

        [TestCase("validKey")]
        [TestCase("invalidKey")]
        public void GetLogResource_AnyResourceKey_Call(string key)
        {
            // Arrange
            var resourceManager = Substitute.For<ResourceManager>();
            logFormatter.ResourceManager = resourceManager;

            var expectedResource = "validResource";
            resourceManager.GetString(key).Returns(expectedResource);

            // Action
            var actualResource = logFormatter.GetLogResource(key);

            // Assert
            Assert.AreEqual(expectedResource, actualResource);
        }
    }
}
