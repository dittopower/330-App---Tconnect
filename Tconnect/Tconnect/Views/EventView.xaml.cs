using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Microsoft.Practices.ServiceLocation;
using Tconnect.Data.ViewModel;

namespace Tconnect
{
	public partial class EventView : BaseView
	{
		public EventView () : this(0){}
		public EventView (int thing)
		{
			InitializeComponent ();
			base.Init ();
			BindingContext = App.Locator.Eventp;
			Title = "Event View";
			((EventViewModel)BindingContext).ID = thing;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			var vm = ServiceLocator.Current.GetInstance<EventViewModel> ();
			vm.OnAppearing ();
		}
	}
}

