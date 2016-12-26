using System;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight;

namespace WpfSamples40.View.Charts
{
    public partial class RadCartesianChartTestView : Window
    {
        public RadCartesianChartTestView()
        {
            InitializeComponent();

            DataContext = this;
            Init();
        }

        private void Init()
        {
            var now = DateTime.Now.Date;
            var i = 1;

            OilProductionLevelItems = new ObservableCollection<OilProductionLevelItem>
            {
                new OilProductionLevelItem
                {
                    LeftBorder = now.AddDays(i++), 
                    RightBorder = now.AddDays(i++),
                    LiquidQ = 500,
                    OilQ = 200,
                    GasQ = 1000
                },
                new OilProductionLevelItem
                {
                    LeftBorder = now.AddDays(i++), 
                    RightBorder = now.AddDays(i++),
                    LiquidQ = 700,
                    OilQ = 300,
                    GasQ = 2000
                },
            };
        }

        public ObservableCollection<OilProductionLevelItem> OilProductionLevelItems { get; set; }
    }

    public class OilProductionLevelItem : ObservableObject
    {
        private DateTime _leftBorder;
        private DateTime _rightBorder;
        private double _liquidQ;
        private double _oilQ;
        private double _gasQ;

        public int Id { get; set; }

        public int LevelId { get; set; }

        public DateTime LeftBorder
        {
            get { return _leftBorder; }
            set { Set("LeftBorder", ref _leftBorder, value); }
        }

        public DateTime RightBorder
        {
            get { return _rightBorder; }
            set { Set("RightBorder", ref _rightBorder, value); }
        }

        public double LiquidQ
        {
            get { return _liquidQ; }
            set
            {
                Set("LiquidQ", ref _liquidQ, value);
                RaisePropertyChanged("WaterQ");
            }
        }

        public double OilQ
        {
            get { return _oilQ; }
            set
            {
                Set("OilQ", ref _oilQ, value);
                RaisePropertyChanged("WaterQ");
            }
        }

        public double GasQ
        {
            get { return _gasQ; }
            set
            {
                Set("GasQ", ref _gasQ, value);
            }
        }

        public double WaterQ
        {
            get { return Math.Round(LiquidQ - OilQ, 3); }
        }
    }

}