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
using System.Net;
using System.IO;
using Org.Json;


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
				CalendarContract.Events.InterfaceConsts.EventLocation,
				CalendarContract.Events.InterfaceConsts.EventTimezone
			};
			//Date d = new Date ();
			Java.Util.Calendar c = Java.Util.Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("Australia/Brisbane"));

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
					Console.WriteLine(cursor.GetString(cursor.GetColumnIndex( CalendarContract.Events.InterfaceConsts.EventTimezone)));
					things.Add(new String[] {calid,title,Dstart,Dend,desc,loc});

					//Console.WriteLine("ID: " + calid);

				}while(cursor.MoveToNext());
			}

			cursor.Close();
			cursor.Dispose ();

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
			cursor.Dispose ();

			return calendars;

		}
			
		public long addToSystemCal(DateTime dstart, String title, String desc, String loc, int calID){

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
			//CalendarContract.Events.InterfaceConsts.Id

			calEvent.Put(CalendarContract.Events.InterfaceConsts.Description, desc);
			calEvent.Put(CalendarContract.Events.InterfaceConsts.EventLocation, loc);
			calEvent.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "Australia/Brisbane");//fuck timezones, who even needs them

			long newid = getNewEventId ();
			calEvent.Put(CalendarContract.Events.InterfaceConsts.Id, newid);

			Forms.Context.ApplicationContext.ContentResolver.Insert(CalendarContract.Events.ContentUri, calEvent);

			return newid;
			//Console.WriteLine ("EVENT ADDED TO PHONE MAYBE");

		}

		public void removeSystemCal(long calid){

			Forms.Context.ApplicationContext.ContentResolver.Delete (CalendarContract.Events.ContentUri, "_id="+calid, null);
			//Forms.Context.ApplicationContext.ContentResolver.Insert(CalendarContract.Events.ContentUri, calEvent);
		}

		public static long getNewEventId(){  
			ICursor cursor = Forms.Context.ApplicationContext.ContentResolver.Query (CalendarContract.Events.ContentUri, new String [] {"MAX(_id) as max_id"}, null, null, "_id");
			cursor.MoveToFirst ();
			long max_val = cursor.GetLong(cursor.GetColumnIndex("max_id"));

			cursor.Close();
			cursor.Dispose ();

			return max_val+1;
		}

		public List<String[]> contactRequest(String token){/* stops working after the first 10 because REST api limit */

			String output = httpRequest(token,"https://www.yammer.com/api/v1/users.json");

			if (output != "") {
				JSONArray jObject = new JSONArray (output); // json
				List<String[]> ppl = new List<String[]> ();

				//int c = 0;
				JSONObject data, tmp;
				String[] items;
				String uid,fname,lname,email,phone="",org,profilepicture,miniprofilepicture;
				//maybe use less memory because im using heaps apparently

				for (int i = 0; i < jObject.Length (); i++) {
					//if(i < 8){//limit for now
						data = jObject.GetJSONObject (i); // get data object
						uid = data.GetString ("id");

					//userdeets = httpRequest (token, "https://www.yammer.com/api/v1/users/" + uid + ".json");

					//if (userdeets != "") {
						//Console.WriteLine (uid);

						//person = new JSONObject (userdeets); // get data object

						fname = data.GetString ("first_name");
						lname = data.GetString ("last_name");
						email = data.GetString ("email");
						profilepicture = data.GetString ("mugshot_url_template");
						profilepicture = profilepicture.Replace ("{width}x{height}", "300x300");

						miniprofilepicture = data.GetString ("mugshot_url");

						tmp = data.GetJSONObject ("contact");
						phone = tmp.GetString ("phone_numbers");

						try{
							tmp = new JSONArray (phone).GetJSONObject (0);
							phone = tmp.GetString ("number");
						}
						catch{
							phone = "";
						}

						org = data.GetString ("network_name");

					//Console.WriteLine (fname + " : " + lname + " : " + email + " : " + phone + " : " + org + " : " + uid);

						items = new String[] { fname, lname, email, phone, org, uid, profilepicture, miniprofilepicture };
						ppl.Add (items);
						tmp.Dispose ();
						data.Dispose ();

						fname = "";
						lname = "";
						email = "";
						phone = "";
						org = "";
						uid = "";
						profilepicture = "";

					//}//individual users
					//c++;

					//}//limit

				}//loop
				jObject.Dispose();

				return ppl;

			}//get all users

			return new List<String[]>();

		}//contactRequest

		public String[] getUserDeets(String token){
			Console.WriteLine ("into the belly of it m8");
			String output = httpRequest(token,"https://www.yammer.com/api/v1/users/current.json");
			Console.WriteLine ("we dun it now");
			if (output != "") {
				
				//JSONArray jObject = new JSONArray (output); // json
				String[] details;

				JSONObject data, tmp;
				//String[] items;
				String uid, fname, lname, email, phone = "", org, profilepicture;
				//maybe use less memory because im using heaps apparently

				data = new JSONObject (output); // get data object

				uid = data.GetString ("id");
				fname = data.GetString ("first_name");
				lname = data.GetString ("last_name");
				email = data.GetString ("email");
				profilepicture = data.GetString ("mugshot_url_template");
				profilepicture = profilepicture.Replace ("{width}x{height}", "300x300");

				tmp = data.GetJSONObject ("contact");
				phone = tmp.GetString ("phone_numbers");

				try {
					tmp = new JSONArray (phone).GetJSONObject (0);
					phone = tmp.GetString ("number");
				} catch {
					phone = "";
				}

				org = data.GetString ("network_name");

				details = new String[] { fname, lname, email, phone, org, uid, profilepicture };
				tmp.Dispose ();
				data.Dispose ();

				fname = "";
				lname = "";
				email = "";
				phone = "";
				org = "";
				uid = "";
				profilepicture = "";

				//jObject.Dispose ();

				return details;

			}
			String[] gg = null;
			return gg;

		}//getuserdeets

		public String httpRequest(String token, String url){
			try{
				HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;  
				request.Headers.Add ("Authorization","Bearer " + token);//fuck this line especially

				// Get response  
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)  
				{  
					// Get the response stream  
					StreamReader reader = new StreamReader(response.GetResponseStream());  

					return reader.ReadToEnd();

				}
			}
			catch{
				Console.WriteLine ("Ur internet got dead");
				return "";
			}

		}//contactRequest

	}
}

