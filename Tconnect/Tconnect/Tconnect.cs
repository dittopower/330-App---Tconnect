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
			dochecks();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
			dochecks();
		}

		private void dochecks(){
			//Login
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
			if (IsLoggedIn) {
				MyCalendar.ImportCalendar ();
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
			MyCalendar.ImportCalendar ();
			MyCalendar.ImportContacts ();

			MyCalendar cal = new MyCalendar ();
			Debug.WriteLine ("ruight herjelkfjsd tohooooooooooooo");
			String[] s = cal.getUserDeets (token);


			database.InsertOrUpdateToken(new Tconnect.Data.Token("fname",s[0]));
			database.InsertOrUpdateToken(new Tconnect.Data.Token("lname",s[1]));
			database.InsertOrUpdateToken(new Tconnect.Data.Token("email",s[2]));
			database.InsertOrUpdateToken(new Tconnect.Data.Token("phone",s[3]));
			database.InsertOrUpdateToken(new Tconnect.Data.Token("org",s[4]));
			database.InsertOrUpdateToken(new Tconnect.Data.Token("uid",s[5]));
			database.InsertOrUpdateToken(new Tconnect.Data.Token("profilepicture",s[6]));

			// { fname, lname, email, phone, org, uid, profilepicture };
			Debug.WriteLine ("oi we got here m8");
			//Person p = new Person (s [0], s [1], s [2], s [3], s [4], s [5], s [6]);
			//p.NoteId = 0;
			//database.InsertOrUpdatePerson (p);
			//Debug.WriteLine ("afftaahh:: " + database.GetPerson(0).Name);
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

