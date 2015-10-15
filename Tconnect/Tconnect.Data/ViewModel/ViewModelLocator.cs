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
		public const string FeedPageKey = "FeedPage";
		public const string EventCreatePageKey = "EventCreatePage";
		public const string TempMenuKey = "TempMenuPage";
		public const string CalendarPageKey = "CalendarPage";
		public const string UserAccountPageKey = "UserAccountPage";
		public const string ContactsPageKey = "ContactsPage";
		public const string NavPageKey = "NavPage";
		/// <summary>
		/// Initializes a new instance of the ViewModelLocator class.
		/// </summary>
		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<MainFeedViewModel>(() => 
				{
					return new MainFeedViewModel(
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
			SimpleIoc.Default.Register<CalendarViewModel>(() => 
				{
					return new CalendarViewModel(
						SimpleIoc.Default.GetInstance<IMyNavigationService>()
					);
				});
			SimpleIoc.Default.Register<UserAccountViewModel>(() => 
				{
					return new UserAccountViewModel(
						SimpleIoc.Default.GetInstance<IMyNavigationService>()
					);
				});
			SimpleIoc.Default.Register<ContactsViewModel>(() => 
				{
					return new ContactsViewModel(
						SimpleIoc.Default.GetInstance<IMyNavigationService>()
					);
				});
			/*SimpleIoc.Default.Register<NavPageViewModel>(() => 
				{
					return new NavPageViewModel(
						SimpleIoc.Default.GetInstance<IMyNavigationService>()
					);
				});*/
		}

		public MainFeedViewModel Feed
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MainFeedViewModel>();
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

		public CalendarViewModel Calendar
		{
			get
			{
				return ServiceLocator.Current.GetInstance<CalendarViewModel>();
			}
		}

		public UserAccountViewModel UserAccount
		{
			get
			{
				return ServiceLocator.Current.GetInstance<UserAccountViewModel>();
			}
		}

		public ContactsViewModel Contacts
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ContactsViewModel>();
			}
		}

		/*public NavPageViewModel NavPage
		{
			get
			{
				return ServiceLocator.Current.GetInstance<NavPageViewModel>();
			}
		}*/

		public static void Cleanup()
		{
			// TODO Clear the ViewModels
		}
	}
}