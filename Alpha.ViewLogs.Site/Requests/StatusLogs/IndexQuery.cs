namespace Alpha.ViewLogs.Site.Requests.StatusLogs
{
    using MediatR;
    using ViewModels.StatusLogs;

    public class IndexQuery : GetLogsQuery, IRequest<IndexViewModel>
    {
        public int ActionTypeId { get; set; }
    }
}