using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTask.Common;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static OnTask.Common.Enumerations;

namespace OnTask.Test.Common
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ExtensionsTest
    {
        #region Tests
        [TestMethod]
        [DataTestMethod]
        [DataRow("03:00", "01:00", "02:00", false, DisplayName = "Not Between")]
        [DataRow("02:00", "03:00", "01:00", false, DisplayName = "Between Incorrect Times")]
        [DataRow("01:00", "03:00", "02:00", false, DisplayName = "Not Between Incorrect Times")]
        [DataRow("02:00", "01:00", "03:00", true, DisplayName = "Exclusive Between")]
        [DataRow("01:00", "01:00", "02:00", true, DisplayName = "Left Inclusive Between")]
        [DataRow("02:00", "01:00", "02:00", true, DisplayName = "Right Inclusive Between")]
        public void Between(string targetText, string startText, string endText, bool expected)
        {
            var target = TimeSpan.Parse(targetText);
            var start = TimeSpan.Parse(startText);
            var end = TimeSpan.Parse(endText);

            var actual = target.Between(start, end);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("2016-01-01", "2016-12-31", 366, DisplayName = "A Leap Year of Dates")]
        [DataRow("2017-01-01", "2017-12-31", 365, DisplayName = "A Year of Dates")]
        [DataRow("2018-01-01", "2018-01-07", 7, DisplayName = "Multiple Dates")]
        [DataRow("2018-01-01 00:00:00.000", "2018-01-07 23:59:59.999", 7, DisplayName = "Multiple Dates with Times")]
        [DataRow("2018-01-01", "2018-01-01", 1, DisplayName = "Single Date")]
        public void GetDateRange(string startText, string endText, int expectedNumberOfDays)
        {
            var start = DateTime.Parse(startText);
            var end = DateTime.Parse(endText);

            var actual = Extensions.GetDateRange(start, end);
            var actualNumberOfDays = actual.Count();

            Assert.AreEqual(expectedNumberOfDays, actualNumberOfDays);
            Assert.AreEqual(start.Date, actual.First().Date);
            Assert.AreEqual(end.Date, actual.Last().Date);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("2018-03-04", DaysOfWeek.Sunday, DisplayName = "Sunday")]
        [DataRow("2018-03-05", DaysOfWeek.Monday, DisplayName = "Monday")]
        [DataRow("2018-03-06", DaysOfWeek.Tuesday, DisplayName = "Tuesday")]
        [DataRow("2018-03-07", DaysOfWeek.Wednesday, DisplayName = "Wednesday")]
        [DataRow("2018-03-08", DaysOfWeek.Thursday, DisplayName = "Thursday")]
        [DataRow("2018-03-09", DaysOfWeek.Friday, DisplayName = "Friday")]
        [DataRow("2018-03-10", DaysOfWeek.Saturday, DisplayName = "Saturday")]
        public void GetDaysOfWeek(string dateText, DaysOfWeek expected)
        {
            var date = DateTime.Parse(dateText);

            var actual = date.GetDaysOfWeek();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(1, 1, true, DisplayName = "Matching Parameters")]
        [DataRow(1, 2, false, DisplayName = "Mismatching Parameters")]
        public void IsParameterNullOrEqual_Integer(int x, int y, bool expected)
        {
            var actual = x.IsParameterNullOrEqual(y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(1, 1, true, DisplayName = "Matching Parameters")]
        [DataRow(null, null, true, DisplayName = "Both Parameters Null")]
        [DataRow(1, null, true, DisplayName = "Second Parameter Null")]
        [DataRow(1, 2, false, DisplayName = "Mismatching Parameters")]
        [DataRow(null, 1, false, DisplayName = "First Parameter Null")]
        public void IsParameterNullOrEqual_NullableInteger(int? x, int? y, bool expected)
        {
            var actual = x.IsParameterNullOrEqual(y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("foo", "foo", true, DisplayName = "Matching Parameters")]
        [DataRow(null, null, true, DisplayName = "Both Parameters Null")]
        [DataRow("foo", null, true, DisplayName = "Second Parameter Null")]
        [DataRow("foo", "bar", false, DisplayName = "Mismatching Parameters")]
        [DataRow(null, "foo", false, DisplayName = "First Parameter Null")]
        public void IsParameterNullOrEqual_String(string x, string y, bool expected)
        {
            var actual = x.IsParameterNullOrEqual(y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(1.5, 1.5, true, DisplayName = "Matching Parameters")]
        [DataRow(1.5, null, true, DisplayName = "Second Parameter Null")]
        [DataRow(1.5, 2.5, false, DisplayName = "Mismatching Parameters")]
        public void IsParameterNullOrEqualForNonNullable_Double(double x, double? y, bool expected)
        {
            var actual = x.IsParameterNullOrEqualForNonNullable(y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(1, 1, true, DisplayName = "Matching Parameters")]
        [DataRow(1, null, true, DisplayName = "Second Parameter Null")]
        [DataRow(1, 2, false, DisplayName = "Mismatching Parameters")]
        public void IsParameterNullOrEqualForNonNullable_Integer(int x, int? y, bool expected)
        {
            var actual = x.IsParameterNullOrEqualForNonNullable(y);
            Assert.AreEqual(expected, actual);
        } 
        #endregion
    }
}
