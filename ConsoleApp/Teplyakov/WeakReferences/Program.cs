using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Teplyakov.WeakReferences
{
    // Кастомный объект с финализатором
    public class Finalizable
    {
        ~Finalizable()
        {
            Console.WriteLine("Finalizable.dtor");
        }
    }

    // Простой трекер, который генерирует событие, когда объект
    // на который указывает слабая ссылка умирает
    public class WeakReferenceTracker
    {
        private readonly WeakReference _wr;

        public WeakReferenceTracker(object o, bool trackResurection)
        {
            _wr = new WeakReference(o, trackResurection);

            // Начинаем следить за тем, когда объект умрет!
            Task.Factory.StartNew(TrackDeath);
        }

        public event Action ReferenceDied = () => { };

        // Не слишком надежная реализация, но для наших целей вполне подходящая
        private void TrackDeath()
        {
            while (true)
            {
                if (!_wr.IsAlive)
                {
                    ReferenceDied();
                    break;
                }

                Thread.Sleep(1);
            }
        }
    }

    // Запускать в Release без отладки (Ctrl+F5)
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Creating 2 trackers...");
 
            var finalizable = new Finalizable();
 
            var weakTracker = new WeakReferenceTracker(finalizable, false);
            weakTracker.ReferenceDied +=
                () => Console.WriteLine("Short weak reference is dead");
 
            var resurectionTracker = new WeakReferenceTracker(finalizable, true);
            resurectionTracker.ReferenceDied +=
                () => Console.WriteLine("Long weak reference is dead");
 
            Console.WriteLine("Forcing 0th generation GC...");
            GC.Collect(0);
            Thread.Sleep(100);
 
            Console.WriteLine("Forcing 1th generation GC...");
            GC.Collect(1);
 
            // Это предотвратит уничтожение сборщиком мусора самих трекеров
            GC.KeepAlive(weakTracker);
            GC.KeepAlive(resurectionTracker);
 
            Console.ReadLine();
        }
    }
}