namespace Alpha.ViewLogs.Tests.Services
{
    using System.Collections.Generic;
    using Criteria;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using ViewLogs.Services;
    using ViewLogs.Services.Impl.Logs;

    [TestFixture]
    public class CustomerSettingLogServiceImplTest
    {
        private const string CacheKeyLogActionTypes = "Alpha.ViewLogs.CustomerSetting.LogActionTypes";
        private const string CacheKeyTransformObject = "Alpha.ViewLogs.CustomerSetting.TransformObject";

        private CustomerService customerService;
        private LogDataAccess baseDataAccess;
        private CachingManager cacheManager;
        private IDynamicRepository dynamicRepository;
        private CustomerSettingServiceImpl customerSettingLog;

        [SetUp]
        public void SetUp()
        {
            customerService = Substitute.For<CustomerService>();
            baseDataAccess = Substitute.For<LogDataAccess>();
            cacheManager = Substitute.For<CachingManager>();
            dynamicRepository = Substitute.For<IDynamicRepository>();

            customerSettingLog = new CustomerSettingServiceImpl(cacheManager, dynamicRepository, customerService, baseDataAccess);
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