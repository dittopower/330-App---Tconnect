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
	public class CalendarList : ICalendarInterface
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

		public List<String[]> getEvents(int CalID)
		{       
			return calendar.getEvents(CalID);
		}


		public List<String[]> getCalendars(){
			return calendar.getCalendars();
		}

		public long addToSystemCal(DateTime dstart, String title, String desc, String loc, int calID){
			return calendar.addToSystemCal(dstart, title, desc, loc, calID);
		}

		public String[] getUserDeets(String token){
			return calendar.getUserDeets (token);
		}

		public List<String[]> contactRequest (String token){
			return calendar.contactRequest (token);
		}

		#endregion

	}
}

