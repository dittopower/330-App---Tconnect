using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Tconnect.Data.ViewModel;
using Tconnect.Data;
using Microsoft.Practices.ServiceLocation;

namespace Tconnect
{
	public partial class CreateEvent : BaseView
	{
		public CreateEvent () : this(0){}
		public CreateEvent (int thing)
		{
			BindingContext = App.Locator.EventCreate;
			InitializeComponent ();
			base.Init ();
			Title = "Edit Event";
			((EventCreateViewModel)BindingContext).ID = thing;
		}

	}
}