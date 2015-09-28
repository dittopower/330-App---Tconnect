using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Tconnect.Data.ViewModel;
using Tconnect.Data;

namespace Tconnect
{
	public partial class TempMenu : BaseView
	{
		public TempMenu ()
		{
			InitializeComponent ();
			base.Init ();
			BindingContext = App.Locator.TempMenu;
			Title = "Temporary Menu";
		}

		protected void ButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync (new CreateEvent ());
		}
	}
}

