namespace Alpha.ViewLogs.Tests.Helpers
{
    using System;
    using NUnit.Framework;
    using ViewLogs.Helpers;

    [TestFixture]
    public class DateTimeHelperTests
    {
        [TestCase(2016, 8, 23, 0, 0, 0)] // Beginning of Date
        [TestCase(2016, 8, 23, 1, 1, 1)]
        [TestCase(2016, 8, 23, 12, 0, 0)] // Mid-date
        [TestCase(2016, 8, 23, 15, 1, 1)]
        [TestCase(2016, 8, 23, 23, 59, 59)] // Ending of date
        public void GetBeginningOfDate_DateTimeInDay_ReturnsBeginningOfDate(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second)
        {
            // Arrange
            var inputDateTime = new DateTime(year, month, day, hour, minute, second);
            var expectedDateTime = new DateTime(year, month, day, 0, 0, 0);

            // Action
            var actualDateTime = DateTimeHelper.GetBeginningOfDate(inputDateTime);

            // Assert
            Assert.AreEqual(expectedDateTime, actualDateTime);
        }

        [TestCase(2016, 8, 23, 0, 0, 0)] // Beginning of Date
        [TestCase(2016, 8, 23, 1, 1, 1)]
        [TestCase(2016, 8, 23, 12, 0, 0)] // Mid-date
        [TestCase(2016, 8, 23, 15, 1, 1)]
        [TestCase(2016, 8, 23, 23, 59, 59)] // Ending of date
        public void GetEndingOfDate_DateTimeInDay_ReturnsEndingOfDate(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second)
        {
            // Arrange
            var inputDateTime = new DateTime(year, month, day, hour, minute, second);
            var expectedDateTime = new DateTime(year, month, day, 23, 59, 59);

            // Action
            var actualDateTime = DateTimeHelper.GetEndingOfDate(inputDateTime);

            // Assert
            Assert.AreEqual(expectedDateTime, actualDateTime);
        }

        [TestCase(2016, 8, 23, 0, 0, 0, "08/23/2016 12:00:00 AM")] // Beginning of Date
        [TestCase(2016, 8, 23, 1, 1, 1, "08/23/2016 01:01:01 AM")]
        [TestCase(2016, 8, 23, 12, 0, 0, "08/23/2016 12:00:00 PM")] // Mid-date
        [TestCase(2016, 8, 23, 15, 1, 1, "08/23/2016 03:01:01 PM")]
        [TestCase(2016, 8, 23, 23, 59, 59, "08/23/2016 11:59:59 PM")] // Ending of date
        public void ToLongDateWithSlash_DateTimeInDay_ReturnsLongDateWithSlash(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second,
            string expectedValue)
        {
            // Arrange
            var inputDateTime = new DateTime(year, month, day, hour, minute, second);

            // Action
            var actualDateTime = DateTimeHelper.ToLongDateWithSlash(inputDateTime);

            // Assert
            Assert.AreEqual(expectedValue, actualDateTime);
        }

        [TestCase(2016, 8, 23, 0, 0, 0, "08/23/2016<br/>12:00:00 AM")] // Beginning of Date
        [TestCase(2016, 8, 23, 1, 1, 1, "08/23/2016<br/>01:01:01 AM")]
        [TestCase(2016, 8, 23, 12, 0, 0, "08/23/2016<br/>12:00:00 PM")] // Mid-date
        [TestCase(2016, 8, 23, 15, 1, 1, "08/23/2016<br/>03:01:01 PM")]
        [TestCase(2016, 8, 23, 23, 59, 59, "08/23/2016<br/>11:59:59 PM")] // Ending of date
        public void ToHtmlLongDateWithSlash_DateTimeInDay_ReturnsHtmlLongDateWithSlash(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second,
            string expectedValue)
        {
            // Arrange
            var inputDateTime = new DateTime(year, month, day, hour, minute, second);

            // Action
            var actualDateTime = DateTimeHelper.ToHtmlLongDateWithSlash(inputDateTime);

            // Assert
            Assert.AreEqual(expectedValue, actualDateTime);
        }
    }
}
