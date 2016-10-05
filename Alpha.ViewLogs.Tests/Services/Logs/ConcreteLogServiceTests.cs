namespace Alpha.ViewLogs.Tests.Services.Logs
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Models;
    using ModelsForTesting;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using ViewLogs.Services;
    using ViewLogs.Services.Impl;

    public abstract class ConcreteLogServiceTests
    {
        protected BaseService LogService { get; set; }

        protected IDynamicRepository DynamicRepository { get; set; }

        protected CustomerService CustomerService { get; set; }

        protected LogDataAccess BaseDataAccess { get; set; }

        public virtual void SetUp()
        {
            DynamicRepository = Substitute.For<IDynamicRepository>();
            CustomerService = Substitute.For<CustomerService>();
            BaseDataAccess = Substitute.For<LogDataAccess>();
        }

        public virtual void GetPagingLogs_ValidDownline_ReturnsDataFromDataAccess()
        {
            // Arrange
            var logParam = new SettingParam
            {
                Language = LanguageCode.English
            };

            var validDownlineInfo = new CustomerInfo(1, false);

            CustomerService.GetDownlineInfoByUsername(logParam.Username, logParam.UplineId).Returns(validDownlineInfo);

            var logs = new List<Log>
            {
                new Log { ActionId = 1 }
            };

            SubstituteGetLogsFromDataAccess(logs);

            var expectedPagingLogs = PagingLogsForTesting<Log>.FromPagingLogs(new PagingLogs<Log> { Logs = logs });

            // Action
            var actualPagingLogs = PagingLogsForTesting<Log>.FromPagingLogs(LogService.GetPagingLogs(logParam));

            // Assert
            Assert.AreEqual(expectedPagingLogs, actualPagingLogs);
        }

        public virtual void CreateGetLogsCriteria_NullLogParam_ThrowsArgumentNullException()
        {
            // Arrange
            SettingParam nullLogParam = null;

            // Action & Assert
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => LogService.CreateGetLogsCriteria(nullLogParam)));
        }

        public virtual void CreateGetTotalLogsCriteria_NullLogParam_ThrowsArgumentNullException()
        {
            // Arrange
            SettingParam nullLogParam = null;

            // Action & Assert
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => LogService.CreateGetTotalLogsCriteria(nullLogParam)));
        }

        protected virtual void SubstituteGetLogsFromDataAccess(IEnumerable<Log> logs)
        {
            DynamicRepository.Fetch<Log>(Arg.Any<ICriteria>()).Returns(logs);
        }
    }
}
