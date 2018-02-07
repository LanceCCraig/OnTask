using Microsoft.VisualStudio.TestTools.UnitTesting;
using Omu.ValueInjecter;
using OnTask.Common.Injections;
using OnTask.Test.Common.Injections.Models;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Common.Injections
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class NullableInjectionTest
    {
        [TestMethod]
        public void InjectFrom_NonNullableToNonNullable()
        {
            var source = new NonNullableModel(1D, 2, "3");
            var target = new NonNullableModel(4D, 5, "6");
            var expected = new NonNullableModel(source);

            var actual = (NonNullableModel)target.InjectFrom<NullableInjection>(source);

            Assert.AreEqual(expected.Double, actual.Double);
            Assert.AreEqual(expected.Integer, actual.Integer);
            Assert.AreEqual(expected.String, actual.String);
        }

        [TestMethod]
        public void InjectFrom_NonNullableToNullable()
        {
            var source = new NonNullableModel(1D, 2, "3");
            var target = new NullableModel(null, null, null);
            var expected = new NullableModel(1D, 2, "3");

            var actual = (NullableModel)target.InjectFrom<NullableInjection>(source);

            Assert.AreEqual(expected.Double, actual.Double);
            Assert.AreEqual(expected.Integer, actual.Integer);
            Assert.AreEqual(expected.String, actual.String);
        }

        [TestMethod]
        public void InjectFrom_NullableToNonNullable()
        {
            var source = new NullableModel(null, 2, null);
            var target = new NonNullableModel(4D, 5, "6");
            var expected = new NonNullableModel(default(double), 2, default(string));

            var actual = (NonNullableModel)target.InjectFrom<NullableInjection>(source);

            Assert.AreEqual(expected.Double, actual.Double);
            Assert.AreEqual(expected.Integer, actual.Integer);
            Assert.AreEqual(expected.String, actual.String);
        }

        [TestMethod]
        public void InjectFrom_NullableToNullable()
        {
            var source = new NullableModel(1D, null, null);
            var target = new NullableModel(4D, 5, "6");
            var expected = new NullableModel(1D, null, null);

            var actual = (NullableModel)target.InjectFrom<NullableInjection>(source);

            Assert.AreEqual(expected.Double, actual.Double);
            Assert.AreEqual(expected.Integer, actual.Integer);
            Assert.AreEqual(expected.String, actual.String);
        }
    }
}
