using System;
using System.ComponentModel;
using System.Linq.Expressions;
using WpfSamples40.WpfInfrastructure;

namespace WpfSamples40.ComplexSamples.EventCommands
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            OnPropertyChanged(ExpressionExtensions.GetMemberInfo((Expression)property).Name);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}