namespace Alpha.ViewLogs.Services.Impl
{
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Xml.XPath;
    using System.Xml.Xsl;
    using Params;

    public class TransformServiceImpl : TransformService
    {
        private const string TransformObjectCacheKey = "Alpha.ViewLogs.TransformObject_{0}";
        private const int OneHour = 1;

        private readonly CachingManager cacheManager;
        private readonly XsltLogFormatter xsltLogFormatService;

        public TransformServiceImpl(
            CachingManager cacheManager,
            XsltLogFormatter xsltLogFormatService)
        {
            this.cacheManager = cacheManager;
            this.xsltLogFormatService = xsltLogFormatService;
        }

        public string TransformTrackingValueToHtml(
            string trackingValue,
            int logActionId,
            TransformParam transformParam)
        {
            if (string.IsNullOrEmpty(trackingValue)
                || logActionId < 0)
            {
                return string.Empty;
            }

            var compiledTransform = GetCompiledTransformTemplate(logActionId, transformParam.TrackingValueTransformTemplatePath);

            if (compiledTransform == null)
            {
                return string.Empty;
            }

            xsltLogFormatService.AssignParams(transformParam);
            var xsltArguments = new XsltArgumentList();
            xsltArguments.AddExtensionObject("urn:LogFormat", xsltLogFormatService);

            // Transform xml must be lower case
            using (MemoryStream stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(trackingValue.ToLower())))
            {
                var document = new XPathDocument(stream);
                using (var writer = new StringWriter())
                {
                    compiledTransform.Transform(document, xsltArguments, writer);
                    return HttpUtility.HtmlDecode(writer.ToString());
                }
            }
        }

        private XslCompiledTransform GetCompiledTransformTemplate(int actionId, string trackingValueTransformTemplatePath)
        {
            var cacheKey = string.Format(TransformObjectCacheKey, actionId);
            var cachedCompiledTransform = cacheManager.Get<XslCompiledTransform>(cacheKey);

            if (cachedCompiledTransform != null)
            {
                return cachedCompiledTransform;
            }

            var compiledTransform = new XslCompiledTransform();
            var path = FindTemplatePathInDirectoryByActionId(actionId, trackingValueTransformTemplatePath);

            if (File.Exists(path))
            {
                compiledTransform.Load(path);
            }

            if (compiledTransform.OutputSettings == null
                || compiledTransform.TemporaryFiles == null)
            {
                return null;
            }

            cacheManager.Add(cacheKey, compiledTransform);

            return compiledTransform;
        }

        private string FindTemplatePathInDirectoryByActionId(int actionId, string directory)
        {
            var path = Path.Combine(directory, string.Format("action_{0}.xslt", actionId));

            if (File.Exists(path))
            {
                return path;
            }

            var subFolders = Directory.GetDirectories(directory);
            foreach (var subFolder in subFolders)
            {
                path = FindTemplatePathInDirectoryByActionId(actionId, Path.Combine(directory, subFolder));

                if (!string.IsNullOrEmpty(path))
                {
                    return path;
                }
            }

            return string.Empty;
        }
    }
}