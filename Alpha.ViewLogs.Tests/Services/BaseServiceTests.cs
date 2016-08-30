namespace Alpha.ViewLogs.Tests.Services
{
    using System.Collections.Generic;
    using Constants;
    using Criteria;
    using DataAccesses;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using ViewLogs.Services;

    [TestFixture]
    public class BaseServiceTests
    {
        private const string LogActionCacheKey = "Alpha.ViewLogs.CustomerSetting.LogAction";
        private const string LogActionTypesCacheKey = "Alpha.ViewLogs.CustomerSetting.LogActionTypes";

        private BaseServiceForTesting baseService;

        private IDynamicRepository dynamicRepository;
        private CustomerService customerService;
        private LogDataAccess logDataAccess;

        private Cache cacheForTesting;

        [SetUp]
        public void SetUp()
        {
            cacheForTesting = Substitute.For<Cache>();

            dynamicRepository = Substitute.For<IDynamicRepository>();
            customerService = Substitute.For<CustomerService>();
            logDataAccess = Substitute.For<LogDataAccess>();

            baseService = new BaseServiceForTesting(dynamicRepository, customerService, logDataAccess);
            CacheService.SwitchCacheStorage(cacheForTesting);
        }

        [Test]
        public void GetLogActionTypes_NoCondition_CallCacheToGetData()
        {
            // Arrange
            IEnumerable<LogActionType> cacheData;

            // Action
            baseService.GetLogActionTypes();

            // Assert
            cacheForTesting.Received().TryGet(LogActionTypesCacheKey, out cacheData);
        }

        [Test]
        public void GetLogActionTypes_HasDataInCache_ReturnsDataInCache()
        {
            // Arrange
            var logActionTypesInCache = new List<LogActionType>();
            cacheForTesting.TryGet(LogActionTypesCacheKey, out logActionTypesInCache).Returns(true);

            // Action
            var actualLogActionTypes = baseService.GetLogActionTypes();

            // Assert
            Assert.AreEqual(logActionTypesInCache, actualLogActionTypes);
        }

        [Test]
        public void GetLogActionTypes_NoDataInCache_CallDataAccessToGetData()
        {
            // Arrange
            List<LogActionType> logActionTypesInCache = null;
            cacheForTesting.TryGet(LogActionTypesCacheKey, out logActionTypesInCache).Returns(false);

            // Action
            baseService.GetLogActionTypes();

            // Assert
            dynamicRepository.Received().Fetch<LogActionType>(Arg.Any<GetAllLogActionTypeCriteria>());
        }

        [Test]
        public void GetLogActionTypes_NoDataInCache_StoreDataToCache()
        {
            // Arrange
            List<LogActionType> logActionTypesInCache = null;
            cacheForTesting.TryGet(LogActionTypesCacheKey, out logActionTypesInCache).Returns(false);

            var expectedLogActionTypes = new List<LogActionType>();
            dynamicRepository.Fetch<LogActionType>(Arg.Any<GetAllLogActionTypeCriteria>()).Returns(expectedLogActionTypes);

            // Action
            var actualActionTypes = baseService.GetLogActionTypes();

            // Assert
            cacheForTesting.Received().Set(LogActionTypesCacheKey, expectedLogActionTypes, Arg.Any<CacheItemOptions>());
        }

        [Test]
        public void GetLogActionTypes_NoDataInCache_ReturnDataFromDataAccess()
        {
            // Arrange
            List<LogActionType> logActionTypesInCache = null;
            cacheForTesting.TryGet(LogActionTypesCacheKey, out logActionTypesInCache).Returns(false);

            var expectedLogActionTypes = new List<LogActionType>();
            dynamicRepository.Fetch<LogActionType>(Arg.Any<GetAllLogActionTypeCriteria>()).Returns(expectedLogActionTypes);

            // Action
            var actualActionTypes = baseService.GetLogActionTypes();

            // Assert
            Assert.AreEqual(expectedLogActionTypes, actualActionTypes);
        }

        [Test]
        public void GetPagingLogs_NullLogParam_ReturnsEmptyPagingLogs()
        {
            // Arrange
            LogParam nullLogParam = null;
            var emptyPagingLogs = new PagingLogs<Log>();

            // Action
            var actualPagingLogs = baseService.GetPagingLogs(nullLogParam);

            // Assert
            Assert.AreEqual(emptyPagingLogs, actualPagingLogs);
        }

        [Test]
        public void GetPagingLogs_NotNullLogParam_CallGetDownlineInfoByUsername()
        {
            // Arrange
            LogParam logParam = new LogParam();
            var downlineInfo = new CustomerInfo();
            customerService.GetDownlineInfoByUsername(logParam.Username, logParam.UplineId).Returns(downlineInfo);

            // Action
            baseService.GetPagingLogs(logParam);

            // Assert
            customerService.Received().GetDownlineInfoByUsername(logParam.Username, logParam.UplineId);
        }

        [Test]
        public void GetPagingLogs_InvalidDownline_ReturnsEmptyPagingLogs()
        {
            // Arrange
            LogParam logParam = new LogParam();

            var invalidDownlineInfo = new CustomerInfo
            {
                CustId = 0
            };

            customerService.GetDownlineInfoByUsername(logParam.Username, logParam.UplineId).Returns(invalidDownlineInfo);

            var emptyPagingLogs = new PagingLogs<Log>();

            // Action
            var actualPagingLogs = baseService.GetPagingLogs(logParam);

            // Assert
            Assert.AreEqual(emptyPagingLogs, actualPagingLogs);
        }

        [Test]
        public void GetPagingLogs_ValidDownline_ReturnsDataFromDataAccess()
        {
            // Arrange
            LogParam logParam = new LogParam
            {
                Language = LanguageCode.English
            };

            var validDownlineInfo = new CustomerInfo
            {
                CustId = 1
            };

            customerService.GetDownlineInfoByUsername(logParam.Username, logParam.UplineId).Returns(validDownlineInfo);

            var logs = new List<Log>
            {
                new Log { ActionId = 1 }
            };

            dynamicRepository.Fetch<Log>(Arg.Any<ICriteria>()).Returns(logs);

            Dictionary<int, LogAction> logActions;

            cacheForTesting.TryGet(LogActionCacheKey, out logActions).Returns(
                x =>
                {
                    x[1] = new Dictionary<int, LogAction>()
                    {
                        { 1, new LogAction() { EnglishName = "Action 1" } }
                    };

                    return true;
                });

            var expectedPagingLogs = new PagingLogs<Log>
            {
                Logs = new List<Log>
                {
                    new Log { ActionId = 1, ActionName = "Action 1" }
                }
            };

            // Action
            var actualPagingLogs = baseService.GetPagingLogs(logParam);

            // Assert
            Assert.AreEqual(expectedPagingLogs, actualPagingLogs);
        }
    }
}
