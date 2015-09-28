/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:NoteTaker1.Data"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Tconnect.Data.ViewModel
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// </summary>
	public class ViewModelLocator
	{
		public const string EventViewPageKey = "EventViewPage";
		public const string EventCreatePageKey = "EventCreatePage";
		public const string TempMenuKey = "TempMenuPage";
		/// <summary>
		/// Initializes a new instance of the ViewModelLocator class.
		/// </summary>
		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			////if (ViewModelBase.IsInDesignModeStatic)
			////{
			////    // Create design time view services and models
			////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
			////}
			////else
			////{
			////    // Create run time view services and models
			////    SimpleIoc.Default.Register<IDataService, DataService>();
			////}

			SimpleIoc.Default.Register<EventViewViewModel>(() => 
				{
					return new EventViewViewModel(
						SimpleIoc.Default.GetInstance<IMyNavigationService>()
					);
				});
			SimpleIoc.Default.Register<EventCreateViewModel>(() => 
				{
					return new EventCreateViewModel(
						SimpleIoc.Default.GetInstance<IMyNavigationService>()
					);
				});
			SimpleIoc.Default.Register<TempMenuViewModel>(() => 
				{
					return new TempMenuViewModel(
						SimpleIoc.Default.GetInstance<IMyNavigationService>()
					);
				});
		}

		public EventViewViewModel NoteList
		{
			get
			{
				return ServiceLocator.Current.GetInstance<EventViewViewModel>();
			}
		}

		public EventCreateViewModel EventCreate
		{
			get
			{
				return ServiceLocator.Current.GetInstance<EventCreateViewModel> ();
			}
		}

		public TempMenuViewModel TempMenu
		{
			get
			{
				return ServiceLocator.Current.GetInstance<TempMenuViewModel>();
			}
		}

		public static void Cleanup()
		{
			// TODO Clear the ViewModels
		}
	}
}