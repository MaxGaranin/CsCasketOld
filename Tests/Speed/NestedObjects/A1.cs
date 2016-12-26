namespace Tests45.Speed.NestedObjects
{
    public class A1
    {
        public A1()
        {
            A2 = new A2();
        }

        public A2 A2 { get; set; }

        public double Value { get; set; }
    }
}