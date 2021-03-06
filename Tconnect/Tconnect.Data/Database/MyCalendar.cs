﻿using System;
using SQLite.Net;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using Tconnect.Data;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tconnect.Data
{
	public class MyCalendar
	{
		ICalendarInterface calendar;
		NoteDatabase database;
		static bool SyncContacts = false;
		static bool SyncCalendar = false;

		public MyCalendar ()
		{
			calendar = DependencyService.Get<ICalendarInterface> ();
			database = new NoteDatabase();
		}

		public List<String[]> getEvents(){
			return calendar.getEvents (int.Parse(database.GetToken("Calendar").Value));
		}
		public void UpdateCal(){
			if (!SyncCalendar) {
				SyncCalendar = true;

				Note n;
				foreach (String[] e in getEvents()) {
					//Debug.WriteLine (e);
					/*
					 * 0=id
					 * 1=title
					 * 2=dstart
					 * 3=dend
					 * 4=desc
					 * 5=loc
					 */
					n = new Note (e [1], mstoDateTime (e [2]), e [5], e [4], "");
					n.CalId = int.Parse (e [0]);
					Debug.WriteLine (n.titleText);
					Debug.WriteLine (n.TimeStamp);
					database.CalendarInsertOrUpdateNote (n);
				}

				SyncCalendar = false;
			}
		}
			
		public List<String[]> getCalendars(){
			return calendar.getCalendars ();
		}

		public String[] getUserDeets(String token){
			return calendar.getUserDeets (token);
		}

		public void contactRequest(){
			if (!SyncContacts) {
				SyncContacts = true;

				List<String[]> ppl = calendar.contactRequest (database.GetToken ("Yammer").Value);
				foreach (String[] s in ppl) {
					database.InsertOrUpdatePerson (new Person (s [0], s [1], s [2], s [3], s [4], s [5], s [6],s[7]));
					//Debug.WriteLine ("Adding " + s[5]);
				}

				SyncContacts = false;
			}
		}

		public long addToSystemCal(DateTime dstart, String title, String desc, String loc){
			return calendar.addToSystemCal (dstart, title, desc, loc, int.Parse(database.GetToken("Calendar").Value));
		}

		public void removeSystemCal(long calid){
			calendar.removeSystemCal (calid);
		}

		public DateTime mstoDateTime(String s){
			//Debug.WriteLine (s);
			Double tt = Convert.ToDouble (s);
			DateTime UnixStartDate = new DateTime(1970, 1, 1);
			UnixStartDate = UnixStartDate.AddMilliseconds (tt);
			UnixStartDate = UnixStartDate.AddHours(20);//Account for weird time loss
			return UnixStartDate;
		}
		//dsfds*************************************************************************************


		//Async methods
		static public async void ImportContacts(){
			MyCalendar cal = new MyCalendar ();
			var t = Task.Factory.StartNew(()=> cal.contactRequest ());
			await t;
			Debug.WriteLine("done Contacts import");
		}

		static public async void ImportCalendar(){
			MyCalendar cal = new MyCalendar ();
			var t = Task.Factory.StartNew(()=> cal.UpdateCal());
			await t;
			Debug.WriteLine("done Calendar import");
		}

	}
}