using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Tconnect.Data.ViewModel;
using Tconnect.Data;
using Microsoft.Practices.ServiceLocation;

namespace Tconnect
{
	public partial class Feed : BaseView
	{
		public Feed ()
		{
			InitializeComponent ();
			base.Init ();
			BindingContext = App.Locator.NoteList;
		}

		protected void ButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync (new CreateEvent ());
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			var vm = ServiceLocator.Current.GetInstance<EventViewViewModel> ();
			vm.OnAppearing ();
		}

	}
}

