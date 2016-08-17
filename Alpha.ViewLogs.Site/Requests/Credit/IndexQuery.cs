namespace Alpha.ViewLogs.Site.Requests.Credit
{
    using MediatR;
    using ViewModels.Credit;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
    {
    }
}