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
        [TestCase(220022, @"<Acc_CustomerUpd_BetSetting " +
            "custid=\"50860452\" UserID=\"50860443\" SubAccName=\"\" Language=\"E\"  IP=\"10.19.10.81\" UpdateMultiple=\"0\">" +
            "<BetLimit SportType=\"1\" BetType=\"1\" MinBet=\"5.00\" MaxBet=\"13.00\" MaxPerMatch=\"100.00\" MaxPayoutPerMatch=\"100.00\"/>" +
            "<BetLimit SportType=\"9901\" BetType=\"1\" MinBet=\"5.00\" MaxBet=\"13.00\" MaxPerMatch=\"100.00\" MaxPayoutPerMatch=\"100.00\"/>" +
            "<BetLimit SportType=\"1\" BetType=\"9\" MinBet=\"19.00\" MaxBet=\"190.00\" MaxPerMatch=\"1900.00\" MaxPayoutPerMatch=\"100.00\"/>" +
            "<BetLimit SportType=\"2\" BetType=\"1\" MinBet=\"5.00\" MaxBet=\"20.00\" MaxPerMatch=\"200.00\" MaxPayoutPerMatch=\"200.00\"/>" +
            "<BetLimit SportType=\"9902\" BetType=\"1\" MinBet=\"5.00\" MaxBet=\"20.00\" MaxPerMatch=\"200.00\" MaxPayoutPerMatch=\"200.00\"/>" +
            "<BetLimit SportType=\"3\" BetType=\"1\" MinBet=\"3.00\" MaxBet=\"30.00\" MaxPerMatch=\"300.00\" MaxPayoutPerMatch=\"300.00\"/>" +
            "<BetLimit SportType=\"5\" BetType=\"1\" MinBet=\"5.00\" MaxBet=\"50.00\" MaxPerMatch=\"500.00\" MaxPayoutPerMatch=\"500.00\"/>" +
            "<BetLimit SportType=\"8\" BetType=\"1\" MinBet=\"8.00\" MaxBet=\"80.00\" MaxPerMatch=\"800.00\" MaxPayoutPerMatch=\"800.00\"/>" +
            "<BetLimit SportType=\"10\" BetType=\"1\" MinBet=\"10.00\" MaxBet=\"100.00\" MaxPerMatch=\"1000.00\" MaxPayoutPerMatch=\"1000.00\"/>" +
            "<BetLimit SportType=\"11\" BetType=\"1\" MinBet=\"11.00\" MaxBet=\"110.00\" MaxPerMatch=\"1100.00\" MaxPayoutPerMatch=\"1100.00\"/>" +
            "<BetLimit SportType=\"99\" BetType=\"1\" MinBet=\"99.00\" MaxBet=\"990.00\" MaxPerMatch=\"9900.00\" MaxPayoutPerMatch=\"9900.00\"/>" +
            "<BetLimit SportType=\"99\" BetType=\"9\" MinBet=\"91.00\" MaxBet=\"910.00\" MaxPerMatch=\"9100.00\" MaxPayoutPerMatch=\"100.00\"/>" +
            "<BetLimit SportType=\"161\" BetType=\"81\" MinBet=\"25.00\" MaxBet=\"210.00\" MaxPerMatch=\"2220.00\" MaxPerNumber=\"2220.00\"/>" +
            "<BetLimit SportType=\"164\" BetType=\"8101\" MinBet=\"11.00\" MaxBet=\"110.00\" MaxPerMatch=\"1000.00\"/>" +
            "</Acc_CustomerUpd_BetSetting>",
            @"<Acc_CustomerUpd_BetSetting " +
            "custid=\"50860452\" UserID=\"50860443\" SubAccName=\"\" Language=\"E\" IP=\"10.19.10.81\" UpdateMultiple=\"0\" " +
            "minb=\"5.00\" maxb=\"13.00\" mpm=\"100.00\" mppm=\"100.00\" " +
            "minbv1saba=\"5.00\" maxbv1saba=\"13.00\" mpmv1saba=\"100.00\" mppmv1saba=\"100.00\" " +
            "minbv1mp=\"19.00\" maxbv1mp=\"190.00\" mpmv1mp=\"1900.00\" mppmv1mp=\"100.00\" " +
            "minbv2=\"5.00\" maxbv2=\"20.00\" mpmv2=\"200.00\" mppmv2=\"200.00\" " +
            "minbv2saba=\"5.00\" maxbv2saba=\"20.00\" mpmv2saba=\"200.00\" mppmv2saba=\"200.00\" " +
            "minbv3=\"3.00\" maxbv3=\"30.00\" mpmv3=\"300.00\" mppmv3=\"300.00\" " +
            "minbv5=\"5.00\" maxbv5=\"50.00\" mpmv5=\"500.00\" mppmv5=\"500.00\" " +
            "minbv8=\"8.00\" maxbv8=\"80.00\" mpmv8=\"800.00\" mppmv8=\"800.00\" " +
            "minbv10=\"10.00\" maxbv10=\"100.00\" mpmv10=\"1000.00\" mppmv10=\"1000.00\" " +
            "minbv11=\"11.00\" maxbv11=\"110.00\" mpmv11=\"1100.00\" mppmv11=\"1100.00\" " +
            "minbv99=\"99.00\" maxbv99=\"990.00\" mpmv99=\"9900.00\" mppmv99=\"9900.00\" " +
            "minbv99mp=\"91.00\" maxbv99mp=\"910.00\" mpmv99mp=\"9100.00\" mppmv99mp=\"100.00\" " +
            "minbv161=\"25.00\" maxbv161=\"210.00\" mpmv161=\"2220.00\" mpbv161=\"2220.00\" " +
            "minbv164=\"11.00\" maxbv164=\"110.00\" mpmv164=\"1000.00\" " +
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