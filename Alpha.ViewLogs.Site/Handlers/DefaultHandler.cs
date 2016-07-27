namespace Alpha.ViewLogs.Site.Handlers
{
    using Alpha.ViewLogs.Site.Requests;
    using Alpha.ViewLogs.Site.ViewModels.Default;
    using MediatR;

    public class DefaultHandler : IRequestHandler<IndexQuery, IndexViewModel>
    {
        public IndexViewModel Handle(IndexQuery message)
        {
            var viewModel = new IndexViewModel();
            return viewModel;
        }
    }
}