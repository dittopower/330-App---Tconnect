using System;
using System.Collections.Generic;

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
//TODO show IOC vs Dependency Injection

[assembly: Dependency (typeof (SqliteDroid))]

namespace Tconnect.Droid
{
	public class SqliteDroid : ISqlite
	{
		public SqliteDroid(){
		}
		#region ISqlite implementation

		public SQLiteConnection GetConnection ()
		{
			const string sqliteFilename = "database.db3";
			var documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var path = Path.Combine (documentsPath, sqliteFilename);
			var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			//Create the connection
			var conn = new SQLiteConnection(plat,path);

			return conn;
		}

		public List<String[]> getEvents()
		{       
			/*var eventsUri = CalendarContract.Events.ContentUri;

			string[] eventsProjection = { 
				CalendarContract.Events.InterfaceConsts.Id,
				CalendarContract.Events.InterfaceConsts.Title,
				CalendarContract.Events.InterfaceConsts.Dtstart
			};
				
			var cursor = ManagedQuery (eventsUri, eventsProjection, String.Format ("calendar_id={0}", calendarid), null, "dtstart ASC");

			List<String[]> things = new List<String[]> ();

			if (cursor.MoveToFirst()){
				do{

					String calid = cursor.GetString(cursor.GetColumnIndex("_id"));
					String title = cursor.GetString(cursor.GetColumnIndex("title"));
					String Dstart = cursor.GetString(cursor.GetColumnIndex("dtstart"));

					things.Add(new String[] {calid,title,Dstart});

				}while(cursor.MoveToNext());
			}

			cursor.Close();

			return things;*/

			List<String[]> things = new List<String[]> ();
			things.Add (new String[] {"1","new title 1","doesnm atter"});
			things.Add (new String[] {"1","new title 2","doesnm atter"});
			things.Add (new String[] {"1","new title 3","doesnm atter"});
			things.Add (new String[] {"1","new title 4","doesnm atter"});
			things.Add (new String[] {"1","new title 5","doesnm atter"});
			return things;
		}


		public List<String[]> getCalendars(){
			// List Calendars
			/*var calendarsUri = CalendarContract.Calendars.ContentUri;

			string[] calendarsProjection = {
				CalendarContract.Calendars.InterfaceConsts.Id,
				CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
				CalendarContract.Calendars.InterfaceConsts.AccountName
			};

			var cursor = ManagedQuery (calendarsUri, calendarsProjection, null, null, null);

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

			return calendars;*/

			return new List<String[]> ();

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

