using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfSamples.View.Templates
{
    public class MyTemplateSelector : DataTemplateSelector
    {
        public TypeDataTemplateDictionary Templates { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) return null;

            return GetTemplate(item.GetType());
        }

        private DataTemplate GetTemplate(Type valueType)
        {
            if (Templates.TryGetValue(valueType, out var tmpl)) return tmpl;

            throw new KeyNotFoundException(string.Format("DataTemplate for {0} not found", valueType));
        }
    }

    public class TypeDataTemplateDictionary : Dictionary<Type, DataTemplate>
    {
    }
}