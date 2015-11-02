using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tconnect.Data;
using SQLite.Net;
using System.IO;
using Xamarin.Forms;
using Tconnect.Droid;

using Android.App;
using Android.Database;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Java.Util;

[assembly: Dependency (typeof (CalendarList))]

namespace Tconnect.Droid
{
	public class CalendarList : ListActivity //IDK WHY LIST ACTIVITY WORKS BUT IT DOES
	{
		//public int _calId;
		//public List<String[]> calen,events;

		public CalendarList ()
		{
		}

		#region CalendarList implementation
			


		#endregion

	}
}

