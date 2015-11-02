using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Microsoft.Practices.ServiceLocation;
using Tconnect.Data.ViewModel;

namespace Tconnect
{
	public partial class UserAccount : BaseView
	{
		public UserAccount () : this(0){}
		public UserAccount (int thing)
		{
			InitializeComponent ();
			base.Init ();
			BindingContext = App.Locator.UserAccount;
			Title = "User Details";
			((UserAccountViewModel)BindingContext).ID = thing;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			var vm = ServiceLocator.Current.GetInstance<EventViewModel> ();
			vm.OnAppearing ();
		}
	}
}

