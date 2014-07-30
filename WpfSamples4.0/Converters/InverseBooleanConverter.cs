namespace WpfSamples40.Converters
{
    public class InverseBooleanConverter : BooleanConverter<bool>
    {
        public InverseBooleanConverter() : base(false, true)
        {
        }
    }
}