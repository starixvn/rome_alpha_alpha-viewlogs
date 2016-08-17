namespace Alpha.ViewLogs.Site.Requests.Status
{
    using MediatR;
    using ViewModels.Status;

    public class IndexQuery : GetLogsQuery, IRequest<IndexViewModel>
    {
    }
}