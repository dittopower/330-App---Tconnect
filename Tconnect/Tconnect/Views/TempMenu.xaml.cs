using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tconnect
{
	public partial class TempMenu : ContentPage
	{
		public TempMenu ()
		{
			InitializeComponent ();
			Title = "Temporary Menu";
		}

		protected void ButtonCreateEvent(object sender, EventArgs e)
		{
			Navigation.PushAsync (new CreateEvent ());				
		}

		protected void ButtonUserAccount(object sender, EventArgs e)
		{
			Navigation.PushAsync (new UserAccount ());				
		}

		protected void ButtonCalendar(object sender, EventArgs e)
		{
			Navigation.PushAsync (new Calendar ());				
		}

		protected void ButtonFeed(object sender, EventArgs e)
		{
			Navigation.PushAsync (new Feed ());			
		}

		protected void ButtonEventView(object sender, EventArgs e)
		{
			Navigation.PushAsync (new EventView ());			
		}

		protected void ButtonContacts(object sender, EventArgs e)
		{
			Navigation.PushAsync (new Contacts ());			
		}
	}
}

