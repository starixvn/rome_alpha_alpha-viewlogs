namespace Alpha.ViewLogs.Site.Helpers
{
    using Alpha.ViewLogs.Models;
    using Constants;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Xml;

    public class BaseLogViewModelHelper
    {
        public static string GetTrackingValueWithDiff(BaseLog baseLog, bool isShowOldValue)
        {
            if (baseLog.TrackingValue == null)
            {
                throw new Exception("TrackingValue is null.");
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

                var isPermissionAttribute = IsPermissionValue(oldValue) || IsPermissionValue(newValue);
                var isFormatAttribute = isPermissionAttribute ||
                    (!string.Equals(newValue, oldValue, StringComparison.OrdinalIgnoreCase) && !ignoreFormatAttributes.Contains(newAttribute.Name.ToLower()) &&
                    (!string.IsNullOrEmpty(newValue) || !string.IsNullOrEmpty(oldValue)));
                if (!isFormatAttribute)
                {
                    continue;
                }

                newValue = StandardizeAttributeValue(newValue);
                oldValue = StandardizeAttributeValue(oldValue);

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
            var standardizedAttributeValue = attrValue;
            var isPermissionAttribute = IsPermissionValue(attrValue);

            var pemissionAbbriations = new Dictionary<string, string>();
            pemissionAbbriations.Add("[A][B][H]", "[A]");
            pemissionAbbriations.Add("[A][B]", "[A]");
            pemissionAbbriations.Add("[B][H]", "[B]");

            if (isPermissionAttribute)
            {
                standardizedAttributeValue = standardizedAttributeValue.ToUpper();

                foreach (var pemissionAbbriation in pemissionAbbriations)
                {
                    standardizedAttributeValue = standardizedAttributeValue.Replace(pemissionAbbriation.Key, pemissionAbbriation.Value);
                }

                var pageAccessTypeNames = PageAccessType.PageAccessTypeName();
                foreach (var pageAccessTypeName in pageAccessTypeNames)
                {
                    if (standardizedAttributeValue.Contains(pageAccessTypeName.Key))
                    {
                        standardizedAttributeValue = standardizedAttributeValue.Replace(pageAccessTypeName.Key, "[" + pageAccessTypeName.Value + "]");
                    }
                }
            }

            standardizedAttributeValue = HttpUtility.HtmlEncode(standardizedAttributeValue);

            return standardizedAttributeValue;
        }
    }
}