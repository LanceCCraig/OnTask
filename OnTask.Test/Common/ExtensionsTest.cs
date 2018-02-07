using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTask.Common;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Common
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ExtensionsTest
    {
        #region Tests
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
