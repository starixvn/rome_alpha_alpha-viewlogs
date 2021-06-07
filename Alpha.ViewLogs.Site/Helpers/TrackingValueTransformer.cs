namespace Alpha.ViewLogs.Site.Helpers
{
    using Alpha.ViewLogs.Constants;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    public static class TrackingValueTransformer
    {
        private static readonly Dictionary<int, Func<string, string>> funcDictionary
            = new Dictionary<int, Func<string, string>>
            {
                [220002] = TransformParlayMaxPayoutTrackingValue,
                [220003] = TransformParlayMaxPayoutTrackingValue
            };

        public static string TransformToStandardTrackingValue(int actionId, string trackingValue)
        {
            if (funcDictionary.ContainsKey(actionId))
            {
                return funcDictionary[actionId](trackingValue);
            }

            return trackingValue;
        }

        private static string TransformParlayMaxPayoutTrackingValue(string trackingValue)
        {
            var ignoredAttributes = new[] { "BetType", "SportType" };
            var trackingValueDoc = new XmlDocument
            {
                InnerXml = trackingValue
            };

            var rootElement = trackingValueDoc.DocumentElement;
            foreach (XmlNode childNode in rootElement.ChildNodes)
            {
                var sportType = Convert.ToInt32(childNode.Attributes["SportType"].Value);
                foreach (XmlAttribute attribute in childNode.Attributes)
                {
                    if (!ignoredAttributes.Contains(attribute.Name, StringComparer.OrdinalIgnoreCase))
                    {
                        rootElement.SetAttribute(
                            FormatParlayMaxPayoutAttributeName(sportType, attribute.Name),
                            attribute.Value);
                    }
                }
            }

            rootElement.InnerXml = string.Empty;
            rootElement.IsEmpty = true;
            return rootElement.OuterXml;
        }

        private static string FormatParlayMaxPayoutAttributeName(int sportType, string attributeName)
        {
            switch (sportType)
            {
                case SportId.SoccerParlay:
                    return string.Format("Soccer{0}", attributeName);

                case SportId.MixSportsParlay:
                    return string.Format("MixSports{0}", attributeName);

                default:
                    return attributeName;
            }
        }
    }
}