namespace Alpha.ViewLogs.Site.Tests.Helpers
{
    using Alpha.ViewLogs.Site.Helpers;
    using NUnit.Framework;

    [TestFixture]
    public class TrackingValueTransformerTests
    {
        [TestCase(0, "", "")]
        [TestCase(1, "Any tracking value", "Any tracking value")]
        [TestCase(220002, @"<Acc_CustSetting_MaxPayoutUpd " +
            "CustID=\"14373487\" " +
            "UserID=\"1176070\" " +
            "SourceAccount=\"masterkz\" " +
            "IP=\"10.19.10.81\">" +
            "<MaxPayoutLimit SportType=\"1\" BetType=\"9\" SingleMatchParlay=\"100.00\" Combo2=\"150000.00\" Combo35=\"500000.00\" ComboOver6=\"2000000.00\"/>" +
            "<MaxPayoutLimit SportType=\"99\" BetType=\"9\" SingleMatchParlay=\"300000.00\" Combo2=\"340000.00\" Combo35=\"600000.00\" ComboOver6=\"1000000.00\"/>" +
            "</Acc_CustSetting_MaxPayoutUpd>",
            @"<Acc_CustSetting_MaxPayoutUpd " +
            "CustID=\"14373487\" " +
            "UserID=\"1176070\" " +
            "SourceAccount=\"masterkz\" " +
            "IP=\"10.19.10.81\" " +
            "SoccerSingleMatchParlay=\"100.00\" SoccerCombo2=\"150000.00\" SoccerCombo35=\"500000.00\" SoccerComboOver6=\"2000000.00\" " +
            "MixSportsSingleMatchParlay=\"300000.00\" MixSportsCombo2=\"340000.00\" MixSportsCombo35=\"600000.00\" MixSportsComboOver6=\"1000000.00\" " +
            "/>")]
        [TestCase(220003, @"<Acc_CustSetting_MaxPayoutUpd " +
            "CustID=\"14373487\" " +
            "UserID=\"1176070\" " +
            "SourceAccount=\"masterkz\" " +
            "IP=\"10.19.10.81\">" +
            "<MaxPayoutLimit SportType=\"1\" BetType=\"9\" SingleMatchParlay=\"100.00\" Combo2=\"150000.00\" Combo35=\"500000.00\" ComboOver6=\"2000000.00\"/>" +
            "<MaxPayoutLimit SportType=\"99\" BetType=\"9\" SingleMatchParlay=\"300000.00\" Combo2=\"340000.00\" Combo35=\"600000.00\" ComboOver6=\"1000000.00\"/>" +
            "</Acc_CustSetting_MaxPayoutUpd>",
            @"<Acc_CustSetting_MaxPayoutUpd " +
            "CustID=\"14373487\" " +
            "UserID=\"1176070\" " +
            "SourceAccount=\"masterkz\" " +
            "IP=\"10.19.10.81\" " +
            "SoccerSingleMatchParlay=\"100.00\" SoccerCombo2=\"150000.00\" SoccerCombo35=\"500000.00\" SoccerComboOver6=\"2000000.00\" " +
            "MixSportsSingleMatchParlay=\"300000.00\" MixSportsCombo2=\"340000.00\" MixSportsCombo35=\"600000.00\" MixSportsComboOver6=\"1000000.00\" " +
            "/>")]
        public void TransformToStandardTrackingValue_Always_ReturnsCorrectValue(
            int actionId,
            string trackingValue,
            string expectedTrackingValue)
        {
            // Act
            var actualTrackingValue = TrackingValueTransformer.TransformToStandardTrackingValue(actionId, trackingValue);

            // Assert
            Assert.AreEqual(expectedTrackingValue, actualTrackingValue);
        }
    }
}