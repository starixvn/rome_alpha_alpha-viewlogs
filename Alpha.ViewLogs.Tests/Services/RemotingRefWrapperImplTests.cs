namespace Alpha.ViewLogs.Tests.Services
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Constants;
    using NUnit.Framework;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class RemotingRefWrapperImplTests
    {
        private const string ValidBetTypeName = "ValidBetTypeName";
        private const string ValidSportName = "ValidSportName";
        private const string ValidProductName = "ValidProductName";
        private const int ValidBetId = 12;

        private RemotingRefWrapperImpl remotingRefWrapper;

        private Func<string, string, string, string, string> getBetTypeNameWithResourceId;
        private Func<string, string, string> getBetTypeName;
        private Func<string, string, string, string> getSportNameWithDefaultValue;
        private Func<string, string, string, string, string> getResourceDataByRefIdAndResourceIdWithDefaultValue;

        [SetUp]
        public void SetUp()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageCode.English);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            getBetTypeNameWithResourceId = (typeId, language, defaultValue, resourceId) => { return ValidBetTypeName; };

            getBetTypeName = (typeId, language) => { return ValidBetTypeName; };

            getSportNameWithDefaultValue = (sportType, language, defaultValue) => { return ValidSportName; };

            getResourceDataByRefIdAndResourceIdWithDefaultValue = (refId, resourceId, language, defaultValue) => { return ValidProductName; };
        }

        [TestCase(null)]
        [TestCase(1)]
        [TestCase(ValidBetId)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void GetBetType_NullBetTypeId_ReturnsEmpty(object betId)
        {
            // Arrange
            object betTypeId = null;

            remotingRefWrapper = new RemotingRefWrapperImpl();

            // Action
            var actualBetTypeName = remotingRefWrapper.GetBetType(betTypeId, betId);

            // Assert
            Assert.IsEmpty(actualBetTypeName);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void GetBetType_NotNullBetTypeIdAndNullBetId_ReturnsNameFromRemotingRef(object betTypeId)
        {
            // Arrange
            object betId = null;
            var expectedBetTypeName = ValidBetTypeName;

            remotingRefWrapper = new RemotingRefWrapperImpl();
            remotingRefWrapper.SetRemotingRefFunctions(getBetTypeNameWithResourceId);

            // Action
            var actualBetTypeName = remotingRefWrapper.GetBetType(betTypeId, betId);

            // Assert
            Assert.AreEqual(expectedBetTypeName, actualBetTypeName);
        }

        [TestCase(null)]
        [TestCase(1)]
        [TestCase(ValidBetId)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void GetBetType_NotNullBetTypeIdAndBetId_ReturnsNameFromRemotingRef(object betId)
        {
            // Arrange
            object betTypeId = 0;
            var expectedBetTypeName = ValidBetTypeName;

            remotingRefWrapper = new RemotingRefWrapperImpl();
            remotingRefWrapper.SetRemotingRefFunctions(getBetTypeNameWithResourceId);

            // Action
            var actualBetTypeName = remotingRefWrapper.GetBetType(betTypeId, betId);

            // Assert
            Assert.AreEqual(expectedBetTypeName, actualBetTypeName);
        }

        [TestCase(LanguageCode.English)]
        [TestCase("")]
        public void GetBetType_NotNullBetTypeIdAndBetIdByLanguage_ReturnsNameFromRemotingRef(string language)
        {
            // Arrange
            object betTypeId = 0;
            object betId = ValidBetId;
            var expectedBetTypeName = ValidBetTypeName;

            remotingRefWrapper = new RemotingRefWrapperImpl();
            remotingRefWrapper.SetRemotingRefFunctions(getBetTypeNameWithResourceId);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            // Action
            var actualBetTypeName = remotingRefWrapper.GetBetType(betTypeId, betId);

            // Assert
            Assert.AreEqual(expectedBetTypeName, actualBetTypeName);
        }

        [Test]
        public void GetBetTypeName_NullBetType_ReturnsEmpty()
        {
            // Arrange
            object betTypeId = null;

            remotingRefWrapper = new RemotingRefWrapperImpl();
            remotingRefWrapper.SetRemotingRefFunctions(null, getBetTypeName);

            // Action
            var actualBetTypeName = remotingRefWrapper.GetBetTypeName(betTypeId);

            // Assert
            Assert.IsEmpty(actualBetTypeName);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void GetBetTypeName_NotNullBetType_ReturnsNameFromRemotingRef(object betTypeId)
        {
            // Arrange
            var expectedBetTypeName = ValidBetTypeName;

            remotingRefWrapper = new RemotingRefWrapperImpl();
            remotingRefWrapper.SetRemotingRefFunctions(null, getBetTypeName);

            // Action
            var actualBetTypeName = remotingRefWrapper.GetBetTypeName(betTypeId);

            // Assert
            Assert.AreEqual(expectedBetTypeName, actualBetTypeName);
        }

        [Test]
        public void GetSportName_NullSport_ReturnsEmpty()
        {
            // Arrange
            object sportId = null;

            remotingRefWrapper = new RemotingRefWrapperImpl();
            remotingRefWrapper.SetRemotingRefFunctions(null, null, getSportNameWithDefaultValue);

            // Action
            var actualSportName = remotingRefWrapper.GetSportName(sportId);

            // Assert
            Assert.IsEmpty(actualSportName);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void GetSportName_NotNullSport_ReturnsNameFromRemotingRef(object sportId)
        {
            // Arrange
            var expectedSportName = ValidSportName;

            remotingRefWrapper = new RemotingRefWrapperImpl();
            remotingRefWrapper.SetRemotingRefFunctions(null, null, getSportNameWithDefaultValue);

            // Action
            var actualSportName = remotingRefWrapper.GetSportName(sportId);

            // Assert
            Assert.AreEqual(expectedSportName, actualSportName);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void GetSportName_NotNullSport_ReturnsNameFromRemotingRef(int productId)
        {
            // Arrange
            var expectedProductName = ValidProductName;

            remotingRefWrapper = new RemotingRefWrapperImpl();
            remotingRefWrapper.SetRemotingRefFunctions(null, null, null, getResourceDataByRefIdAndResourceIdWithDefaultValue);

            // Action
            var actualProductName = remotingRefWrapper.GetProductName(productId);

            // Assert
            Assert.AreEqual(expectedProductName, actualProductName);
        }
    }
}
