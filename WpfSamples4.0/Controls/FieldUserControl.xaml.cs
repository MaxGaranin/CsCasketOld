using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfSamples40.Controls
{
    /// <summary>
    /// Interaction logic for FieldUserControl.xaml
    /// </summary>
    public partial class FieldUserControl : UserControl
    {
        public FieldUserControl()
        {
            InitializeComponent();

//            LayoutRoot.DataContext = this;
            DataContext = this;
        }

        public const string TEST = "Test";

        #region ParentDataContext DP

        public object ParentDataContext
        {
            get { return (object)GetValue(ParentDataContextProperty); }
            set { SetValue(ParentDataContextProperty, value); }
        }

        public static readonly DependencyProperty ParentDataContextProperty =
            DependencyProperty.Register("ParentDataContext", typeof(object), typeof(FieldUserControl), new PropertyMetadata(null));

        #endregion

        #region Label DP

        /// <summary>
        /// Gets or sets the Label which is displayed next to the field
        /// </summary>
        public String Label
        {
            get { return (String) GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string),
              typeof(FieldUserControl), new PropertyMetadata(""));

        #endregion

        #region Value DP

        /// <summary>
        /// Gets or sets the Value which is being displayed
        /// </summary>
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object),
              typeof(FieldUserControl), new PropertyMetadata(null));

        #endregion
    }

}
