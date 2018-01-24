using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTask.Common;

namespace OnTask.Test.Common
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void IsParameterNullOrEqual_IntegerParametersAreEqual_ReturnsTrue()
        {
            var x = 1;
            var y = 1;

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_IntegerParametersAreNotEqual_ReturnsFalse()
        {
            var x = 1;
            var y = 2;

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_NullableIntegerBothParametersAreNull_ReturnsTrue()
        {
            int? x = null;
            int? y = null;

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_NullableIntegerFirstParameterIsNull_ReturnsFalse()
        {
            int? x = null;
            int? y = 1;

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_NullableIntegerParametersAreEqual_ReturnsTrue()
        {
            int? x = 1;
            int? y = 1;

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_NullableIntegerParametersAreNotEqual_ReturnsFalse()
        {
            int? x = 1;
            int? y = 2;

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_NullableIntegerSecondParameterIsNull_ReturnsTrue()
        {
            int? x = 1;
            int? y = null;

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_StringBothParametersAreNull_ReturnsTrue()
        {
            var x = default(string);
            var y = default(string);

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_StringFirstParameterIsNull_ReturnsFalse()
        {
            var x = default(string);
            var y = "foo";

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_StringParametersAreEqual_ReturnsTrue()
        {
            var x = "foo";
            var y = "foo";

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_StringParametersAreNotEqual_ReturnsFalse()
        {
            var x = "foo";
            var y = "bar";

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsParameterNullOrEqual_StringSecondParameterIsNull_ReturnsTrue()
        {
            var x = "foo";
            var y = default(string);

            var actual = x.IsParameterNullOrEqual(y);

            Assert.IsTrue(actual);
        }
    }
}
