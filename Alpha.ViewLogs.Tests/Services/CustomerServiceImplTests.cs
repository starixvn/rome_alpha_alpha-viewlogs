namespace Alpha.ViewLogs.Tests.Services
{
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class CustomerServiceImplTests
    {
        private const string ValidUsername = "validUsername";
        private const int ValidCustId = 1;

        private IDynamicRepository dynamicRepository;
        private CustomerServiceImpl customerService;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();

            customerService = new CustomerServiceImpl(dynamicRepository);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetDownlineInfoByUsername_NullOrEmptyUsername_ReturnsDefaultCustomerInfo(string username)
        {
            // Arrange 
            var expectedCustomerInfo = new CustomerInfo();

            // Action 
            var customerInfo = customerService.GetDownlineInfoByUsername(username, 0);

            // Assert
            Assert.AreEqual(expectedCustomerInfo, customerInfo);
        }

        [Test]
        public void GetDownlineInfoByUsername_ValidUsername_ReturnCustomerInfoFromDataAccess()
        {
            // Arrange
            var criteria = new GetCustInfoByUsernameCommand
            {
                Username = ValidUsername,
                UplineId = 0
            };

            var expectedCustomerInfo = new CustomerInfo
            {
                IsSubAcc = true,
                CustId = ValidCustId
            };

            dynamicRepository.When(d => d.Execute(Arg.Is<GetCustInfoByUsernameCommand>(x => x.Username == criteria.Username && x.CustId == criteria.CustId)))
                .Do(c =>
                {
                    var custInfo = c[0] as GetCustInfoByUsernameCommand;
                    custInfo.CustId = ValidCustId;
                    custInfo.IsSubAcc = true;
                });

            // Action 
            var actualCustomerInfo = customerService.GetDownlineInfoByUsername(ValidUsername, 0);

            // Assert
            Assert.AreEqual(actualCustomerInfo, expectedCustomerInfo);
        }
    }
}
