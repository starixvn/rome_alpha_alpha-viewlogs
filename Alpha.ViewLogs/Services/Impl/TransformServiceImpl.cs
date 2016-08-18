namespace Alpha.ViewLogs.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Xml.XPath;
    using System.Xml.Xsl;
    using Alpha.ViewLogs.Models;
    using Params;

    public class TransformServiceImpl : TransformService
    {
        private const string CacheKeyTransformObject = "Alpha.ViewLogs.TransformObject";
        private const int HourToCacheCompiledTransforms = 1;

        private readonly CachingManager cacheManager;
        private readonly XsltLogFormatter xsltLogFormatService;

        public TransformServiceImpl(
            CachingManager cacheManager,
            XsltLogFormatter xsltLogFormatService)
        {
            this.cacheManager = cacheManager;
            this.xsltLogFormatService = xsltLogFormatService;
        }

        public void InitCompiledTransformCache(TransformTrackingValueToHtmlParam transformParam)
        {
            var compiledTransforms = cacheManager.Get<Dictionary<int, XslCompiledTransform>>(CacheKeyTransformObject);

            if (compiledTransforms == null)
            {
                compiledTransforms = new Dictionary<int, XslCompiledTransform>();
                cacheManager.Add(CacheKeyTransformObject, compiledTransforms, TimeSpan.FromHours(HourToCacheCompiledTransforms));
            }
        }

        public string TransformTrackingValueToHtml(
            string trackingValue,
            Log log,
            TransformTrackingValueToHtmlParam transformParam)
        {
            var htmlTrackingValue = string.Empty;

            if (string.IsNullOrEmpty(trackingValue) || log.ActionId < 0)
            {
                return htmlTrackingValue;
            }

            var compiledTransform = GetCompiledTransformByActionIdFromCache(log.ActionId) ??
                GetCompiledTransformByActionIdFromTemplates(log.ActionId, transformParam.TrackingValueTransformTemplatePath);

            if (compiledTransform.OutputSettings == null || compiledTransform.TemporaryFiles == null)
            {
                return htmlTrackingValue;
            }

            CacheCompiledTransform(log.ActionId, compiledTransform);

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
            var path = FindTemplatePathInDirectoryByActionId(actionId, trackingValueTransformTemplatePath);

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
