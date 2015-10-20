using System;

namespace ConsoleApp.WeakEvents
{
    internal class Program
    {
        private static void Main()
        {
//            Test1();
            Test2();
        }

        private static void Test1()
        {
            var shortLived = new ShortLivedEventHandler();
            // Подписываемся на событие обычным образом!
            shortLived.Subscribe();
            // Создаем слабую ссылку, чтобы отслеживать время жизни короткоживущего объекта
            var firstWeakReference = new WeakReference(shortLived, false);

            Console.WriteLine("Зажигаем событие");
            LongLivedEventProvider.Instance.RaiseEvent();

            Console.WriteLine("А жив ли обджект? " + firstWeakReference.IsAlive);

            Console.WriteLine("Собираем мусор");
            GC.Collect();

            Console.WriteLine("А жив ли обджек? " + firstWeakReference.IsAlive);
        }

        private static void Test2()
        {
            var shortLived = new ShortLivedEventHandler();
            var firstWeakReference = new WeakReference(shortLived, false);

            shortLived.SubscribeWeakly();
            LongLivedEventProvider.Instance.RaiseEvent();

            GC.Collect();
            // Теперь свойство IsAlive вернет false!
            Console.WriteLine("А жив ли обджект? " + firstWeakReference.IsAlive);            
        }
    }
}