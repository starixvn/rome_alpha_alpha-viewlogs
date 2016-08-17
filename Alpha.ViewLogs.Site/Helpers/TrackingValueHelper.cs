namespace Alpha.ViewLogs.Site.Helpers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Xml;
    using Alpha.ViewLogs.Models;
    using ViewLogs.Constants;
    using ViewLogs.Helpers;

    public static class TrackingValueHelper
    {
        public static string GetTrackingValueWithDiff(Log baseLog, bool isShowOldValue)
        {
            if (string.IsNullOrEmpty(baseLog.TrackingValue))
            {
                return string.Empty;
            }

            baseLog.TrackingValue = StandardizeTrackingValue(baseLog.TrackingValue);
            baseLog.TrackingValueOld = StandardizeTrackingValue(baseLog.TrackingValueOld);

            var trackingValueDoc = new XmlDocument();
            trackingValueDoc.InnerXml = baseLog.TrackingValue;

            var hasOldXml = !string.IsNullOrEmpty(baseLog.TrackingValueOld);
            var trackingValueOldDoc = hasOldXml ? new XmlDocument() : trackingValueDoc;
            trackingValueOldDoc.InnerXml = hasOldXml ? baseLog.TrackingValueOld : trackingValueDoc.InnerXml;

            FormatAttributesWithDiff(trackingValueDoc.DocumentElement.Attributes, trackingValueOldDoc.DocumentElement.Attributes, isShowOldValue);

            for (int i = 0, childNodeCount = trackingValueDoc.DocumentElement.ChildNodes.Count; i < childNodeCount; i++)
            {
                var newNodes = trackingValueDoc.DocumentElement.ChildNodes[i];
                var oldNodes = trackingValueOldDoc.DocumentElement.ChildNodes[i];
                FormatAttributesWithDiff(newNodes.Attributes, oldNodes.Attributes, isShowOldValue);
            }

            return trackingValueDoc.InnerXml;
        }

        private static void FormatAttributesWithDiff(
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

                var isFormatAttribute = (!string.IsNullOrEmpty(newValue) || !string.IsNullOrEmpty(oldValue)) &&
                    !ignoreFormatAttributes.Contains(newAttribute.Name.ToLower()) &&
                    !string.Equals(newValue, oldValue, StringComparison.OrdinalIgnoreCase);
                if (!isFormatAttribute)
                {
                    newAttributes[newAttribute.Name].Value = newValue;
                    continue;
                }

                var attributeWithDiff = string.Empty;

                if (!isShowOldValue)
                {
                    attributeWithDiff = $"<span class=\"valuechanged\" title=\"{oldValue} --> {newValue}\">{newValue}</span>";
                }
                else if (string.Equals(oldValue, newValue, StringComparison.OrdinalIgnoreCase))
                {
                    attributeWithDiff = $"<span class=\"valuechanged\">${newValue}</span>";
                }
                else
                {
                    attributeWithDiff =
                        $"<span class=\"valuechanged\">{newValue}</span>" +
                        $"<div class=\"line\"></div>" +
                        $"<span class=\"valueold\">{oldValue}</span>";
                }

                newAttributes[newAttribute.Name].Value = attributeWithDiff;
            }
        }

        private static string StandardizeTrackingValue(string trackingValue)
        {
            var result = trackingValue;
            if (!string.IsNullOrEmpty(result))
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
            if (string.IsNullOrEmpty(attrValue))
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
                standardizedAttributeValue = standardizedAttributeValue.Replace(pemissionAbbriation.Key, pemissionAbbriation.Value);
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