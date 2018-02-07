using Microsoft.VisualStudio.TestTools.UnitTesting;
using Omu.ValueInjecter;
using OnTask.Common.Injections;
using OnTask.Test.Common.Injections.Models;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Common.Injections
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SmartInjectionTest
    {
        #region Tests
        [TestMethod]
        public void InjectFrom_DifferentModels_InjectsProperties()
        {
            var source = new NullableModel(null, null, null);
            var target = new NonNullableModel(default(double), 1, string.Empty);
            var expected = new NonNullableModel(default(double), default(int), default(string));

            var actual = (NonNullableModel)target.InjectFrom<SmartInjection>(source);

            Assert.AreEqual(expected.Double, actual.Double);
            Assert.AreEqual(expected.Integer, actual.Integer);
            Assert.AreEqual(expected.String, actual.String);
            Assert.IsTrue(actual.DoubleChanged);
            Assert.IsTrue(actual.IntegerChanged);
            Assert.IsTrue(actual.StringChanged);
        }

        [TestMethod]
        public void InjectFrom_IdenticalModels_DoesNotInjectProperties()
        {
            var source = new NonNullableModel(1D, 1, "1");
            var target = new NonNullableModel(1D, 1, "1");
            var expected = new NonNullableModel(source);

            var actual = (NonNullableModel)target.InjectFrom<SmartInjection>(source);

            Assert.AreEqual(expected.Double, actual.Double);
            Assert.AreEqual(expected.Integer, actual.Integer);
            Assert.AreEqual(expected.String, actual.String);
            Assert.IsFalse(actual.DoubleChanged);
            Assert.IsFalse(actual.IntegerChanged);
            Assert.IsFalse(actual.StringChanged);
        }

        [TestMethod]
        public void InjectFrom_SlightlyDifferentModels_InjectsSomeProperties()
        {
            var source = new NullableModel(null, 1, null);
            var target = new NonNullableModel(1D, 1, "1");
            var expected = new NonNullableModel(default(double), 1, default(string));

            var actual = (NonNullableModel)target.InjectFrom<SmartInjection>(source);

            Assert.AreEqual(expected.Double, actual.Double);
            Assert.AreEqual(expected.Integer, actual.Integer);
            Assert.AreEqual(expected.String, actual.String);
            Assert.IsTrue(actual.DoubleChanged);
            Assert.IsFalse(actual.IntegerChanged);
            Assert.IsTrue(actual.StringChanged);
        } 
        #endregion
    }
}
