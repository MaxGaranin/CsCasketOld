using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using NUnit.Framework;

namespace Tests45.CommonTests
{
    [TestFixture]
    public class ReadOnlyObservableCollectionTests
    {
        public class Source
        {
            public Source()
            {
                m_collection = new ObservableCollection<int>();
                m_collectionReadOnly = new ReadOnlyObservableCollection<int>(m_collection);

                m_collection.Add(1);
                m_collection.Add(2);
                m_collection.Add(3);
                m_collection.Add(1);
            }

            public ReadOnlyObservableCollection<int> Items
            {
                get { return m_collectionReadOnly; }
            }

            readonly ObservableCollection<int> m_collection;
            readonly ReadOnlyObservableCollection<int> m_collectionReadOnly;

            public void AddItem(int item)
            {
                m_collection.Add(item);
            }

            public void RemoveItem(int item)
            {
                m_collection.Remove(item);
            }
        }
 
        [Test]
        public void Test()
        {
            var source = new Source();
            Subscribe(source);
            source.AddItem(12);
            source.RemoveItem(1);

            Unsubscribe(source);
            source.AddItem(12);
        }

        void Subscribe(Source source)
        {
            ((INotifyCollectionChanged)source.Items).CollectionChanged += SourceItems_CollectionChanged;
        }

        void Unsubscribe(Source source)
        {
            ((INotifyCollectionChanged)source.Items).CollectionChanged -= SourceItems_CollectionChanged;
        }

        void SourceItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int last = -999;
            if (e.NewItems != null)
            {
                last = (int) e.NewItems[e.NewItems.Count - 1];
            }
            else if (e.OldItems != null)
            {
                last = (int) e.OldItems[e.OldItems.Count - 1];

            }
            Console.WriteLine("Changed, last = {0}", last);
        }
    }
}