using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using WpfSamples40.Annotations;

namespace WpfSamples40.ComplexSamples.Measures
{
    public class MeasureContainer : INotifyPropertyChanged
    {
        public event EventHandler ValueValidationRuleChanged;

        private IList<Measure> _measures;
        private double _persistValue;
        private Measure _persistMeasure;
        private double _currentValue;
        private Measure _currentMeasure;
        private ValidationRule _valueValidationRule;
        private IDictionary<Measure, ValidationRule> _measuresToValidationRules;

        public MeasureContainer(double value, Measure measure)
        {
            if (measure == null)
                throw new ArgumentNullException("measure", "Размерность должна быть задана.");

            PersistValue = value;
            PersistMeasure = measure;
            Measures = measure.MeasureGroupCode.GetMeasures().ToList();
        }

        public IList<Measure> Measures
        {
            get { return _measures; }
            set
            {
                if (Equals(value, _measures)) return;
                _measures = value;
                RaisePropertyChanged("Measures");
            }
        }

        public double PersistValue
        {
            get { return _persistValue; }
            set
            {
                if (value.Equals(_persistValue)) return;
                _persistValue = value;
                RaisePropertyChanged("PersistValue");

                _currentValue = value;
                RaisePropertyChanged("CurrentValue");
            }
        }

        public Measure PersistMeasure
        {
            get { return _persistMeasure; }
            set
            {
                if (Equals(value, _persistMeasure)) return;
                _persistMeasure = value;
                RaisePropertyChanged("PersistMeasure");

                _currentMeasure = value;
                RaisePropertyChanged("CurrentMeasure");
            }
        }

        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (value.Equals(_currentValue)) return;
                _currentValue = value;
                RaisePropertyChanged("CurrentValue");

                if ((CurrentMeasure != null) && (PersistMeasure != null))
                {
                    _persistValue = CurrentMeasure.Equals(PersistMeasure)
                        ? _currentValue
                        : MeasureManager.ConvertMeasure(CurrentMeasure, PersistMeasure, value);
                    RaisePropertyChanged("PersistValue");
                }
            }
        }

        public Measure CurrentMeasure
        {
            get { return _currentMeasure; }
            set
            {
                if (Equals(value, _currentMeasure)) return;
                _currentMeasure = value;
                RaisePropertyChanged("CurrentMeasure");

                if ((value != null) && (PersistMeasure != null))
                {
                    _currentValue = MeasureManager.ConvertMeasure(PersistMeasure, value, PersistValue);
                    RaisePropertyChanged("CurrentValue");

                    SetValueValidationRule();
                }
            }
        }

        /// <summary>
        /// Текущее правило валидации
        /// </summary>
        public ValidationRule ValueValidationRule
        {
            get { return _valueValidationRule; }
            set
            {
                if (Equals(value, _valueValidationRule)) return;
                _valueValidationRule = value;
                RaisePropertyChanged("ValueValidationRule");
                RaiseValueValidationRuleChanged();
            }
        }

        /// <summary>
        /// Правила валидации в зависимости от размерности.
        /// </summary>
        public IDictionary<Measure, ValidationRule> MeasuresToValidationRules
        {
            get { return _measuresToValidationRules; }
            set
            {
                if (Equals(value, _measuresToValidationRules)) return;
                _measuresToValidationRules = value;
                RaisePropertyChanged("MeasuresToValidationRules");

                SetValueValidationRule();
            }
        }

        private void SetValueValidationRule()
        {
            if ((MeasuresToValidationRules == null) || (CurrentMeasure == null)) return;

            ValidationRule rule;
            var result = MeasuresToValidationRules.TryGetValue(CurrentMeasure, out rule);
            if (result)
            {
                ValueValidationRule = rule;
            }
        }

        private void RaiseValueValidationRuleChanged()
        {
            var handler = ValueValidationRuleChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

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