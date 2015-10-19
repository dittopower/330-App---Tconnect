using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Tconnect.Data;
using Tconnect.Data.ViewModel;

namespace Tconnect
{
	public partial class MenuPage : ContentPage
	{
		public MenuPage ()
		{
			InitializeComponent ();

			var mymenu = new List<MyMenuItem> () {
				new MyMenuItem("Home",ViewModelLocator.FeedPageKey),
				new MyMenuItem("Calendar",ViewModelLocator.CalendarPageKey),
				new MyMenuItem("Contacts",ViewModelLocator.ContactsPageKey),
				new MyMenuItem("Profile",ViewModelLocator.UserAccountPageKey)
			};
			menulistView.ItemsSource = mymenu;

			menulistView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				if (OnMenuSelect != null)
				{
					var category = (MyMenuItem) e.SelectedItem;
					var categoryPage = category.Page;
					OnMenuSelect(categoryPage);
				}				
			};
		}

		public Action<string> OnMenuSelect {
			get;
			set;
		}
	}
}

