using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Common.Injections.Models
{
    [ExcludeFromCodeCoverage]
    public class NonNullableModel
    {
        #region Fields
        private double doubleField;
        private int integerField;
        private string stringField;
        #endregion

        #region Properties
        public double Double
        {
            get => doubleField;
            set
            {
                doubleField = value;
                DoubleChanged = true;
            }
        }
        public bool DoubleChanged { get; private set; }
        public int Integer
        {
            get => integerField;
            set
            {
                integerField = value;
                IntegerChanged = true;
            }
        }
        public bool IntegerChanged { get; private set; }
        public string String
        {
            get => stringField;
            set
            {
                stringField = value;
                StringChanged = true;
            }
        }
        public bool StringChanged { get; private set; }
        #endregion

        #region Initialization
        public NonNullableModel()
            : this(default(double), default(int), default(string))
        {
        }

        public NonNullableModel(double doubleField, int integerField, string stringField)
        {
            this.doubleField = doubleField;
            this.integerField = integerField;
            this.stringField = stringField;
        }

        public NonNullableModel(NonNullableModel other)
            : this(other.Double, other.Integer, other.String)
        {
        }
        #endregion
    }
}
