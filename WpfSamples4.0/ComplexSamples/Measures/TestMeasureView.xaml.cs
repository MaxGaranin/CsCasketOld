using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfSamples40.Annotations;

namespace WpfSamples40.ComplexSamples.Measures
{
    public partial class TestMeasureView : Window, INotifyPropertyChanged
    {
        private MeasureContainer _tempMeasureContainer;
        private MeasureContainer _viscMeasureContainer;
        private Measure _currentTempMeasure;
        private Measure _currentViscMeasure;

        public TestMeasureView()
        {
            InitializeComponent();

            DataContext = this;

            TempMeasureContainer = new MeasureContainer(10.0, Measures.TemperatureC);
            ViscMeasureContainer = new MeasureContainer(4.0, Measures.ViscositySp);

            TempMeasures = MeasureGroups.TemperatureMeasures.ToList();
            CurrentTempMeasure = TempMeasures[0];

            ViscMeasures = MeasureGroups.ViscosityMeasures.ToList();
            CurrentViscMeasure = ViscMeasures[0];
        }

        public MeasureContainer TempMeasureContainer
        {
            get { return _tempMeasureContainer; }
            set
            {
                if (Equals(value, _tempMeasureContainer)) return;
                _tempMeasureContainer = value;
                RaisePropertyChanged("TempMeasureContainer");
            }
        }

        public MeasureContainer ViscMeasureContainer
        {
            get { return _viscMeasureContainer; }
            set
            {
                if (Equals(value, _viscMeasureContainer)) return;
                _viscMeasureContainer = value;
                RaisePropertyChanged("ViscMeasureContainer");
            }
        }

        public Measure CurrentTempMeasure
        {
            get { return _currentTempMeasure; }
            set
            {
                if (Equals(value, _currentTempMeasure)) return;
                _currentTempMeasure = value;
                RaisePropertyChanged("CurrentTempMeasure");

                TempMeasureContainer.CurrentMeasure = value;
            }
        }

        public Measure CurrentViscMeasure
        {
            get { return _currentViscMeasure; }
            set
            {
                if (Equals(value, _currentViscMeasure)) return;
                _currentViscMeasure = value;
                RaisePropertyChanged("CurrentViscMeasure");

                ViscMeasureContainer.CurrentMeasure = value;
            }
        }

        public IList<Measure> TempMeasures { get; set; }

        public IList<Measure> ViscMeasures { get; set; }

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
