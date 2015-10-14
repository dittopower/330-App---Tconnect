using System;
using Xamarin.Forms;
using Tconnect.Data.ViewModel;
using Tconnect.Data;
using GalaSoft.MvvmLight.Ioc;

namespace Tconnect
{
	class Mainpage :  MasterDetailPage
	{
		public Mainpage()
		{
			//BindingContext = App.Locator.NavPage;
			Label header = new Label
			{
				Text = "Menu",
				Font = Font.SystemFontOfSize(30, FontAttributes.Bold),
				HorizontalOptions = LayoutOptions.Center
			};

			// Assemble an array of NamedColor objects.

			// Create the master page with the ListView.
			var menuPage = new MenuPage ();
			menuPage.OnMenuSelect = (categoryPage) => {
				var navPage = new NavigationPage(categoryPage);//swap to feed when bug is fixed
				nav.Initialize (navPage);
				Detail = navPage;
				IsPresented = false;
			};

			this.Master = menuPage;
			this.Detail = GetMainPage();
			// Create the detail page using NamedColorPage and wrap it in a



		}

		private static NavigationService nav;

		public Page GetMainPage()
		{
			nav = new NavigationService ();
			nav.Configure (ViewModelLocator.EventViewPageKey, typeof(Feed));
			nav.Configure (ViewModelLocator.EventCreatePageKey, typeof(CreateEvent));
			nav.Configure (ViewModelLocator.CalendarPageKey, typeof(Calendar));
			nav.Configure (ViewModelLocator.TempMenuKey, typeof(TempMenu));
			nav.Configure (ViewModelLocator.UserAccountPageKey, typeof(UserAccount));
			nav.Configure (ViewModelLocator.ContactsPageKey, typeof(Contacts));
			//nav.Configure (ViewModelLocator.NavPageKey, typeof(Mainpage));
			SimpleIoc.Default.Register<IMyNavigationService> (()=> nav, true);
			var navPage = new NavigationPage (new Calendar ());//swap to feed when bug is fixed
			nav.Initialize (navPage);
			return navPage;
		}
	}

}