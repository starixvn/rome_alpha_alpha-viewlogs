namespace Alpha.ViewLogs.Site.Requests.CustomerSetting
{
    using MediatR;
    using ViewModels.CustomerSetting;

    public class IndexQuery : GetLogsQuery, IRequest<IndexViewModel>
    {
    }
}