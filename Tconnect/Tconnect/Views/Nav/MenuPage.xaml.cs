using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tconnect
{
	public partial class MenuPage : ContentPage
	{
		public MenuPage ()
		{
			InitializeComponent ();

			var mymenu = new List<MyMenuItem> () {
				new MyMenuItem("Home",() => new Feed()),
				new MyMenuItem("Calendar",() =>  new Calendar()),
				new MyMenuItem("Contacts",() =>  new Contacts()),
				new MyMenuItem("Profile",() =>  new UserAccount())
			};
			menulistView.ItemsSource = mymenu;

			menulistView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				if (OnMenuSelect != null)
				{
					var category = (MyMenuItem) e.SelectedItem;
					var categoryPage = category.Page();
					OnMenuSelect(categoryPage);
				}				
			};
		}

		public Action<ContentPage> OnMenuSelect {
			get;
			set;
		}
	}
}

