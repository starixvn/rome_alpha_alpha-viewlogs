namespace Alpha.ViewLogs.Site.Helpers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Xml;
    using Alpha.ViewLogs.Models;
    using ViewLogs.Constants;
    using ViewLogs.Helpers;

    public class TrackingValueFormatterImpl : TrackingValueFormatter
    {
        public string GetFormattedDiffTrackingValue(Log log, bool isShowOldValue)
        {
            if (log == null ||
                string.IsNullOrWhiteSpace(log.TrackingValue))
            {
                return string.Empty;
            }

            log.TrackingValue = StandardizeTrackingValue(log.TrackingValue);
            log.TrackingValueOld = StandardizeTrackingValue(log.TrackingValueOld);

            var trackingValueDoc = new XmlDocument();
            trackingValueDoc.InnerXml = log.TrackingValue;

            var hasOldXml = !string.IsNullOrWhiteSpace(log.TrackingValueOld);
            var trackingValueOldDoc = hasOldXml ? new XmlDocument() : trackingValueDoc;
            trackingValueOldDoc.InnerXml = hasOldXml ? log.TrackingValueOld : trackingValueDoc.InnerXml;

            FormatDiffAttributes(trackingValueDoc.DocumentElement.Attributes, trackingValueOldDoc.DocumentElement.Attributes, isShowOldValue);

            var childNodeCount = trackingValueDoc.DocumentElement.ChildNodes.Count;

            for (int i = 0; i < childNodeCount; i++)
            {
                var newNodes = trackingValueDoc.DocumentElement.ChildNodes[i];
                var oldNodes = trackingValueOldDoc.DocumentElement.ChildNodes[i];
                FormatDiffAttributes(newNodes.Attributes, oldNodes.Attributes, isShowOldValue);
            }

            return trackingValueDoc.InnerXml;
        }

        private static void FormatDiffAttributes(
            XmlAttributeCollection newAttributes,
            XmlAttributeCollection oldAttributes,
            bool isShowOldValue)
        {
            var ignoreFormatAttributes = new string[] { "ip", "loginip", "stakelevel1" };

            foreach (XmlAttribute newAttribute in newAttributes)
            {
                var newValue = newAttribute.Value;

                var oldAttribute = oldAttributes[newAttribute.Name];
                var oldValue = oldAttribute.Value;

                newValue = StandardizeAttributeValue(newValue);
                oldValue = StandardizeAttributeValue(oldValue);

                var isAttributeNeedBeFormatted = (!string.IsNullOrWhiteSpace(newValue) || !string.IsNullOrWhiteSpace(oldValue)) &&
                    !ignoreFormatAttributes.Contains(newAttribute.Name.ToLower()) &&
                    !string.Equals(newValue, oldValue, StringComparison.OrdinalIgnoreCase);

                if (!isAttributeNeedBeFormatted)
                {
                    newAttributes[newAttribute.Name].Value = newValue;
                    continue;
                }

                newAttributes[newAttribute.Name].Value = FormatValue(newValue, oldValue, isShowOldValue);
            }
        }

        private static string FormatValue(string newValue, string oldValue, bool isShowOldValue)
        {
            string formattedValue = string.Empty;

            if (!isShowOldValue)
            {
                formattedValue = $"<span class=\"valuechanged\" title=\"{oldValue} --> {newValue}\">{newValue}</span>";
            }
            else
            {
                formattedValue =
                    $"<span class=\"valuechanged\">{newValue}</span>" +
                    $"<div class=\"line\"></div>" +
                    $"<span class=\"valueold\">{oldValue}</span>";
            }

            return formattedValue;
        }

        private static string StandardizeTrackingValue(string trackingValue)
        {
            var result = trackingValue;

            if (!string.IsNullOrWhiteSpace(result))
            {
                result = result.Replace("\a", string.Empty);
            }

            return result;
        }

        private static bool IsPermissionValue(string attrValue)
        {
            return attrValue.Contains("[") && attrValue.Contains("]");
        }

        private static string StandardizeAttributeValue(string attrValue)
        {
            if (string.IsNullOrWhiteSpace(attrValue))
            {
                return attrValue;
            }

            var standardizedAttributeValue = attrValue;
            var isPermissionAttribute = IsPermissionValue(attrValue);

            if (isPermissionAttribute)
            {
                standardizedAttributeValue = FormatPermissionValue(standardizedAttributeValue);
            }

            standardizedAttributeValue = StringHelper.RemoveTrailingZeros(standardizedAttributeValue);
            standardizedAttributeValue = HttpUtility.HtmlEncode(standardizedAttributeValue);

            return standardizedAttributeValue;
        }

        private static string FormatPermissionValue(string attrValue)
        {
            var standardizedAttributeValue = attrValue.ToUpper();

            var pemissionAbbriations = PageAccessType.PemissionAbbriations;

            foreach (var pemissionAbbriation in pemissionAbbriations)
            {
                if (standardizedAttributeValue.Contains(pemissionAbbriation.Key))
                {
                    standardizedAttributeValue = standardizedAttributeValue.Replace(pemissionAbbriation.Key, pemissionAbbriation.Value);
                }
            }

            var pageAccessTypeNames = PageAccessType.PageAccessTypeName;

            foreach (var pageAccessTypeName in pageAccessTypeNames)
            {
                if (standardizedAttributeValue.Contains(pageAccessTypeName.Key))
                {
                    standardizedAttributeValue = standardizedAttributeValue.Replace(pageAccessTypeName.Key, "[" + pageAccessTypeName.Value + "]");
                }
            }

            return standardizedAttributeValue;
        }
    }
}