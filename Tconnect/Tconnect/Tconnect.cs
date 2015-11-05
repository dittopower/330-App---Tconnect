using System;

using Xamarin.Forms;

using GalaSoft.MvvmLight.Ioc;
using Tconnect.Data.ViewModel;
using Tconnect.Data;
using System.Diagnostics;

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
			if (!IsLoggedIn) {
				var database = new NoteDatabase ();
				if (database.HasToken ("Yammer")) {
					var t = database.GetToken ("Yammer");
					if (t != null) {
						_Token = t.Value;
					}
					Debug.WriteLine (_Token);
				}
			}
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
			if (!IsLoggedIn) {
				var database = new NoteDatabase ();
				if (database.HasToken ("Yammer")) {
					var t = database.GetToken ("Yammer");
					if (t != null) {
						_Token = t.Value;
					}
				}
			}
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
				nav.Configure (ViewModelLocator.EventPageKey, typeof(EventView));
				//nav.Configure (ViewModelLocator.NavPageKey, typeof(Mainpage));
				SimpleIoc.Default.Register<IMyNavigationService> (() => nav, true);
			} 
			var navPage = new NavigationPage (new Feed ());//swap to feed when bug is fixed
			nav.Initialize (navPage);
			return navPage;
		}


		//Yammer auth stuff
		public static bool IsLoggedIn {
			get { 
				var database = new NoteDatabase ();
				if (database.HasToken ("Yammer")) {
					return !string.IsNullOrWhiteSpace (_Token);
				} else {
					_Token = null;
					return false;
				}
			}
		}

		static string _Token;
		public static string Token {
			get { return _Token; }
		}

		public static void SaveToken(string token)
		{
			_Token = token;
			var database = new NoteDatabase ();
			database.InsertOrUpdateToken(new Tconnect.Data.Token("Yammer",token));
			database.InsertOrUpdateToken(new Tconnect.Data.Token("Calendar","1"));
		}

		public static Action SuccessfulLoginAction
		{
			get {
				return new Action (() => {
					nav.GoBack();
				});
			}
		}
	}
}

