namespace Alpha.ViewLogs.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using Criteria;
    using DataAccesses;
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

        private CachingManager cacheManager;
        private IDynamicRepository dynamicRepository;
        private CustomerService customerService;
        private LogDataAccess logDataAccess;

        [SetUp]
        public void SetUp()
        {
            cacheManager = Substitute.For<CachingManager>();
            dynamicRepository = Substitute.For<IDynamicRepository>();
            customerService = Substitute.For<CustomerService>();
            logDataAccess = Substitute.For<LogDataAccess>();

            baseService = new BaseServiceForTesting(cacheManager, dynamicRepository, customerService, logDataAccess);
        }

        [Test]
        public void GetLogActionTypes_NoCondition_CallCacheToGetData()
        {
            // Action
            baseService.GetLogActionTypes();

            // Assert
            cacheManager.Received().Get<IEnumerable<LogActionType>>(LogActionTypesCacheKey);
        }

        [Test]
        public void GetLogActionTypes_HasDataInCache_ReturnsDataInCache()
        {
            // Arrange
            var logActionTypesInCache = new List<LogActionType>();
            cacheManager.Get<IEnumerable<LogActionType>>(LogActionTypesCacheKey).Returns(logActionTypesInCache);

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
            cacheManager.Get<IEnumerable<LogActionType>>(LogActionTypesCacheKey).Returns(logActionTypesInCache);

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
            cacheManager.Get<IEnumerable<LogActionType>>(LogActionTypesCacheKey).Returns(logActionTypesInCache);

            var expectedLogActionTypes = new List<LogActionType>();
            dynamicRepository.Fetch<LogActionType>(Arg.Any<GetAllLogActionTypeCriteria>()).Returns(expectedLogActionTypes);

            // Action
            var actualActionTypes = baseService.GetLogActionTypes();

            // Assert
            cacheManager.Received().Add(LogActionTypesCacheKey, expectedLogActionTypes);
        }

        [Test]
        public void GetLogActionTypes_NoDataInCache_ReturnDataFromDataAccess()
        {
            // Arrange
            List<LogActionType> logActionTypesInCache = null;
            cacheManager.Get<IEnumerable<LogActionType>>(LogActionTypesCacheKey).Returns(logActionTypesInCache);

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
            var pagingLogs = baseService.GetPagingLogs(nullLogParam);

            // Assert
            Assert.AreEqual(emptyPagingLogs, pagingLogs);
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
    }
}
