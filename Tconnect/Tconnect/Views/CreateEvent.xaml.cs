using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Tconnect.Data.ViewModel;
using Tconnect.Data;

namespace Tconnect
{
	public partial class CreateEvent : BaseView
	{
		public CreateEvent ()
		{
			BindingContext = App.Locator.EventCreate;
			InitializeComponent ();
			base.Init ();
			Title = "Events";

		}

	}
}