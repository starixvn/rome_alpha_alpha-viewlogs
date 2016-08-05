namespace Alpha.ViewLogs.Services.Impl
{
    using Alpha.ViewLogs.Models;
    using Params;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Xml.XPath;
    using System.Xml.Xsl;

    public class TransformTrackingValueToHtmlServiceImpl : TransformTrackingValueToHtmlService
    {
        private const string CacheKeyTransformObject = "Alpha.ViewLogs.TransformObject";
        private const int HourToCacheCompiledTransforms = 1;

        private readonly CachingManager cacheManager;
        private readonly XsltLogFormatService xsltLogFormatService;

        public TransformTrackingValueToHtmlServiceImpl(
            CachingManager cacheManager,
            XsltLogFormatService xsltLogFormatService)
        {
            this.cacheManager = cacheManager;
            this.xsltLogFormatService = xsltLogFormatService;
        }

        public void InitCompiledTransformCache(TransformTrackingValueToHtmlParam transformParam)
        {
            if (transformParam.IsDebug)
            {
                cacheManager.Remove(CacheKeyTransformObject);
            }

            var compiledTransforms = cacheManager.Get<Dictionary<int, XslCompiledTransform>>(CacheKeyTransformObject);

            if (compiledTransforms == null)
            {
                compiledTransforms = new Dictionary<int, XslCompiledTransform>();
                cacheManager.Add(CacheKeyTransformObject, compiledTransforms, TimeSpan.FromHours(HourToCacheCompiledTransforms));
            }
        }

        public string TransformTrackingValueToHtml(
            string trackingValue,
            BaseLog baseLog,
            TransformTrackingValueToHtmlParam transformParam)
        {
            var htmlTrackingValue = string.Empty;
            if (string.IsNullOrEmpty(trackingValue) || baseLog.ActionId < 0)
            {
                return htmlTrackingValue;
            }

            var compiledTransform = GetCompiledTransformByActionIdFromCache(baseLog.ActionId) ??
                GetCompiledTransformByActionIdFromTemplates(baseLog.ActionId, transformParam.TrackingValueTransformTemplatePath);
            if (compiledTransform.OutputSettings == null || compiledTransform.TemporaryFiles == null)
            {
                return htmlTrackingValue;
            }

            CacheCompiledTransform(baseLog.ActionId, compiledTransform);

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
                    htmlTrackingValue = writer.ToString();
                }
            }

            return HttpUtility.HtmlDecode(htmlTrackingValue);
        }

        private XslCompiledTransform GetCompiledTransformByActionIdFromTemplates(int actionId, string trackingValueTransformTemplatePath)
        {
            var xslCompiledTransform = new XslCompiledTransform();
            var path = Path.Combine(trackingValueTransformTemplatePath, string.Format("action_{0}.xslt", actionId));

            if (File.Exists(path))
            {
                xslCompiledTransform.Load(path);
            }

            return xslCompiledTransform;
        }

        private XslCompiledTransform GetCompiledTransformByActionIdFromCache(int actionId)
        {
            var compiledTransforms = cacheManager.Get<Dictionary<int, XslCompiledTransform>>(CacheKeyTransformObject);
            var compiledTransform = compiledTransforms != null && compiledTransforms.ContainsKey(actionId) ? compiledTransforms[actionId] : null;

            return compiledTransform;
        }

        private void CacheCompiledTransform(int actionId, XslCompiledTransform compiledTransform)
        {
            var compiledTransforms = cacheManager.Get<Dictionary<int, XslCompiledTransform>>(CacheKeyTransformObject);
            if (!compiledTransforms.ContainsKey(actionId))
            {
                compiledTransforms.Add(actionId, compiledTransform);
            }

            cacheManager.Add(CacheKeyTransformObject, compiledTransforms);
        }
    }
}
