namespace Alpha.ViewLogs.Site.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using Fanex.Common;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using Requests.Credit;
    using Services;
    using Site.Handlers;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Constants;
    using ViewLogs.Helpers;
    using ViewModels;

    [TestFixture]
    public class CreditHandlerTests
    {
        private LogService logService;
        private LogFormatter logFormatter;
        private CreditHandler creditHandler;
        private IDateTime dateTimeWrapper;

        private IUserSession currentUserSession;

        private Dictionary<int, bool> actionIdVisibilities;

        private Dictionary<string, bool> custStatuses;

        private Dictionary<string, int> resourcesByBetTypeCacheRef;

        [SetUp]
        public void SetUp()
        {
            logService = Substitute.For<LogService>();
            logFormatter = Substitute.For<LogFormatter>();
            dateTimeWrapper = Substitute.For<IDateTime>();
            creditHandler = new CreditHandler(logService, logFormatter, dateTimeWrapper);

            currentUserSession = Substitute.For<IUserSession>();
            actionIdVisibilities = new Dictionary<int, bool>();
            custStatuses = new Dictionary<string, bool>();
            resourcesByBetTypeCacheRef = new Dictionary<string, int>();
        }

        [Test]
        public void HandleIndexQuery_NullIndexQuery_ThrowsNullReferenceException()
        {
            // Arrange
            IndexQuery indexQuery = null;

            // Action & Assert
            Assert.Throws<NullReferenceException>(new TestDelegate(() => creditHandler.Handle(indexQuery)));
        }

        [Test]
        public void HandleIndexQuery_NullFromToDateAndTodayIsBeginningOfMonth_GetsPagingLogsFromBeginningOfMonthToBeginningOfMonth()
        {
            // Arrange
            var indexQuery = new IndexQuery(currentUserSession, true, actionIdVisibilities, custStatuses, resourcesByBetTypeCacheRef, false);

            var toDate = new DateTime(2016, 10, 1);
            dateTimeWrapper.Now.Returns(toDate);

            logService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            logService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.FromDate == toDate && logParam.ToDate == toDate));
        }

        [Test]
        public void HandleIndexQuery_NullFromToDateAndTodayIsNotBeginningOfMonth_GetsPagingLogsFromYesterdayToToday()
        {
            // Arrange
            var indexQuery = new IndexQuery(currentUserSession, true, actionIdVisibilities, custStatuses, resourcesByBetTypeCacheRef, false);

            var toDate = new DateTime(2016, 10, 2);
            var yesterday = new DateTime(2016, 10, 1);
            dateTimeWrapper.Now.Returns(toDate);

            logService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            logService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.FromDate == yesterday && logParam.ToDate == toDate));
        }

        [TestCase("2016-10-09", "2016-10-09")]
        [TestCase("2016-10-08", "2016-10-09")]
        [TestCase("2016-10-01", "2016-10-09")]
        [TestCase("2016-10-31", "2016-10-09")]
        [TestCase("2016-10-01", "2016-10-01")]
        [TestCase("2016-10-31", "2016-10-31")]
        public void HandleIndexQuery_HasFromToDate_GetsPagingLogsWithFromAnToDateRange(string fromDateString, string toDateString)
        {
            // Arrange
            var fromDate = Converter.ToDateTime(fromDateString);
            var toDate = Converter.ToDateTime(toDateString);

            var indexQuery = new IndexQuery(currentUserSession, true, actionIdVisibilities, custStatuses, resourcesByBetTypeCacheRef, false);
            indexQuery.FromDate = fromDateString;
            indexQuery.ToDate = toDateString;

            logService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            logService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.FromDate == fromDate && logParam.ToDate == toDate));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("0")]
        public void HandleIndexQuery_NotHavePageSizeAndPageIndex_GetsPagingLogsWithDefaultPagingOptions(string pageSize)
        {
            // Arrange
            var indexQuery = new IndexQuery(currentUserSession, true, actionIdVisibilities, custStatuses, resourcesByBetTypeCacheRef, false);
            indexQuery.PageSize = pageSize;

            logService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            logService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.PageSize == DefaultVariables.PageSize
                && logParam.PageIndex == 0));
        }

        [TestCase("50", 50)]
        [TestCase("1", 1)]
        [TestCase("-1", -1)]
        [TestCase("2147483647", int.MaxValue)]
        [TestCase("-2147483648", int.MinValue)]
        [TestCase("asdasd", DefaultVariables.PageSize)]
        public void HandleIndexQuery_HasPageSize_GetsPagingLogsWithInputPageSize(string pageSize, int expectedPageSize)
        {
            // Arrange
            var indexQuery = new IndexQuery(currentUserSession, true, actionIdVisibilities, custStatuses, resourcesByBetTypeCacheRef, false);
            indexQuery.PageSize = pageSize;

            logService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            logService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.PageSize == expectedPageSize));
        }

        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("-1", -1)]
        [TestCase("2147483647", int.MaxValue)]
        [TestCase("-2147483648", int.MinValue)]
        [TestCase("asdasd", 0)]
        public void HandleIndexQuery_HasPageIndex_GetsPagingLogsWithInputPageSize(string pageIndex, int expectedPageIndex)
        {
            // Arrange
            var indexQuery = new IndexQuery(currentUserSession, true, actionIdVisibilities, custStatuses, resourcesByBetTypeCacheRef, false);
            indexQuery.PageIndex = pageIndex;

            logService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            logService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.PageIndex == expectedPageIndex));
        }

        [Test]
        public void HandleIndexQuery_HasLogsAndNotHideAnyLog_GetsPagingLogsWithInputPageSize()
        {
            // Arrange
            var actionIdVisibilities = new Dictionary<int, bool>();
            actionIdVisibilities.Add(1, true);

            var indexQuery = new IndexQuery(currentUserSession, true, actionIdVisibilities, custStatuses, resourcesByBetTypeCacheRef, false);
            indexQuery.PageIndex = string.Empty;
            indexQuery.PageSize = DefaultVariables.PageSize.ToString();

            var pagingLogs = new PagingLogs<Log>
            {
                TotalRecords = 2,
                Logs = new List<Log>
                {
                    new Log() { ActionId = 1 },
                    new Log() { ActionId = 2 }
                }
            };

            logService.GetPagingLogs(Arg.Any<LogParam>()).Returns(pagingLogs);

            var expectedLogViewModel = new LogViewModel(Arg.Any<LogParam>(), pagingLogs.TotalRecords);

            // Action
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            Assert.AreEqual(expectedLogViewModel, actualLogViewModel);
        }
    }
}
