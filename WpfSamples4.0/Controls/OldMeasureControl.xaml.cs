using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Omu.ValueInjecter;
using WpfSamples40.Annotations;
using WpfSamples40.ComplexSamples.Measures;
using WpfSamples40.Model;
using WpfSamples40.Properties;

namespace WpfSamples40.Controls
{
    public partial class OldMeasureControl : UserControl, INotifyPropertyChanged
    {
        private bool _isLoaded;
        private bool _isCurrentMeasureInit;
        private bool _isCurrentValueInit;

        private bool _isSetFromPersistValue;
        private bool _isSetFromPersistMeasure;
        private bool _isSetFromCurrentValue;
        private bool _isSetFromCurrentMeasure;

        public OldMeasureControl()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
            IsShowCombo = true;

            Loaded += (sender, args) =>
            {
                _isLoaded = true;

                // Устанавливаем свойства в определенном порядке, 
                // это важно, так как они перезаписывают друг друга

                // 0
                // Делаем легкие копии текущих свойств (Current), 
                // чтобы они не затерлись при установке сохраняемых свойств (Persist)
                var currentValueCopy = CurrentValue;
                Measure currentMeasureCopy = null;
                if (CurrentMeasure != null)
                {
                    currentMeasureCopy = (Measure) CurrentMeasure.InjectFrom(CurrentMeasure);
                }

                // 1
                OnPersistValueChanged(PersistValue);

                // 2
                OnPersistMeasureChanged(PersistMeasure);

                // 3
                if (_isCurrentMeasureInit)
                {
                    CurrentMeasure = currentMeasureCopy;
                    OnCurrentMeasureChanged(CurrentMeasure);
                }

                // 4
                if (_isCurrentValueInit)
                {
                    CurrentValue = currentValueCopy;
                    OnCurrentValueChanged(CurrentValue);
                }
            };
        }

        #region CurrentValue

        public double CurrentValue
        {
            get { return Convert.ToDouble(GetValue(CurrentValueProperty)); }
            set { SetValue(CurrentValueProperty, value); }
        }

        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(double), typeof(OldMeasureControl),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) =>
            {
                var c = s as OldMeasureControl;
                if (!c._isLoaded)
                {
                    c._isCurrentValueInit = true;
                    return;
                }

                c.OnCurrentValueChanged(Convert.ToDouble(e.NewValue));

            }));

        private void OnCurrentValueChanged(double currentValue)
        {
            if (_isSetFromPersistValue) return;
            if (_isSetFromCurrentMeasure) return;

            if ((CurrentMeasure != null) && (PersistMeasure != null))
            {
                _isSetFromCurrentValue = true;
                PersistValue = CurrentMeasure.Equals(PersistMeasure)
                    ? currentValue
                    : MeasureManager.ConvertMeasure(CurrentMeasure, PersistMeasure, currentValue);
                _isSetFromCurrentValue = false;
            }
        }

        #endregion

        #region CurrentMeasure

        public Measure CurrentMeasure
        {
            get { return (Measure)GetValue(CurrentMeasureProperty); }
            set { SetValue(CurrentMeasureProperty, value); }
        }

        public static readonly DependencyProperty CurrentMeasureProperty =
            DependencyProperty.Register("CurrentMeasure", typeof(Measure), typeof(OldMeasureControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) =>
            {
                var c = (s as OldMeasureControl);
                if (!c._isLoaded)
                {
                    c._isCurrentMeasureInit = true;
                    return;
                }

                c.OnCurrentMeasureChanged((Measure)e.NewValue);
            }));

        private void OnCurrentMeasureChanged(Measure currentMeasure)
        {
            if (_isSetFromPersistMeasure) return;

            if ((currentMeasure != null) && (PersistMeasure != null))
            {
                _isSetFromCurrentMeasure = true;
                CurrentValue = MeasureManager.ConvertMeasure(PersistMeasure, currentMeasure, PersistValue);
                _isSetFromCurrentMeasure = false;
            }

            if ((currentMeasure != null) && (MeasuresToValidationRules != null))
            {
                var rule = MeasuresToValidationRules[currentMeasure];
                SetValidationRule(ValueTextBox, rule);
            }
        }

        #endregion

        #region PersistValue

        public double PersistValue
        {
            get { return Convert.ToDouble(GetValue(PersistValueProperty)); }
            set { SetValue(PersistValueProperty, value); }
        }

        public static readonly DependencyProperty PersistValueProperty =
            DependencyProperty.Register("PersistValue", typeof(double), typeof(OldMeasureControl),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) =>
            {
                var c = s as OldMeasureControl;
                if (!c._isLoaded) return;

                c.OnPersistValueChanged(Convert.ToDouble(e.NewValue));
            }));

        private void OnPersistValueChanged(double persistValue)
        {
            if (_isSetFromCurrentValue) return;

            _isSetFromPersistValue = true;
            CurrentValue = persistValue;
            _isSetFromPersistValue = false;
        }

        #endregion

        #region PersistsMeasure

        public Measure PersistMeasure
        {
            get { return (Measure)GetValue(PersistMeasureProperty); }
            set { SetValue(PersistMeasureProperty, value); }
        }

        public static readonly DependencyProperty PersistMeasureProperty =
            DependencyProperty.Register("PersistMeasure", typeof(Measure), typeof(OldMeasureControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) =>
            {
                var c = s as OldMeasureControl;
                if (!c._isLoaded) return;

                c.OnPersistMeasureChanged((Measure)e.NewValue);
            }));

        private void OnPersistMeasureChanged(Measure persistMeasure)
        {
            _isSetFromPersistMeasure = true;
            CurrentMeasure = persistMeasure;
            _isSetFromPersistMeasure = false;
        }

        #endregion

        #region PercentMeasures

        public List<Measure> Measures
        {
            get { return (List<Measure>)GetValue(MeasuresProperty); }
            set { SetValue(MeasuresProperty, value); }
        }

        public static readonly DependencyProperty MeasuresProperty =
            DependencyProperty.Register("Measures", typeof(List<Measure>), typeof(OldMeasureControl),
            new PropertyMetadata(null));

        #endregion

        #region ValidationRule

        public ValidationRule ValueValidationRule
        {
            get { return (ValidationRule)GetValue(ValueValidationRuleProperty); }
            set { SetValue(ValueValidationRuleProperty, value); }
        }

        public static readonly DependencyProperty ValueValidationRuleProperty =
            DependencyProperty.Register("ValueValidationRule", typeof(ValidationRule), typeof(OldMeasureControl),
            new PropertyMetadata((s, e) =>
            {
                var control = (s as OldMeasureControl);

                var binding = BindingOperations.GetBinding(control.ValueTextBox, TextBox.TextProperty);
                if (binding == null) return;
                binding.ValidationRules.Clear();
                binding.ValidationRules.Add((ValidationRule)e.NewValue);
            }));

        #endregion

        #region MeasuresToValidationRules

        public Dictionary<Measure, ValidationRule> MeasuresToValidationRules
        {
            get { return (Dictionary<Measure, ValidationRule>)GetValue(MeasuresToValidationRulesProperty); }
            set { SetValue(MeasuresToValidationRulesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MeasuresToValidationRules.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MeasuresToValidationRulesProperty =
            DependencyProperty.Register("MeasuresToValidationRules", typeof(Dictionary<Measure, ValidationRule>), typeof(OldMeasureControl),
            new PropertyMetadata((s, e) =>
            {
                var control = (s as OldMeasureControl);

                if (control.CurrentMeasure == null) return;
                var rule = ((Dictionary<Measure, ValidationRule>)e.NewValue)[control.CurrentMeasure];
                SetValidationRule(control.ValueTextBox, rule);
            }));
        #endregion

        #region IsShowCombo

        public bool IsShowCombo
        {
            get { return (bool)GetValue(IsShowComboProperty); }
            set { SetValue(IsShowComboProperty, value); }
        }

        public static readonly DependencyProperty IsShowComboProperty =
            DependencyProperty.Register("IsShowCombo", typeof(bool), typeof(OldMeasureControl),
            new PropertyMetadata(false, (s, e) =>
            {
                var isShow = (bool)e.NewValue;

                var control = s as OldMeasureControl;
                control.ComboColumn.Width = isShow
                    ? new GridLength(1, GridUnitType.Star)
                    : new GridLength(1, GridUnitType.Auto);
            }));

        #endregion

        #region Private methods

        private static void SetValidationRule(TextBox valueTextBox, ValidationRule rule)
        {
            var binding = BindingOperations.GetBinding(valueTextBox, TextBox.TextProperty);
            if (binding == null) return;
            binding.ValidationRules.Clear();
            binding.ValidationRules.Add(rule);
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
