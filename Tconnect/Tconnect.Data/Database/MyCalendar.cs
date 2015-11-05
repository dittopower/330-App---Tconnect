﻿using System;
using SQLite.Net;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using Tconnect.Data;

namespace Tconnect.Data
{
	public class MyCalendar
	{
		ICalendarInterface calendar;
		NoteDatabase database;
		public MyCalendar ()
		{
			calendar = DependencyService.Get<ICalendarInterface> ();
			database = new NoteDatabase();
		}

		public List<String[]> getEvents(){
			return calendar.getEvents ();
		}
		public void UpdateCal(){
			Note n;
			foreach(String[] e in getEvents()){
				//Debug.WriteLine (e);
				/*
				 * 0=id
				 * 1=title
				 * 2=dstart
				 * 3=dend
				 * 4=desc
				 * 5=loc
				 */
				n = new Note (e[1],mstoDateTime(e[2]), e[5], e[4],"");
				n.CalId = int.Parse (e [0]);
				database.CalendarInsertOrUpdateNote(n);
			}
		}
			
		public List<String[]> getCalendars(){
			return calendar.getCalendars ();
		}

		public void contactRequest(){
			List<String[]> ppl = calendar.contactRequest (database.GetToken("Yammer").Value);
			foreach(String[] s in ppl){
				database.InsertOrUpdatePerson (new Person(s[0],s[1],s[2],s[3],s[4],s[5],s[6]));
				//Debug.WriteLine ("Adding " + s[5]);
			}
		}

		public void addToSystemCal(DateTime dstart, String title, String desc, String loc, int calID){
			calendar.addToSystemCal (dstart, title, desc, loc, calID);
		}

		public DateTime mstoDateTime(String s){
			//Debug.WriteLine (s);
			Double tt = Convert.ToDouble (s);
			DateTime UnixStartDate = new DateTime(1970, 1, 1);
			UnixStartDate = UnixStartDate.AddMilliseconds (tt);
			UnixStartDate = UnixStartDate.AddHours(20);
			return UnixStartDate;
		}

	}
}