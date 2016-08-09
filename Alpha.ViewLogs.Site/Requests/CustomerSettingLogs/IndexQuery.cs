namespace Alpha.ViewLogs.Site.Requests.CustomerSettingLogs
{
    using MediatR;
    using ViewModels.CustomerSettingLogs;

    public class IndexQuery : GetLogsQuery, IRequest<IndexViewModel>
    {
    }
}