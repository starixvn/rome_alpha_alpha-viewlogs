namespace Alpha.ViewLogs.Site.Requests
{
    using MediatR;
    using ViewModels.Default;

    public class IndexQuery : IRequest<IndexViewModel>
    {
    }
}