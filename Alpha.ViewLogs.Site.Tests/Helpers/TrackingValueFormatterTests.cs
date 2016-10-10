namespace Alpha.ViewLogs.Site.Tests.Helpers
{
    using System.Xml;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Helpers;

    [TestFixture]
    public class TrackingValueFormatterTests
    {
        private TrackingValueFormatterImpl trackingValueFormatter;

        [SetUp]
        public void SetUp()
        {
            trackingValueFormatter = new TrackingValueFormatterImpl();
        }

        [Test]
        public void GetFormattedDiffTrackingValue_NullLog_ReturnsEmpty()
        {
            // Arrange
            Log nullLog = null;

            // Action
            var actualTrackingValue = trackingValueFormatter.GetFormattedDiffTrackingValue(nullLog, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(actualTrackingValue);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void GetFormattedDiffTrackingValue_LogWithNullOrWhiteSpaceTrackingValue_ReturnsEmpty(string trackingValue)
        {
            // Arrange
            var log = new Log
            {
                TrackingValue = trackingValue
            };

            // Action
            var actualTrackingValue = trackingValueFormatter.GetFormattedDiffTrackingValue(log, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(actualTrackingValue);
        }

        [TestCase("<></>")]
        [TestCase("<div>")]
        [TestCase("</div>")]
        [TestCase("<div></tag>")]
        [TestCase("<div>content</tag>")]
        public void GetFormattedDiffTrackingValue_LogWithWrongXMLFormattedTrackingValue_ThrowsXmlException(string trackingValue)
        {
            // Arrange
            var log = new Log
            {
                TrackingValue = trackingValue
            };

            // Action & Assert
            Assert.Throws<XmlException>(new TestDelegate(() => trackingValueFormatter.GetFormattedDiffTrackingValue(log, Arg.Any<bool>())));
        }

        [TestCase("<div>content</div>", "<div>content</div>")]
        [TestCase("<div ip=\"ValidIP\"></div>", "<div ip=\"ValidIP\"></div>")]
        [TestCase("<div loginip=\"ValidIP\"></div>", "<div loginip=\"ValidIP\"></div>")]
        [TestCase("<div stakelevel1=\"ValidStakelevel\"></div>", "<div stakelevel1=\"ValidStakelevel\"></div>")]
        [TestCase("<div formatAttribute=\"\"></div>", "<div formatAttribute=\"\"></div>")]
        [TestCase("<div formatAttribute=\" \"></div>", "<div formatAttribute=\" \"></div>")]
        [TestCase("<div formatAttribute=\"  \"></div>", "<div formatAttribute=\"  \"></div>")]
        [TestCase("<div><subnode>content</subnode></div>", "<div><subnode>content</subnode></div>")]
        public void GetFormattedDiffTrackingValue_LogWithXMLFormattedTrackingValueAndNotHaveTrackingValueOld_ReturnsExpectedTrackingValue(
            string trackingValue,
            string expectedTrackingValue)
        {
            // Arrange
            var log = new Log
            {
                TrackingValue = trackingValue
            };

            // Action
            var actualTrackingValue = trackingValueFormatter.GetFormattedDiffTrackingValue(log, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(expectedTrackingValue, actualTrackingValue);
        }
    }
}
