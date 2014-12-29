using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Dynamic;

namespace Test40.ClassesUnderTest
{
    public class DynamicCollection
    {
        private readonly ObservableCollection<dynamic> _items;

        public DynamicCollection()
        {
            Columns = new List<string>();
            _items = new ObservableCollection<dynamic>();
        }

        public IList<string> Columns { get; private set; }

        public void AddColumn(string columnName)
        {
            Columns.Add(columnName);
        }

        public void AddNewItem(ExpandoObject item)
        {
            IDictionary<string, object> newItem = new ExpandoObject();
            IDictionary<string, object> addItem = item;
            foreach (var column in Columns)
            {
                newItem[column] = addItem[column];
            }
            
            _items.Add(newItem);
        }

        public DataTable ToDataTable()
        {
            var table = new DataTable();
            foreach (var column in Columns)
            {
                table.Columns.Add(column);
            }

            foreach (var item in _items)
            {
                var row = table.NewRow();
                foreach (var column in Columns)
                {
                    row[column] = ((IDictionary<string, object>) item)[column];
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}