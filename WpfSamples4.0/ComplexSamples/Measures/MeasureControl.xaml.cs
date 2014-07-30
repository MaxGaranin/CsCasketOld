using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfSamples40.Annotations;

namespace WpfSamples40.ComplexSamples.Measures
{
    public partial class MeasureControl : UserControl, INotifyPropertyChanged
    {
        public MeasureControl()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
            IsShowCombo = true;
        }

        #region MeasureContainer

        public MeasureContainer MeasureContainer
        {
            get { return (MeasureContainer)GetValue(MeasureContainerProperty); }
            set { SetValue(MeasureContainerProperty, value); }
        }

        public static readonly DependencyProperty MeasureContainerProperty =
            DependencyProperty.Register("MeasureContainer", typeof(MeasureContainer), typeof(MeasureControl), 
            new PropertyMetadata(null, (s, e) =>
            {
                var c = s as MeasureControl;
                var measureContainer = (MeasureContainer) e.NewValue;

                measureContainer.ValueValidationRuleChanged += c.OnValueValidationRuleChanged;
                c.OnValueValidationRuleChanged(c, EventArgs.Empty);
            }));

        private void OnValueValidationRuleChanged(object sender, EventArgs e)
        {
            if (MeasureContainer.ValueValidationRule == null) return;

            var binding = BindingOperations.GetBinding(ValueTextBox, TextBox.TextProperty);
            if (binding == null) return;
            binding.ValidationRules.Clear();
            binding.ValidationRules.Add(MeasureContainer.ValueValidationRule);   
         
            UpdateValueTextBox();
        }
        
        #endregion
        
        #region IsShowCombo

        public bool IsShowCombo
        {
            get { return (bool)GetValue(IsShowComboProperty); }
            set { SetValue(IsShowComboProperty, value); }
        }

        public static readonly DependencyProperty IsShowComboProperty =
            DependencyProperty.Register("IsShowCombo", typeof(bool), typeof(MeasureControl),
            new PropertyMetadata(false, (s, e) =>
            {
                var isShow = (bool)e.NewValue;

                var control = s as MeasureControl;
                control.ComboColumn.Width = isShow
                    ? new GridLength(1, GridUnitType.Star)
                    : new GridLength(1, GridUnitType.Auto);
            }));

        #endregion

        #region UpdateValueTextBox

        private void MeasureCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateValueTextBox();
        }

        private void UpdateValueTextBox()
        {
            var bindingExpression = ValueTextBox.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression != null)
                bindingExpression.UpdateSource();
        } 
        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
