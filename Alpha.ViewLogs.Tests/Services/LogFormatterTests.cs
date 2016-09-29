namespace Alpha.ViewLogs.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Resources;
    using System.Threading;
    using System.Xml;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using ViewLogs.Constants;
    using ViewLogs.Services;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class LogFormatterTests
    {
        private const string ValidCountry = "validCountry";

        private const string ValidResourceKey = "validKey";
        private const string LowerCaseValidResourceKey = "validkey";
        private const string InvalidResourceKey = "invalidKey";

        private IPService ipService;
        private RemotingRefWrapper remotingRef;
        private LogFormatterImpl logFormatter;

        private Dictionary<string, bool> sportBetTypeVisibilities;
        [SetUp]
        public void SetUp()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageCode.English);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            ipService = Substitute.For<IPService>();
            remotingRef = Substitute.For<RemotingRefWrapper>();

            logFormatter = new LogFormatterImpl(ipService, remotingRef);
            logFormatter.Init(null);

            sportBetTypeVisibilities = new Dictionary<string, bool>();
            sportBetTypeVisibilities.Add("validKey", true);

            var resourceManager = Substitute.For<ResourceManager>();
            logFormatter.ResourceManager = resourceManager;

            resourceManager.GetString(Arg.Is<string>(s => string.Equals(s, ValidResourceKey, StringComparison.OrdinalIgnoreCase))).Returns("validResource");
            resourceManager.GetString(Arg.Is<string>(s => string.Equals(s, InvalidResourceKey, StringComparison.OrdinalIgnoreCase))).Returns((string)null);
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

        [TestCase(ValidResourceKey, "validResource")]
        [TestCase(LowerCaseValidResourceKey, "validResource")]
        [TestCase(InvalidResourceKey, "")]
        public void GetLogResource_AnyResourceKey_ReturnsCorrectResource(string key, string expectedResource)
        {
            // Action
            var actualResource = logFormatter.GetLogResource(key);

            // Assert
            Assert.AreEqual(expectedResource, actualResource);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        public void GetBetTypeName_AnyBetType_ReturnsBetTypeNameFromCacheRef(int betType)
        {
            // Arrange
            var expectedBetTypeName = "validBetTypeName";
            remotingRef.GetBetType(betType).Returns(expectedBetTypeName);

            // Action
            var actualBetTypeName = logFormatter.GetBetTypeName(betType);

            // Assert
            Assert.AreEqual(expectedBetTypeName, actualBetTypeName);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        public void GetSportName_AnySportType_ReturnsSportTypeNameFromCacheRef(int sportType)
        {
            // Arrange
            var expectedSportTypeName = "validSportTypeName";
            remotingRef.GetSportName(sportType).Returns(expectedSportTypeName);

            // Action
            var actualSportTypeName = logFormatter.GetSportName(sportType);

            // Assert
            Assert.AreEqual(expectedSportTypeName, actualSportTypeName);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        public void GetProductName_AnyProductId_ReturnsProductNameFromCacheRef(int productId)
        {
            // Arrange
            var expectedProductName = "validProductName";
            remotingRef.GetProductName(productId).Returns(expectedProductName);

            // Action
            var actualProductName = logFormatter.GetProductName(productId);

            // Assert
            Assert.AreEqual(expectedProductName, actualProductName);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void TransferConditionFromCode_NullOrWhiteSpaceDayCodesWithDiff_ReturnsEmpty(string dayCodesWithDiff)
        {
            // Arrange
            var expectedTransferCondition = string.Empty;

            // Action
            var actualTransferCondition = logFormatter.TransferConditionFromCode(dayCodesWithDiff);

            // Assert
            Assert.AreEqual(expectedTransferCondition, actualTransferCondition);
        }

        [TestCase("<></>")]
        [TestCase("<div>")]
        [TestCase("</div>")]
        [TestCase("<div></tag>")]
        [TestCase("<div>abc</tag>")]
        public void TransferConditionFromCode_InvalidXMLFormatDayCodesWithDiff_ThrowsXmlException(string dayCodesWithDiff)
        {
            // Action & Assert
            Assert.Throws<XmlException>(new TestDelegate(() => logFormatter.TransferConditionFromCode(dayCodesWithDiff)));
        }

        [TestCase("a", "")]
        [TestCase("1", "")]
        [TestCase("00000000", "Daily")]
        public void TransferConditionFromCode_NonWeeklyDayCode_ReturnsCorrectTransferCondition(
            string dayCodesWithDiff,
            string expectedTransferCondition)
        {
            // Action
            var actualTransferCondition = logFormatter.TransferConditionFromCode(dayCodesWithDiff);

            // Assert
            Assert.AreEqual(expectedTransferCondition, actualTransferCondition);
        }

        [TestCase("00000001", "")]
        [TestCase("10000001", "Sun")]
        [TestCase("a0000001", "")]

        [TestCase("01000001", "Mon")]
        [TestCase("00100001", "Tue")]
        [TestCase("00010001", "Wed")]
        [TestCase("00001001", "Thurs")]
        [TestCase("00000101", "Fri")]
        [TestCase("00000011", "Sat")]
        [TestCase("10000011", "Sun - Sat")]
        [TestCase("a0000011", "Sat")]

        [TestCase("11111111", "Sun - Mon - Tue - Wed - Thurs - Fri - Sat")]
        [TestCase("111111111", "Sun - Mon - Tue - Wed - Thurs - Fri - Sat")]
        public void TransferConditionFromCode_WeeklyDayCode_ReturnsCorrectTransferCondition(
            string dayCodesWithDiff,
            string selectedDays)
        {
            // Arrange
            var expectedTransferCondition = "Weekly (" + selectedDays + ")";

            // Action
            var actualTransferCondition = logFormatter.TransferConditionFromCode(dayCodesWithDiff);

            // Assert
            Assert.AreEqual(expectedTransferCondition, actualTransferCondition);
        }

        [TestCase("<span></span>")]
        [TestCase("<span name=\"newvalue\"></span>")]
        public void TransferConditionFromCode_OneNodeXMLWithoutTitleAttribute_ThrowsNullReferenceException(string dayCodesWithDiff)
        {
            // Action & Assert
            Assert.Throws<NullReferenceException>(new TestDelegate(() => logFormatter.TransferConditionFromCode(dayCodesWithDiff)));
        }

        [TestCase("<span title=\"\"></span>")]
        [TestCase("<span title=\"a\"></span>")]
        [TestCase("<span title=\"1\"></span>")]
        [TestCase("<span title=\"00000000\"></span>")]
        [TestCase("<span title=\"\">00000000</span>")]
        public void TransferConditionFromCode_OneNodeXMLHasTitleAttributeAndOneValueEmpty_ThrowsArgumentException(string dayCodesWithDiff)
        {
            // Action & Assert
            Assert.Throws<ArgumentException>(new TestDelegate(() => logFormatter.TransferConditionFromCode(dayCodesWithDiff)));
        }

        [TestCase("<span title=\"00000000-->\">00000000</span>", "<span title=\"Daily-->\">Daily</span>")]
        [TestCase("<span title=\"00000001-->\">00000001</span>", "<span title=\"Weekly ()-->\">Weekly ()</span>")]
        [TestCase("<span title=\"00000000-->00000000\">00000000</span>", "<span title=\"Daily-->Daily\">Daily</span>")]
        [TestCase("<span title=\"00000001-->00000001\">00000001</span>", "<span title=\"Weekly ()-->Weekly ()\">Weekly ()</span>")]
        public void TransferConditionFromCode_OneNodeXMLWithTitleAttribute_ReturnsCorrectTransferCondition(
            string dayCodesWithDiff,
            string expectedTransferCondition)
        {
            // Action
            var actualTransferCondition = logFormatter.TransferConditionFromCode(dayCodesWithDiff);

            // Assert
            Assert.AreEqual(expectedTransferCondition, actualTransferCondition);
        }

        /// <summary>
        /// Old Value must:
        ///     -Start with (
        ///     -Have value length >= 8 (1 ( + 7 days)
        /// </summary>
        [TestCase("<span></span><span></span>")]
        [TestCase("<span></span><span>(</span>")]
        [TestCase("<span></span><span>1</span>")]
        [TestCase("<span></span><span>(1</span>")]
        public void TransferConditionFromCode_TwoNodesXMLAndWrongOldValue_ThrowsArgumentOutOfRangeException(string dayCodesWithDiff)
        {
            // Action & Assert
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => logFormatter.TransferConditionFromCode(dayCodesWithDiff)));
        }

        public void TransferConditionFromCode_TwoNodesXML_ReturnsCorrectTransferCondition(
            string dayCodesWithDiff,
            string expectedTransferCondition)
        {
        }
    }
}
