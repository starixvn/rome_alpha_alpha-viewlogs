namespace Alpha.ViewLogs.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Resources;
    using System.Threading;
    using System.Xml;
    using Constants;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
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
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
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

        [Test]
        public void GetBetTypeName_AllBetNormalGamesBetType_ReturnsBetTypeNameFromResource()
        {
            // Arrange
            var expectedBetTypeName = "Baccarat, Roulette, Sic Bo, Dragon Tiger";
            var betType = BetTypeId.AllBetNormalGames;

            // Action
            var actualBetTypeName = logFormatter.GetBetTypeName(betType);

            // Assert
            Assert.AreEqual(expectedBetTypeName, actualBetTypeName);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
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
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
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

        [TestCase("<span></span><span>(00000000)</span>")]
        [TestCase("<span></span><span>(00000001)</span>")]
        [TestCase("<span></span><span>(10000001)</span>")]
        public void TransferConditionFromCode_TwoNodesXMLWithEmptyNewValue_ThrowsArgumentException(string dayCodesWithDiff)
        {
            // Action & Assert
            Assert.Throws<ArgumentException>(new TestDelegate(() => logFormatter.TransferConditionFromCode(dayCodesWithDiff)));
        }

        [TestCase("<span>00000000</span><span>(00000000)</span>", "<span>Daily</span><span>(Daily)</span>")]
        [TestCase("<span>00000000</span><span>(00000001)</span>", "<span>Daily</span><span>(Weekly ())</span>")]
        [TestCase("<span>00000000</span><span>(10000001)</span>", "<span>Daily</span><span>(Weekly (Sun))</span>")]
        [TestCase("<span>00000001</span><span>(00000000)</span>", "<span>Weekly ()</span><span>(Daily)</span>")]
        [TestCase("<span>00000001</span><span>(00000001)</span>", "<span>Weekly ()</span><span>(Weekly ())</span>")]
        [TestCase("<span>00000001</span><span>(10000001)</span>", "<span>Weekly ()</span><span>(Weekly (Sun))</span>")]
        [TestCase("<span>10000001</span><span>(00000000)</span>", "<span>Weekly (Sun)</span><span>(Daily)</span>")]
        [TestCase("<span>10000001</span><span>(00000001)</span>", "<span>Weekly (Sun)</span><span>(Weekly ())</span>")]
        [TestCase("<span>10000001</span><span>(10000001)</span>", "<span>Weekly (Sun)</span><span>(Weekly (Sun))</span>")]
        public void TransferConditionFromCode_TwoNodesXML_ReturnsCorrectTransferCondition(
            string dayCodesWithDiff,
            string expectedTransferCondition)
        {
            // Arrange

            // Action
            var actualTransferCondition = logFormatter.TransferConditionFromCode(dayCodesWithDiff);

            // Assert
            Assert.AreEqual(expectedTransferCondition, actualTransferCondition);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void FormatCodeSite_IsNullOrWhiteSpaceCode_ReturnsEmpty(string code)
        {
            // Action
            var actualCondition = logFormatter.FormatCodeSite(code);

            // Assert
            Assert.IsEmpty(actualCondition);
        }

        [TestCase("-1")]
        [TestCase("3")]
        public void FormatCodeSite_NotSupportedSite_ReturnsEmpty(string code)
        {
            // Action
            var actualCondition = logFormatter.FormatCodeSite(code);

            // Assert
            Assert.IsEmpty(actualCondition);
        }

        [TestCase("1", "Accounting Site")]
        [TestCase("2", "Agent Site")]
        public void FormatCodeSite_SupportedSite_ReturnsCorrectResource(string code, string expectedSite)
        {
            // Arrange
            logFormatter.Init(new TransformParam
            {
                IsShowOldValue = false,
                TrackingValueTransformTemplatePath = string.Empty,
                Sites = new List<string>() { SiteId.Accounting.ToString(), SiteId.Agency.ToString() }
            });

            // Action
            var actualCondition = logFormatter.FormatCodeSite(code);

            // Assert
            Assert.AreEqual(expectedSite, actualCondition);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void FormatErrCode_IsNullOrWhiteSpaceCode_ReturnsNonErrorCode(string code)
        {
            // Arrange
            var expectedCode = "0";

            // Action
            var actualCode = logFormatter.FormatErrCode(code);

            // Assert
            Assert.AreEqual(expectedCode, actualCode);
        }

        [TestCase("0")]
        [TestCase("1")]
        [TestCase("-1")]
        public void FormatErrCode_NotNullOrWhiteSpaceCode_ReturnsErrorCode(string code)
        {
            // Arrange
            var expectedCode = code;

            // Action
            var actualCode = logFormatter.FormatErrCode(code);

            // Assert
            Assert.AreEqual(expectedCode, actualCode);
        }

        [Test]
        public void FormatErrMsg_NoInfoMessage_ReturnsEmpty()
        {
            // Arrange
            var message = "no information is available";

            // Action
            var actualMessage = logFormatter.FormatErrMsg(message);

            // Assert
            Assert.IsEmpty(actualMessage);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("validMessage")]
        [TestCase("valid message with space")]
        public void FormatErrMsg_NotNoInfoMessage_ReturnsInputMessage(string message)
        {
            // Arrange
            var expectedMessage = message;

            // Action
            var actualMessage = logFormatter.FormatErrMsg(message);

            // Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void FormatMemberWinLimit_IsNullOrWhiteSpaceValue_ReturnsEmpty(string value)
        {
            // Action
            var actualMemberWinLimit = logFormatter.FormatMemberWinLimit(value);

            // Assert
            Assert.IsEmpty(actualMemberWinLimit);
        }

        [TestCase("x", "X")]
        [TestCase("X", "X")]
        [TestCase("xx", "XX")]
        [TestCase("xX", "XX")]
        [TestCase("XX", "XX")]
        [TestCase("unlimited", "Unlimited")]
        [TestCase("Unlimited", "Unlimited")]
        [TestCase("unlimitedunlimited", "UnlimitedUnlimited")]
        [TestCase("unlimitedUnlimited", "UnlimitedUnlimited")]
        [TestCase("UnlimitedUnlimited", "UnlimitedUnlimited")]
        [TestCase("xunlimited", "XUnlimited")]
        [TestCase("xUnlimited", "XUnlimited")]
        [TestCase("XUnlimited", "XUnlimited")]
        public void FormatMemberWinLimit_NotNullOrWhiteSpaceValue_ReturnsFormattedValue(
            string value,
            string expectedMemberWinLimit)
        {
            // Action
            var actualMemberWinLimit = logFormatter.FormatMemberWinLimit(value);

            // Assert
            Assert.AreEqual(expectedMemberWinLimit, actualMemberWinLimit);
        }

        [Test]
        public void FormatUnlimited_UnlimitValue_ReturnsUnlimited()
        {
            // Arrange
            var unlimitValue = -1;
            var expectedValue = "Unlimited";

            // Action
            var actualValue = logFormatter.FormatUnlimited(unlimitValue);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(0, "0")]
        [TestCase(1, "1")]
        [TestCase(int.MaxValue, "2147483647")]
        [TestCase(int.MinValue, "-2147483648")]
        public void FormatUnlimited_NotUnlimitValue_ReturnsLimitedValue(
            int limitValue,
            string expectedValue)
        {
            // Action
            var actualValue = logFormatter.FormatUnlimited(limitValue);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("0", true)]
        [TestCase("1", false)]
        [TestCase("-1", false)]
        public void IsVisibleKeno_AnyHideKenoValue_ReturnsCorrectVisible(string hideKeno, bool expectedVisible)
        {
            // Action
            var actualVisible = logFormatter.IsVisibleKeno(hideKeno);

            // Assert
            Assert.AreEqual(expectedVisible, actualVisible);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("  ")]
        [TestCase("0")]
        [TestCase("1")]
        [TestCase("-1")]
        public void GetDisabledKenoBetTypeName_AnyBetType_ReturnsCorrectName(string disableBetType)
        {
            // Arrange
            var kenoBetTypeName = "valieKenoBetTypeName";
            remotingRef.GetBetTypeName(disableBetType).Returns(kenoBetTypeName);
            var expectedDisabledKenoBetTypeName = "Disabled " + kenoBetTypeName;

            // Action
            var actualDisabledKenoBetTypeName = logFormatter.GetDisabledKenoBetTypeName(disableBetType);

            // Assert
            Assert.AreEqual(expectedDisabledKenoBetTypeName, actualDisabledKenoBetTypeName);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsShowMyanmarOdds_NoInput_Returns(bool isShowMyanmarOdds)
        {
            // Arrange
            var expectedIsShowMyanmarOdds = isShowMyanmarOdds;
            logFormatter.Init(new TransformParam
            {
                IsShowMyanmarOdds = isShowMyanmarOdds
            });

            // Action
            var actualIsShowMyanmarOdds = logFormatter.IsShowMyanmarOdds();

            // Assert
            Assert.AreEqual(expectedIsShowMyanmarOdds, actualIsShowMyanmarOdds);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void IsRNGCasino_NotRNGCasino_ReturnsFalse(int sportType)
        {
            // Action
            var actualIsRNGCasino = logFormatter.IsRNGCasino(sportType);

            // Assert
            Assert.IsFalse(actualIsRNGCasino);
        }

        [TestCase(SportId.RNGGame)]
        [TestCase(SportId.RNGMiniGame)]
        [TestCase(SportId.RNGMobile)]
        public void IsRNGCasino_RNGCasino_ReturnsTrue(int sportType)
        {
            // Action
            var actualIsRNGCasino = logFormatter.IsRNGCasino(sportType);

            // Assert
            Assert.IsTrue(actualIsRNGCasino);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void IsGoldDeluxe_NotGoldDeluxe_ReturnsFalse(int sportType)
        {
            // Action
            var actualIsGoldDeluxe = logFormatter.IsGoldDeluxe(sportType);

            // Assert
            Assert.IsFalse(actualIsGoldDeluxe);
        }

        [TestCase(SportId.GoldDeluxe)]
        public void IsGoldDeluxe_GoldDeluxe_ReturnsTrue(int sportType)
        {
            // Action
            var actualIsGoldDeluxe = logFormatter.IsGoldDeluxe(sportType);

            // Assert
            Assert.IsTrue(actualIsGoldDeluxe);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void RenderCustStatus_IsNullOrWhiteSpaceName_ReturnsEmpty(string name)
        {
            // Action
            var actualCustStatus = logFormatter.RenderCustStatus(name, Arg.Any<string>());

            // Assert
            Assert.IsEmpty(actualCustStatus);
        }

        [TestCase("validStatus")]
        public void RenderCustStatus_ResourcesByBetTypeCacheRef_ReturnsCorrectStatus(string name)
        {
            // Arrange
            var statusValue = "anyValue";

            var validBetType = 1;
            var validBetTypeName = "validBetTypeName";
            var resourcesByBetTypeCacheRef = new Dictionary<string, int>();
            resourcesByBetTypeCacheRef.Add(name.ToLower(), validBetType);

            remotingRef.GetBetType(validBetType).Returns(validBetTypeName);

            logFormatter.Init(new TransformParam
            {
                ResourcesByBetTypeCacheRef = resourcesByBetTypeCacheRef
            });

            var expectedCustStatus = $"<tr><td colspan=\"3\" class=\"colW1\">{validBetTypeName}</td><td colspan=\"3\" class=\"colW2\">{statusValue}</td></tr>";

            // Action
            var actualCustStatus = logFormatter.RenderCustStatus(name, statusValue);

            // Assert
            Assert.AreEqual(expectedCustStatus, actualCustStatus);
        }

        [TestCase("validStatus", true, "<tr><td colspan=\"3\" class=\"colW1\">validStatusName</td><td colspan=\"3\" class=\"colW2\">anyValue</td></tr>")]
        [TestCase("validStatus", false, "")]
        public void RenderCustStatus_HasConfigResourcesByBetTypeCacheRefAndUseResourceFromCode_ReturnsCorrectStatus(
            string name,
            bool isVisible,
            string expectedCustStatus)
        {
            // Arrange
            var statusValue = "anyValue";

            var validBetType = 1;
            var resourcesByBetTypeCacheRef = new Dictionary<string, int>();
            resourcesByBetTypeCacheRef.Add("betTypeStatus", validBetType);

            var custStatuses = new Dictionary<string, bool>();
            custStatuses.Add(name.ToLower(), isVisible);

            var validStatusName = "validStatusName";
            logFormatter.ResourceManager.GetString(name).Returns(validStatusName);

            logFormatter.Init(new TransformParam
            {
                ResourcesByBetTypeCacheRef = resourcesByBetTypeCacheRef,
                CustStatuses = custStatuses
            });

            // Action
            var actualCustStatus = logFormatter.RenderCustStatus(name, statusValue);

            // Assert
            Assert.AreEqual(expectedCustStatus, actualCustStatus);
        }

        [TestCase("validStatus", true, "<tr><td colspan=\"3\" class=\"colW1\">validStatusName</td><td colspan=\"3\" class=\"colW2\">anyValue</td></tr>")]
        [TestCase("validStatus", false, "")]
        public void RenderCustStatus_NotConfigResourcesByBetTypeCacheRefAndUseResourceFromCode_ReturnsCorrectStatus(
            string name,
            bool isVisible,
            string expectedCustStatus)
        {
            // Arrange
            var statusValue = "anyValue";

            var custStatuses = new Dictionary<string, bool>();
            custStatuses.Add(name.ToLower(), isVisible);

            var validStatusName = "validStatusName";
            logFormatter.ResourceManager.GetString(name).Returns(validStatusName);

            logFormatter.Init(new TransformParam
            {
                CustStatuses = custStatuses
            });

            // Action
            var actualCustStatus = logFormatter.RenderCustStatus(name, statusValue);

            // Assert
            Assert.AreEqual(expectedCustStatus, actualCustStatus);
        }
    }
}
