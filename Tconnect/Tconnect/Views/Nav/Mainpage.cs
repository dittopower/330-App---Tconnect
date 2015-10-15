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
			var menuPage = new MenuPage ();
			menuPage.OnMenuSelect = (categoryPage) => {
				var navPage = new NavigationPage(categoryPage);

				NavigationPage.SetHasBackButton(navPage,false);
				nav.NavigateToPage(navPage);

				IsPresented = false;
			};

			this.Master = menuPage;
			this.Detail = GetMainPage();
			// Create the detail page using NamedColorPage and wrap it in a



		}

		private static NavigationService nav;

		public Page GetMainPage()
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