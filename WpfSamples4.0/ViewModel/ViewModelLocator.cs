/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:TextBoxWithValidation.WPF.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace WpfSamples40.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<SimpleControlTestViewModel>();
            SimpleIoc.Default.Register<TriggersTestsViewModel>();
            SimpleIoc.Default.Register<MultiTriggerViewModel>();
            SimpleIoc.Default.Register<ValidationTestModel>();
            SimpleIoc.Default.Register<TestViewModel>();
        }

        public SimpleControlTestViewModel SimpleControlTest
        {
            get { return ServiceLocator.Current.GetInstance<SimpleControlTestViewModel>(); }
        }

        public TriggersTestsViewModel TriggersTests
        {
            get { return ServiceLocator.Current.GetInstance<TriggersTestsViewModel>(); }
        }

        public MultiTriggerViewModel MultiTrigger
        {
            get { return ServiceLocator.Current.GetInstance<MultiTriggerViewModel>(); }
        }

        public ValidationTestModel ValidationTest
        {
            get { return ServiceLocator.Current.GetInstance<ValidationTestModel>(); }
        }

        public TestViewModel Test
        {
            get { return ServiceLocator.Current.GetInstance<TestViewModel>(); }
        }

        public static void Cleanup()
        {
        }
    }
}