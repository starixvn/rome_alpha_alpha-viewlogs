namespace Alpha.ViewLogs.Site.Handlers
{
    using AppResources;
    using MediatR;
    using Requests;
    using Services;
    using System.Collections.Generic;
    using System.Linq;
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
            viewModel.LogActionTypes = GetLogActionTypes(customerSettingQuery);

            customerSettingService.GetLogs(customerSettingQuery.ConvertToGetLogsParam());

            return viewModel;
        }

        private List<LogActionTypeViewModel> GetLogActionTypes(CustomerSettingQuery customerSettingQuery)
        {
            var logActionTypes = customerSettingService.LogActionTypes();

            var logActionTypeViewModels = logActionTypes.Select(logActionType => new LogActionTypeViewModel(logActionType, customerSettingQuery.Language))
                .ToList();
            logActionTypeViewModels.Insert(0, new LogActionTypeViewModel(0, Resources.All));

            return logActionTypeViewModels;
        }
    }
}