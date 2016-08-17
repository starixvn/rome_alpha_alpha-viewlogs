namespace Alpha.ViewLogs.Site.Requests.CustomerSetting
{
    using MediatR;
    using ViewModels.CustomerSetting;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
    {
        public string TrackingValueTransformTemplatePath { get; set; }
    }
}