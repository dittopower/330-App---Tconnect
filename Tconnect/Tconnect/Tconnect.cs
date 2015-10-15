using System;

using Xamarin.Forms;

using GalaSoft.MvvmLight.Ioc;
using Tconnect.Data.ViewModel;
using Tconnect.Data;

namespace Tconnect
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			MainPage = new Mainpage();
			//MainPage = GetMainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

		private static ViewModelLocator _locator;
		//private static NavigationService nav;

		public static ViewModelLocator Locator
		{
			get
			{
				return _locator ?? (_locator = new ViewModelLocator());
			}
		}

		/*
		public Page GetMainPage()
		{
			nav = new NavigationService ();
			nav.Configure (ViewModelLocator.EventViewPageKey, typeof(Feed));
			nav.Configure (ViewModelLocator.EventCreatePageKey, typeof(CreateEvent));
			nav.Configure (ViewModelLocator.CalendarPageKey, typeof(Calendar));
			nav.Configure (ViewModelLocator.TempMenuKey, typeof(TempMenu));
			nav.Configure (ViewModelLocator.UserAccountPageKey, typeof(UserAccount));
			nav.Configure (ViewModelLocator.ContactsPageKey, typeof(Contacts));
			nav.Configure (ViewModelLocator.NavPageKey, typeof(Mainpage));
			SimpleIoc.Default.Register<IMyNavigationService> (()=> nav, true);
			var navPage = new NavigationPage (new Mainpage ());
			nav.Initialize (navPage);
			return navPage;
		}*/

		public static NavigationService nav;

		public static Page GetMainPage()
		{
			if (nav == null) {
				nav = new NavigationService ();
				nav.Configure (ViewModelLocator.FeedPageKey, typeof(Feed));
				nav.Configure (ViewModelLocator.EventCreatePageKey, typeof(CreateEvent));
				nav.Configure (ViewModelLocator.CalendarPageKey, typeof(Calendar));
				nav.Configure (ViewModelLocator.TempMenuKey, typeof(TempMenu));
				nav.Configure (ViewModelLocator.UserAccountPageKey, typeof(UserAccount));
				nav.Configure (ViewModelLocator.ContactsPageKey, typeof(Contacts));
				//nav.Configure (ViewModelLocator.NavPageKey, typeof(Mainpage));
				SimpleIoc.Default.Register<IMyNavigationService> (() => nav, true);
			} 
			var navPage = new NavigationPage (new Feed ());//swap to feed when bug is fixed
			nav.Initialize (navPage);
			return navPage;
		}
	}
}

