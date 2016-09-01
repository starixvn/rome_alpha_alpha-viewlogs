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
        private const string LogActionCacheKey = "Alpha.ViewLogs.Setting.LogAction";
        private const string LogActionTypesCacheKey = "Alpha.ViewLogs.Setting.LogActionTypes";

        private BaseServiceForTesting baseService;

        private IDynamicRepository dynamicRepository;
        private CustomerService customerService;
        private LogDataAccess logDataAccess;

        private Cache cacheForTesting;

        [SetUp]
        public void SetUp()
        {
            cacheForTesting = Substitute.For<Cache>();
            CacheService.SwitchCacheStorage(cacheForTesting);

            dynamicRepository = Substitute.For<IDynamicRepository>();
            customerService = Substitute.For<CustomerService>();
            logDataAccess = Substitute.For<LogDataAccess>();

            baseService = new BaseServiceForTesting(dynamicRepository, customerService, logDataAccess);
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
            var expectedLogActionTypes = new List<LogActionType>()
            {
                new LogActionType { Id = 1 }
            };

            List<LogActionType> logActionTypesInCache;

            cacheForTesting.TryGet(LogActionTypesCacheKey, out logActionTypesInCache).Returns(x =>
            {
                x[1] = expectedLogActionTypes;

                return true;
            });

            // Action
            var actualLogActionTypes = baseService.GetLogActionTypes();

            // Assert
            Assert.AreEqual(expectedLogActionTypes, actualLogActionTypes);
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
        public void GetLogActionTypes_EmptyListInCache_CallDataAccessToGetData()
        {
            // Arrange
            List<LogActionType> logActionTypesInCache = new List<LogActionType>();
            cacheForTesting.TryGet(LogActionTypesCacheKey, out logActionTypesInCache).Returns(x =>
            {
                x[1] = logActionTypesInCache;

                return true;
            });

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

        [TestCase(1, "Action 1")]
        [TestCase(2, "Action 2")]
        public void GetLogAction_HasDataInCache_ReturnsDataInCache(int actionId, string expectedActionName)
        {
            // Arrange
            var expectedActionDictionary = new Dictionary<int, LogAction>
            {
                { 1, new LogAction() { Id = 1, EnglishName = "Action 1" } },
                { 2, new LogAction() { Id = 2, EnglishName = "Action 2" } }
            };

            var expectedAction = expectedActionDictionary[actionId];

            Dictionary<int, LogAction> logActionInCache;
            cacheForTesting.TryGet(LogActionCacheKey, out logActionInCache).Returns(x =>
            {
                x[1] = expectedActionDictionary;

                return true;
            });

            // Action
            var actualLogAction = baseService.GetLogAction(actionId);

            // Assert
            Assert.AreEqual(expectedAction, actualLogAction);
        }

        [TestCase(-1, null)]
        [TestCase(3, null)]
        public void GetLogAction_NotMatchDataInCache_ReturnsDefaultAction(int actionId, string expectedActionName)
        {
            // Arrange
            var expectedActionDictionary = new Dictionary<int, LogAction>
            {
                { 1, new LogAction() { Id = 1, EnglishName = "Action 1" } },
                { 2, new LogAction() { Id = 2, EnglishName = "Action 2" } }
            };

            var expectedAction = new LogAction() { Id = actionId, EnglishName = expectedActionName };

            Dictionary<int, LogAction> logActionInCache;
            cacheForTesting.TryGet(LogActionCacheKey, out logActionInCache).Returns(x =>
            {
                x[1] = expectedActionDictionary;

                return true;
            });

            // Action
            var actualLogAction = baseService.GetLogAction(actionId);

            // Assert
            Assert.IsTrue(actualLogAction.Id == expectedAction.Id && actualLogAction.EnglishName == expectedAction.EnglishName);
        }

        [TestCase(-1, null)]
        [TestCase(3, null)]
        public void GetLogAction_NoDataInCache_ReturnsDefaultAction(int actionId, string expectedActionName)
        {
            // Arrange
            var expectedAction = new LogAction()
            {
                Id = actionId,
                EnglishName = expectedActionName
            };

            Dictionary<int, LogAction> logActionInCache;
            cacheForTesting.TryGet(LogActionCacheKey, out logActionInCache).Returns(false);

            // Action
            var actualLogAction = baseService.GetLogAction(actionId);

            // Assert
            Assert.IsTrue(actualLogAction.Id == expectedAction.Id && actualLogAction.EnglishName == expectedAction.EnglishName);
        }

        [TestCase(-1, null, LanguageCode.ChineseTraditional)]
        [TestCase(-1, null, LanguageCode.ChineseSimplified)]
        [TestCase(-1, null, LanguageCode.Japanese)]
        [TestCase(-1, null, LanguageCode.Thai)]
        [TestCase(-1, null, LanguageCode.Korean)]
        [TestCase(-1, null, LanguageCode.Vietnamese)]
        [TestCase(-1, null, LanguageCode.English)]
        [TestCase(1, "Action Name 1 - ChineseTraditional", LanguageCode.ChineseTraditional)]
        [TestCase(1, "Action Name 1 - ChineseSimplified", LanguageCode.ChineseSimplified)]
        [TestCase(1, "Action Name 1 - Japanese", LanguageCode.Japanese)]
        [TestCase(1, "Action Name 1 - Thai", LanguageCode.Thai)]
        [TestCase(1, "Action Name 1 - Korean", LanguageCode.Korean)]
        [TestCase(1, "Action Name 1 - Vietnamese", LanguageCode.Vietnamese)]
        [TestCase(1, "Action Name 1 - English", LanguageCode.English)]
        public void GetLogActionName_AnyActionId_ReturnsCorrectName(
            int actionId,
            string expectedActionName,
            string language)
        {
            // Arrange
            var logAction = new LogAction()
            {
                Id = 1,
                ChineseTraditionalName = "Action Name 1 - ChineseTraditional",
                ChineseSimplifiedName = "Action Name 1 - ChineseSimplified",
                JapaneseName = "Action Name 1 - Japanese",
                ThaiName = "Action Name 1 - Thai",
                KoreanName = "Action Name 1 - Korean",
                VietnameseName = "Action Name 1 - Vietnamese",
                EnglishName = "Action Name 1 - English"
            };

            var expectedActionDictionary = new Dictionary<int, LogAction>
            {
                { 1, logAction }
            };

            Dictionary<int, LogAction> logActionInCache;
            cacheForTesting.TryGet(LogActionCacheKey, out logActionInCache).Returns(x =>
            {
                x[1] = expectedActionDictionary;

                return true;
            });

            // Action
            var actualLogActionName = baseService.GetLogActionName(actionId, language);

            // Assert
            Assert.AreEqual(expectedActionName, actualLogActionName);
        }
    }
}
