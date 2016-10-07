namespace Alpha.ViewLogs.Site.Tests.Handlers
{
    using System;
    using Fanex.Common;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using Requests.Login;
    using Services;
    using Site.Handlers;
    using ViewLogs.Constants;

    [TestFixture]
    public class LoginHandlerTests : ConcreteLogHandlerTests
    {
        private LoginHandler loginHandler;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            loginHandler = new LoginHandler(LogService, LogFormatter, DateTimeWrapper);
        }

        [Test]
        public void HandleIndexQuery_NullIndexQuery_ThrowsNullReferenceException()
        {
            // Arrange
            IndexQuery indexQuery = null;

            // Action & Assert
            Assert.Throws<NullReferenceException>(new TestDelegate(() => loginHandler.Handle(indexQuery)));
        }

        [Test]
        public void HandleIndexQuery_NullFromToDateAndTodayIsBeginningOfMonth_GetsPagingLogsFromBeginningOfMonthToBeginningOfMonth()
        {
            // Arrange
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                false);

            var toDate = new DateTime(2016, 10, 1);
            DateTimeWrapper.Now.Returns(toDate);

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = loginHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.FromDate == toDate && logParam.ToDate == toDate));
        }

        [Test]
        public void HandleIndexQuery_NullFromToDateAndTodayIsNotBeginningOfMonth_GetsPagingLogsFromYesterdayToToday()
        {
            // Arrange
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                false);

            var toDate = new DateTime(2016, 10, 2);
            var yesterday = new DateTime(2016, 10, 1);
            DateTimeWrapper.Now.Returns(toDate);

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = loginHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.FromDate == yesterday && logParam.ToDate == toDate));
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

            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                false);

            indexQuery.FromDate = fromDateString;
            indexQuery.ToDate = toDateString;

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = loginHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.FromDate == fromDate && logParam.ToDate == toDate));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("0")]
        public void HandleIndexQuery_NotHavePageSizeAndPageIndex_GetsPagingLogsWithDefaultPagingOptions(string pageSize)
        {
            // Arrange
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                false);

            indexQuery.PageSize = pageSize;

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = loginHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.PageSize == DefaultVariables.PageSize
                && logParam.PageIndex == 0));
        }

        [TestCase("50", 50)]
        [TestCase("1", 1)]
        [TestCase("-1", -1)]
        [TestCase("2147483647", int.MaxValue)]
        [TestCase("-2147483648", int.MinValue)]
        [TestCase(InvalidPageIndex, DefaultVariables.PageSize)]
        public void HandleIndexQuery_HasPageSize_GetsPagingLogsWithInputPageSize(string pageSize, int expectedPageSize)
        {
            // Arrange
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                false);

            indexQuery.PageSize = pageSize;

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = loginHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.PageSize == expectedPageSize));
        }

        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("-1", -1)]
        [TestCase("2147483647", int.MaxValue)]
        [TestCase("-2147483648", int.MinValue)]
        [TestCase(InvalidPageIndex, 0)]
        public void HandleIndexQuery_HasPageIndex_GetsPagingLogsWithInputPageIndex(string pageIndex, int expectedPageIndex)
        {
            // Arrange
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                false);

            indexQuery.PageIndex = pageIndex;

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Action
            var actualLogViewModel = loginHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.PageIndex == expectedPageIndex));
        }
    }
}
