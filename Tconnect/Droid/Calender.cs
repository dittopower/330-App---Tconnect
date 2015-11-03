using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Database;
using Android.Provider;
using Java.Util;

namespace Tconnect.Droid
{
	[Activity (Label = "Calender")]			
	public class Calender : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here
		}

		public List<String[]> getEvents(int calendarid = 1)
		{       
			var eventsUri = CalendarContract.Events.ContentUri;
			Console.WriteLine ("whatever");
			string[] eventsProjection = { 
				CalendarContract.Events.InterfaceConsts.Id,
				CalendarContract.Events.InterfaceConsts.Title,
				CalendarContract.Events.InterfaceConsts.Dtstart,
				CalendarContract.Events.InterfaceConsts.Dtend,
				CalendarContract.Events.InterfaceConsts.Description,
				CalendarContract.Events.InterfaceConsts.EventLocation
			};
			//Date d = new Date ();
			Java.Util.Calendar c = Java.Util.Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("AEST"));

			c.Add(CalendarField.Month, -1);

			Console.WriteLine ("Date: " + c.Get(CalendarField.DayOfMonth) + "/" + c.Get(CalendarField.Month) + "/" + c.Get(CalendarField.Year)+" ::: "+ c.TimeInMillis);

			String ww = "calendar_id=" + calendarid + " AND dtstart > " + c.TimeInMillis;

			ICursor cursor = Forms.Context.ApplicationContext.ContentResolver.Query (eventsUri, eventsProjection, ww, null, "dtstart ASC");
			//do date not datetime, write to system calendar

			List<String[]> things = new List<String[]> ();

			if (cursor.MoveToFirst()){
				do{

					String calid = cursor.GetString(cursor.GetColumnIndex( CalendarContract.Events.InterfaceConsts.Id));
					String title = cursor.GetString(cursor.GetColumnIndex( CalendarContract.Events.InterfaceConsts.Title));
					String Dstart = cursor.GetString(cursor.GetColumnIndex( CalendarContract.Events.InterfaceConsts.Dtstart));
					String Dend = cursor.GetString(cursor.GetColumnIndex( CalendarContract.Events.InterfaceConsts.Dtend));
					String desc = cursor.GetString(cursor.GetColumnIndex( CalendarContract.Events.InterfaceConsts.Description));
					String loc = cursor.GetString(cursor.GetColumnIndex( CalendarContract.Events.InterfaceConsts.EventLocation));

					things.Add(new String[] {calid,title,Dstart,Dend,desc,loc});

					//Console.WriteLine("ID: " + calid);

				}while(cursor.MoveToNext());
			}

			cursor.Close();

			return things;
		}


		public List<String[]> getCalendars(){
			// List Calendars
			var calendarsUri = CalendarContract.Calendars.ContentUri;

			string[] calendarsProjection = {
				CalendarContract.Calendars.InterfaceConsts.Id,
				CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
				CalendarContract.Calendars.InterfaceConsts.AccountName
			};

			ICursor cursor = Forms.Context.ApplicationContext.ContentResolver.Query (calendarsUri, calendarsProjection, null, null, null);

			List<String[]> calendars = new List<String[]> ();      

			if (cursor.MoveToFirst()){
				do{

					String calid = cursor.GetString(cursor.GetColumnIndex("_id"));
					String calname = cursor.GetString(cursor.GetColumnIndex("calendar_displayName"));
					String accname = cursor.GetString(cursor.GetColumnIndex("account_name"));

					calendars.Add(new String[] {calid,calname,accname});

				}while(cursor.MoveToNext());
			}

			cursor.Close();

			return calendars;

		}

	}
}

