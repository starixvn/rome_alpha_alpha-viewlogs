namespace Alpha.ViewLogs.Site.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using Requests.Credit;
    using Services;
    using Site.Handlers;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Helpers;

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

        private IEnumerable<string> sites;

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
            sites = new List<string>();
        }

        [Test]
        public void Handle_NullIndexQuery_ThrowsNullReferenceException()
        {
            // Arrange
            IndexQuery indexQuery = null;

            // Action & Assert
            Assert.Throws<NullReferenceException>(new TestDelegate(() => creditHandler.Handle(indexQuery)));
        }

        [Test]
        public void Handle_NullFromToDateAndFromDateIsBeginningOfMonth_GetsPagingLogsFromBeginningOfMonthToBeginningOfMonth()
        {
            // Arrange
            var indexQuery = new IndexQuery(currentUserSession, true, actionIdVisibilities, custStatuses, resourcesByBetTypeCacheRef, sites, false);

            var toDate = new DateTime(2016, 10, 1);
            dateTimeWrapper.Now.Returns(toDate);

            // logService.GetPagingLogs(Arg.Is<LogParam>());

            // Action
            // var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
        }
    }
}
