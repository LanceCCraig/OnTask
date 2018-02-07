using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTask.Business.Models.Event;
using OnTask.Business.Validators.Event;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Business.Validators.Event
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EventGroupDeleteMultipleModelValidatorTest
    {
        #region Fields
        private readonly IValidator<EventGroupDeleteMultipleModel> target = new EventGroupDeleteMultipleModelValidator();
        #endregion

        #region Tests
        [TestMethod]
        public void Validate_InvalidEventParentId()
        {
            var eventParentId = default(int);

            target.ShouldHaveValidationErrorFor(x => x.EventParentId, eventParentId);
        }

        [TestMethod]
        public void Validate_ValidModel()
        {
            var model = new EventGroupDeleteMultipleModel
            {
                EventParentId = 1
            };

            var result = target.Validate(model);

            Assert.IsTrue(result.IsValid);
            target.ShouldNotHaveValidationErrorFor(x => x.EventParentId, model);
        }
        #endregion
    }
}
