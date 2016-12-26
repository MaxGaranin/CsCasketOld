using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using WpfSamples40.Properties;

namespace WpfSamples40.Controls.Common.DataGridCustomization
{
    public class FooterData : INotifyPropertyChanged
    {
        private readonly Dictionary<string, string> _dataDictionary;

        public FooterData()
        {
            _dataDictionary = new Dictionary<string, string>();
        }

        public object this[string columnId]
        {
            get { return GetValueById(columnId); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateFooterValue(string columnId, string value)
        {
            if (_dataDictionary.ContainsKey(columnId))
                _dataDictionary[columnId] = value;
            else
            {
                _dataDictionary.Add(columnId, value);
            }

            OnPropertyChangedForIndexer();
        }

        public object GetValueById(string columnId)
        {
            if (!_dataDictionary.ContainsKey(columnId))
                return null;

            return _dataDictionary[columnId];
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChangedForIndexer()
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(Binding.IndexerName));
        }

        protected void ClearAllValues()
        {
            _dataDictionary.Clear();
            OnPropertyChangedForIndexer();
        }
    }
}