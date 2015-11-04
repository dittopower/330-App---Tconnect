﻿using System;
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

		public List<String[]> getEvents()
		{       
			return calendar.getEvents();
		}


		public List<String[]> getCalendars(){
			return calendar.getCalendars();
		}

		public void addToSystemCal(DateTime dstart, String title, String desc, String loc, int calID){
			calendar.addToSystemCal(dstart, title, desc, loc, calID);
		}

		public List<String[]> contactRequest (String token){
			return calendar.contactRequest (token);
		}

		#endregion

	}
}

