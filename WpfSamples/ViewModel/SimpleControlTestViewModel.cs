using GalaSoft.MvvmLight;
using WpfSamples.ViewModel.HelperClasses;

namespace WpfSamples.ViewModel
{

    public class SimpleControlTestViewModel : ViewModelBase
    {
        public SimpleControlTestViewModel()
        {
            ZipCode = "ZipCode";
            Test = 123;

            ModelObject = new ModelObject()
                {
                    Height = 30,
                    ShoeSize = 28
                };
        }
        
        #region ZipCode
        public const string ZipCodePropertyName = "ZipCode";

        private string _zipCode = string.Empty;

        public string ZipCode
        {
            get
            {
                return _zipCode;
            }
            set
            {
                if (Set(ZipCodePropertyName, ref _zipCode, value))
                {
                    RaisePropertyChanged(ZipCodePropertyName);
                }
            }
        }
        #endregion

        #region Test
        public const string TestPropertyName = "Test";

        private int _test;

        public int Test
        {
            get
            {
                return _test;
            }
            set
            {
                if (Set(TestPropertyName, ref _test, value))
                {
                    RaisePropertyChanged(TestPropertyName);
                }
            }
        }
        #endregion

        #region ModelObject
		
        public const string ModelObjectPropertyName = "ModelObject";

        private ModelObject _modelObject;

        public ModelObject ModelObject
        {
            get
            {
                return _modelObject;
            }
            set
            {
                if (Set(ModelObjectPropertyName, ref _modelObject, value))
                {
                    RaisePropertyChanged(ModelObjectPropertyName);
                }
            }
        }

	    #endregion  
    }
}