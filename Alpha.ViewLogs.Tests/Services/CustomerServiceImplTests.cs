namespace Alpha.ViewLogs.Tests.Services
{
    using Criteria;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class CustomerServiceImplTests
    {
        private const string ValidUsername = "validUsername";
        private const int ValidCustId = 1;

        private CustomerDataAccess customerDataAccess;
        private IDynamicRepository dynamicRepository;
        private CustomerServiceImpl customerService;

        [SetUp]
        public void SetUp()
        {
            customerDataAccess = Substitute.For<CustomerDataAccess>();
            dynamicRepository = Substitute.For<IDynamicRepository>();

            customerService = new CustomerServiceImpl(customerDataAccess, dynamicRepository);
        }

        [Test]
        public void CheckIsBelongingByUsername_NullCriteria_ThrowNullReferenceException()
        {
            // Arrange
            CheckIsBelongingByUsernameCriteria nullCriteria = null;

            // Action & Assert
            Assert.Throws<NullReferenceException>(new TestDelegate(() => customerService.CheckIsBelongingByUsername(nullCriteria)));
        }

        [TestCase("")]
        [TestCase(null)]
        public void CheckIsBelongingByUsername_NullOrEmptyUsername_ReturnsNull(string username)
        {
            // Arrange
            var criteria = new CheckIsBelongingByUsernameCriteria
            {
                Username = username
            };

            // Action 
            var customerInfo = customerService.CheckIsBelongingByUsername(criteria);

            // Assert
            Assert.IsNull(customerInfo);
        }

        [Test]
        public void CheckIsBelongingByUsername_ValidUsername_ReturnCustomerInfoFromDataAccess()
        {
            // Arrange
            var criteria = new CheckIsBelongingByUsernameCriteria
            {
                Username = ValidUsername
            };

            var expectedCustomerInfo = new CustomerInfo
            {
                IsSubAcc = true,
                CustId = ValidCustId
            };

            customerDataAccess.CheckIsBelongingByUsername(criteria).Returns<CustomerInfo>(expectedCustomerInfo);

            // Action 
            var actualCustomerInfo = customerService.CheckIsBelongingByUsername(criteria);

            // Assert
            Assert.AreEqual(actualCustomerInfo, expectedCustomerInfo);
        }
    }
}
