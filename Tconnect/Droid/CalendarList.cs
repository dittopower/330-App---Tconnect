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
	public class CalendarList : ICalendarInterface //IDK WHY LIST ACTIVITY WORKS BUT IT DOES
	{
		public int _calId;
		public List<String[]> calen,events;
		Calender calendar;
		//Activity test = new Activity();
		public CalendarList ()
		{
			calendar = new Calender ();
		}

		#region ICalendarList implementation

		public List<String[]> getEvents()
		{       
			
			return calendar.getEvents();
		}


		public List<String[]> getCalendars(){
			
			return calendar.getCalendars();

			//return new List<String[]> ();

		}


		/*long GetDateTimeMS (int yr, int month, int day, int hr, int min)
		{
			Calendar c = Calendar.GetInstance (Java.Util.TimeZone.Default);

			c.Set (Calendar.DayOfMonth, 15);
			c.Set (Calendar.HourOfDay, hr);
			c.Set (Calendar.Minute, min);
			c.Set (Calendar.Month, Calendar.December);
			c.Set (Calendar.Year, 2011);

			return c.TimeInMillis;
		}*/

		#endregion

	}
}

