using System;
using System.Linq.Expressions;
using GalaSoft.MvvmLight;

namespace WpfSamples40.WpfInfrastructure
{
    public class MyViewModelBase : ViewModelBase
    {
        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            RaisePropertyChanged(ExpressionExtensions.GetMemberInfo((Expression)property).Name);
        }
    }
}