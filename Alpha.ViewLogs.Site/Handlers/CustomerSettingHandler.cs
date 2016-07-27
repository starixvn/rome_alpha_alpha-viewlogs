namespace Alpha.ViewLogs.Site.Handlers
{
    using MediatR;
    using Requests;
    using Services;
    using ViewModels.CustomerSetting;

    public class CustomerSettingHandler : IRequestHandler<CustomerSettingQuery, CustomerSettingViewModel>
    {
        private readonly CustomerSettingService customerSettingService;

        public CustomerSettingHandler(CustomerSettingService customerSettingService)
        {
            this.customerSettingService = customerSettingService;
        }

        public CustomerSettingViewModel Handle(CustomerSettingQuery customerSettingQuery)
        {
            var viewModel = new CustomerSettingViewModel(customerSettingQuery);
            viewModel.LogActionTypes = customerSettingService.LogActionTypes();

            return viewModel;
        }
    }
}