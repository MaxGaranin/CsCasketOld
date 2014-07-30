using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfSamples40.Annotations;
using WpfSamples40.ValidationRules;

namespace WpfSamples40.ComplexSamples.Measures
{
    public partial class MeasureWithControlView : Window, INotifyPropertyChanged
    {
        public MeasureWithControlView()
        {
            InitializeComponent();
            DataContext = this;

            TemperatureMeasureContainer = new MeasureContainer(10.0, Measures.TemperatureC);
            var measuresToValidationRules = new Dictionary<Measure, ValidationRule>
            {
                {Measures.TemperatureC, new DoubleRangeValidationRule(0, 30)},
                {Measures.TemperatureF, new DoubleRangeValidationRule(0, 500)},
                {Measures.TemperatureK, new DoubleRangeValidationRule(-200, 200)},
            };
            TemperatureMeasureContainer.MeasuresToValidationRules = measuresToValidationRules;

            ViscosityMeasureContainer = new MeasureContainer(4.0, Measures.ViscositySp);

            CurrentMeasureContainer = TemperatureMeasureContainer;
        }

        private MeasureContainer _currentMeasureContainer;
        public MeasureContainer CurrentMeasureContainer
        {
            get { return _currentMeasureContainer; }
            set
            {
                if (Equals(value, _currentMeasureContainer)) return;
                _currentMeasureContainer = value;
                RaisePropertyChanged("CurrentMeasureContainer");
            }
        }

        public MeasureContainer TemperatureMeasureContainer { get; set; }
        public MeasureContainer ViscosityMeasureContainer { get; set; }

        #region Controls events

        private void SetViscosityButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentMeasureContainer = ViscosityMeasureContainer;
        }

        private void SetKelvinTemperatureButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentMeasureContainer.PersistMeasure.MeasureGroupCode != MeasureGroupCode.Temperature) return;

            CurrentMeasureContainer.CurrentMeasure = Measures.TemperatureK;
        }

        #endregion

        #region INotifyPropertyChanged members

		public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

	    #endregion
    }
}
