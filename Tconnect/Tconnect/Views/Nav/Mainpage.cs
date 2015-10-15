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
				//var navPage = new NavigationPage(categoryPage);

				NavigationPage.SetHasBackButton(categoryPage,false);
				App.nav.NavigateToPage(categoryPage);

				//navPage.
				//Detail.Title = categoryPage.Title;
				//Detail.Navigation = navPage.Navigation;
				IsPresented = false;
			};

			this.Master = menuPage;
			this.Detail = App.GetMainPage();
			// Create the detail page using NamedColorPage and wrap it in a



		}


	}

}