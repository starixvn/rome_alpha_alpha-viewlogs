namespace Alpha.ViewLogs.Site.Requests.Login
{
    using MediatR;
    using ViewModels.Login;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
    {
    }
}