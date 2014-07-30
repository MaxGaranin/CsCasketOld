using NUnit.Framework;

namespace Test40.Fabric
{
    [TestFixture]
    public class FabricTests
    {
        public abstract class ScaleBase
        {
            public string Periods { get; set; }

            public void Initialize()
            {
                FillPeriods();    
            }

            public abstract void FillPeriods();
        }

        public class YearScale : ScaleBase
        {
            private YearScale()
            {
            }

            public static YearScale Create()
            {
                var yearScale = new YearScale();
                yearScale.FillPeriods();
                return yearScale;
            }

            public override void FillPeriods()
            {
                Periods = "Year";
            }
        }

        public class CustomScale : ScaleBase
        {
            private CustomScale()
            {
            }

            public static CustomScale Create()
            {
                var customScale = new CustomScale();
                customScale.FillPeriods();
                return customScale;
            }

            public override void FillPeriods()
            {
                Periods = "Custom";
            }
        }

        // Пока не будет работать, т.к. конструкторы приватные
        public class ScaleFactory
        {
            public static ScaleBase Create<T>() where T : ScaleBase, new()
            {
                T scale = new T();
                scale.Initialize();
                return scale;
            }
        }

        [Test]
        public void Test()
        {
            ScaleBase customScale = CustomScale.Create();
            ScaleBase yaerScale = YearScale.Create();
        }
    }
}