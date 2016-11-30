using System;

namespace CustomParameters.Model
{
    public abstract class CustomParameterValue
    {
        public DateTime Date { get; set; }

        public abstract object ObjectValue { get; set; }

        public bool IsActive { get; set; }
    }

    public class CustomParameterValue<TValue> : CustomParameterValue
    {
        public TValue Value { get; set; }

        public override object ObjectValue {
            get { return Value; }
            set { Value = (TValue) value; }
        }
    }


}