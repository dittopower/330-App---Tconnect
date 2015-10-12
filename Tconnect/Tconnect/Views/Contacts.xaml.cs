using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Tconnect.Data.ViewModel;
using Tconnect.Data;
using Microsoft.Practices.ServiceLocation;

namespace Tconnect
{
	public partial class Contacts : BaseView
	{
		public Contacts ()
		{
			InitializeComponent ();

			Title = "Contacts";
			base.Init ();
			BindingContext = App.Locator.Contacts;

		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			var vm = ServiceLocator.Current.GetInstance<ContactsViewModel> ();
			vm.OnAppearing ();
		}
	}
}

