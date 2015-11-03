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
			
		public void addToSystemCal(DateTime dstart, String title, String desc, String loc, int calID){

			ContentResolver contentResolver = Forms.Context.ApplicationContext.ContentResolver;

			ContentValues calEvent = new ContentValues();
			calEvent.Put(CalendarContract.Events.InterfaceConsts.CalendarId, calID);
			calEvent.Put(CalendarContract.Events.InterfaceConsts.Title, title);

			double time = dstart.ToUniversalTime ().Subtract (new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

			DateTime dend = dstart;
			dend.AddHours (2);//default 2 hours long

			double timeend = dend.ToUniversalTime ().Subtract (new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

			//Console.WriteLine ("dst " + time + " eventloc: " + loc);

			calEvent.Put(CalendarContract.Events.InterfaceConsts.Dtstart, time);
			calEvent.Put(CalendarContract.Events.InterfaceConsts.Dtend, timeend);

			calEvent.Put(CalendarContract.Events.InterfaceConsts.Description, desc);
			calEvent.Put(CalendarContract.Events.InterfaceConsts.EventLocation, loc);
			calEvent.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "AEST");//fuck timezones, who even needs them

			Forms.Context.ApplicationContext.ContentResolver.Insert(CalendarContract.Events.ContentUri, calEvent);

			//Console.WriteLine ("EVENT ADDED TO PHONE MAYBE");

		}

	}
}

