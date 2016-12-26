namespace Tests45.Speed.NestedObjects
{
    public class A2
    {
        public A2()
        {
            A3 = new A3();
        }

        public A3 A3 { get; set; }  
    }
}