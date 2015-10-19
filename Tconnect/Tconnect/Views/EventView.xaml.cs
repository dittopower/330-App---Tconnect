using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Microsoft.Practices.ServiceLocation;

namespace Tconnect
{
	public partial class EventView : BaseView
	{
		public EventView ()
		{
			InitializeComponent ();
			base.Init ();
			BindingContext = App.Locator.Eventp;
			Title = "Event View";
		}
	}
}

