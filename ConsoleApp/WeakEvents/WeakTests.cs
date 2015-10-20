using System;

namespace ConsoleApp.WeakEvents
{
    internal class LongLivedEventProvider
    {
        public static readonly LongLivedEventProvider Instance =
            new LongLivedEventProvider();

        public event EventHandler<EventArgs> Event;

        public void RaiseEvent()
        {
            if (Event != null) 
                Event.Invoke(this, EventArgs.Empty);
        }
    }

    internal class ShortLivedEventHandler
    {
        public void Subscribe()
        {
            EventHandler<EventArgs> handler = (sender, args) => EventHandler();
            // Отсутствие отписки от события приведет к утеччке памяти
            LongLivedEventProvider.Instance.Event += handler;
        }

        public void SubscribeWeakly()
        {
            // Не используем 'this' в лямбде.
            // Неявно захваченный 'this' будет строгой ссылкой
            var handler = WeakEventHandler<EventArgs>.Create(
                this, (@this, o, args) => @this.EventHandler());

            // Теперь можно и не отписываться от события.
            // Во всяком случае текущий объект будет собран сборщиком
            LongLivedEventProvider.Instance.Event += handler;
        }

        private void EventHandler()
        {
            Console.WriteLine("Обрабатываем событие!");
        }
    }

    internal static class WeakEventHandler<TArgs>
    {
        public static EventHandler<TArgs> Create<THandler>(
            THandler handler, Action<THandler, object, TArgs> invoker)
            where THandler : class
        {
            var weakEventHandler = new WeakReference<THandler>(handler);

            return (sender, args) =>
            {
                THandler thandler;
                if (weakEventHandler.TryGetTarget(out thandler))
                {
                    invoker(thandler, sender, args);
                }
            };
        }
    }
}