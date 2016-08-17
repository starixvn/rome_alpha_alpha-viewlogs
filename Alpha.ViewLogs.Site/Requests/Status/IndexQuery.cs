namespace Alpha.ViewLogs.Site.Requests.Status
{
    using MediatR;
    using ViewModels.Status;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
    {
        public string TrackingValueTransformTemplatePath { get; set; }
    }
}