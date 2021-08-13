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
                [220000] = TransformParlayMaxPayoutTrackingValue,
                [220001] = TransformParlayMaxPayoutTrackingValue,
                [220002] = TransformParlayMaxPayoutTrackingValue,
                [220003] = TransformParlayMaxPayoutTrackingValue,
                [220004] = TransformParlayMaxPayoutTrackingValue,
                [220005] = TransformParlayMaxPayoutTrackingValue,
                [220006] = TransformParlayMaxPayoutTrackingValue,
                [220020] = TransformBetSettingTrackingValue,
                [220021] = TransformBetSettingTrackingValue,
                [220022] = TransformBetSettingTrackingValue,
                [220023] = TransformBetSettingTrackingValue,
                [220024] = TransformBetSettingTrackingValue,
                [220025] = TransformBetSettingTrackingValue,
                [220026] = TransformBetSettingTrackingValue
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
            var trackingValueDoc = new XmlDocument
            {
                InnerXml = trackingValue
            };

            var rootElement = trackingValueDoc.DocumentElement;
            foreach (XmlNode childNode in rootElement.ChildNodes)
            {
                var sportType = Convert.ToInt32(childNode.Attributes[AttributeName.SportType].Value);
                foreach (XmlAttribute attribute in childNode.Attributes)
                {
                    if (!AttributeName.IgnoredAttributes.Contains(attribute.Name, StringComparer.OrdinalIgnoreCase))
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

        private static string TransformBetSettingTrackingValue(string trackingValue)
        {
            var trackingValueDoc = new XmlDocument
            {
                InnerXml = trackingValue
            };

            var rootElement = trackingValueDoc.DocumentElement;
            foreach (XmlNode childNode in rootElement.ChildNodes)
            {
                var betType = Convert.ToInt32(childNode.Attributes[AttributeName.BetType].Value);
                var sportType = Convert.ToInt32(childNode.Attributes[AttributeName.SportType].Value);
                foreach (XmlAttribute attribute in childNode.Attributes)
                {
                    if (!AttributeName.IgnoredAttributes.Contains(attribute.Name, StringComparer.OrdinalIgnoreCase))
                    {
                        rootElement.SetAttribute(
                            FormatBetSettingAttributeName(betType, sportType, attribute.Name),
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

        private static string FormatBetSettingAttributeName(int betType, int sportType, string attributeName)
        {
            var shortenedAttributeName = ShortenAttributeName(attributeName);
            switch (sportType)
            {
                case SportId.SoccerParlay:
                    return betType == BetTypeId.Parlay
                        ? string.Concat(shortenedAttributeName, "v", sportType, "mp")
                        : shortenedAttributeName;

                case SportId.MixSportsParlay:
                    return betType == BetTypeId.Parlay
                        ? string.Concat(shortenedAttributeName, "v", sportType, "mp")
                        : string.Concat(shortenedAttributeName, "v", sportType);

                default:
                    return string.Concat(shortenedAttributeName, "v", sportType);
            }
        }

        private static string ShortenAttributeName(string attributeName)
        {
            switch (attributeName)
            {
                case AttributeName.MinBet:
                    return "minb";

                case AttributeName.MaxBet:
                    return "maxb";

                case AttributeName.MaxPerMatch:
                    return "mpm";

                case AttributeName.MaxPayoutPerMatch:
                    return "mppm";

                case AttributeName.MaxPerNumber:
                    return "mpb";

                default:
                    return attributeName;
            }
        }
    }
}