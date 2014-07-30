using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NUnit.Framework;

namespace Test40.ObservableNotifiableCollection
{
    [TestFixture]
    public class ObservableNotifiableCollectionTests
    {
        [Test]
        public void Test()
        {
            var list = new List<MyObject>
                {
                    new MyObject() { Name = "a", Correlation = new Correlation() {Value = 1}},
                    new MyObject() { Name = "b", Correlation = new Correlation() {Value = 2}},
                    new MyObject() { Name = "c", Correlation = new Correlation() {Value = 3}},
                };

            var obsCol = ToObservableNotifiableCollection(list);

            obsCol.ItemPropertyChanged += (s, e) =>
                {
                    
                };

            // После этого obsCol.ItemPropertyChanged НЕ сработает
            list[1].Correlation.Value = 999;

            Assert.AreEqual(999, obsCol[1].Correlation.Value);

            // После этого obsCol.ItemPropertyChanged сработает
            list[1].Correlation = new Correlation();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> coll)
        {
            return new ObservableCollection<T>(coll);
        }

        public static ObservableNotifiableCollection<T> ToObservableNotifiableCollection<T>(IEnumerable<T> coll) where T : INotifyPropertyChanged
        {
            var c = new ObservableNotifiableCollection<T>();
            foreach (var e in coll)
                c.Add(e);
            return c;
        }
    }
}
