namespace Alpha.ViewLogs.Tests.Services
{
    using Criteria.CustomerSetting;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Models.CustomerSetting;
    using NSubstitute;
    using NUnit.Framework;
    using Params.CustomerSetting;
    using System.Collections.Generic;
    using ViewLogs.Services;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class CustomerSettingLogServiceImplTest
    {
        private const string CacheKeyLogActionTypes = "Alpha.ViewLogs.CustomerSetting.LogActionTypes";
        private const string CacheKeyTransformObject = "Alpha.ViewLogs.CustomerSetting.TransformObject";

        private CustomerService customerService;
        private BaseDataAccess baseDataAccess;
        private CachingManager cacheManager;
        private IDynamicRepository dynamicRepository;
        private CustomerSettingServiceImpl customerSettingLog;
        private BaseService baseLogService;

        [SetUp]
        public void SetUp()
        {
            customerService = Substitute.For<CustomerService>();
            baseDataAccess = Substitute.For<BaseDataAccess>();
            cacheManager = Substitute.For<CachingManager>();
            dynamicRepository = Substitute.For<IDynamicRepository>();
            baseLogService = Substitute.For<BaseService>();

            customerSettingLog = new CustomerSettingServiceImpl(customerService, baseDataAccess, cacheManager, dynamicRepository, baseLogService);
        }

        [Test]
        public void GetLogActionTypes_HasDataInCache_ReturnDataInCache()
        {
            // Arrange
            var validActionTypeList = GetValidActionTypeList();
            cacheManager.Get<List<LogActionType>>(CacheKeyLogActionTypes).Returns(validActionTypeList);

            // Action
            var actualActionTypes = customerSettingLog.GetLogActionTypes();

            // Assert
            Assert.That(actualActionTypes, Is.EquivalentTo(validActionTypeList));
        }

        [Test]
        public void GetLogActionTypes_NoDataInCache_ReturnsActionTypesFromDataAccess()
        {
            // Arrange
            List<LogActionType> emptyActionTypeList = null;
            cacheManager.Get<List<LogActionType>>(CacheKeyLogActionTypes).Returns(emptyActionTypeList);

            var validActionTypeList = GetValidActionTypeList();
            dynamicRepository.Fetch<LogActionType>(Arg.Any<GetAllLogActionTypeCriteria>()).Returns(validActionTypeList);

            // Action
            var actualActionTypes = customerSettingLog.GetLogActionTypes();

            // Assert
            Assert.That(actualActionTypes, Is.EquivalentTo(validActionTypeList));
        }

        [Test]
        public void GetLogActionTypes_NoDataInCache_SetDataReceivedFromDataAccessToCache()
        {
            // Arrange
            List<LogActionType> emptyActionTypeList = null;
            cacheManager.Get<List<LogActionType>>(CacheKeyLogActionTypes).Returns(emptyActionTypeList);

            var validActionTypeList = GetValidActionTypeList();
            dynamicRepository.Fetch<LogActionType>(Arg.Any<GetAllLogActionTypeCriteria>()).Returns(validActionTypeList);

            // Action
            var actualActionTypes = customerSettingLog.GetLogActionTypes();

            // Assert
            cacheManager.Received().Add(CacheKeyLogActionTypes, Arg.Any<List<LogActionType>>());
        }

        [Test]
        public void GetLogs_IsDebug_ClearTransformObjectCache()
        {
            // Arrange
            var getLogsParam = new GetLogsParam
            {
                IsDebug = true
            };

            // Action
            customerSettingLog.GetLogs(getLogsParam);

            // Assert
            cacheManager.Received().Remove(CacheKeyTransformObject);
        }

        private List<LogActionType> GetValidActionTypeList()
        {
            var actionTypes = new List<LogActionType>();
            actionTypes.Add(new LogActionType()
            {
                Id = 1,
                EnglishName = "Action Type 1"
            });

            actionTypes.Add(new LogActionType()
            {
                Id = 2,
                EnglishName = "Action Type 2"
            });

            return actionTypes;
        }
    }
}
