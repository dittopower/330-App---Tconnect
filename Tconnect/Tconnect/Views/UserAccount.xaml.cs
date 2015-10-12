﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tconnect
{
	public partial class UserAccount : BaseView
	{
		public UserAccount ()
		{
			InitializeComponent ();
			base.Init ();
			BindingContext = App.Locator.UserAccount;

			Title = "User Details";

			/*var noteList = new List<Notes> ();

			noteList.Add (new Notes ("Meeting with Cellcare"){ TimeStamp = "11:00 am"});
			noteList.Add (new Notes ("Recollection Meeting"){ TimeStamp = "12:00 pm" });
			noteList.Add (new Notes ("Help out Jeff"){ TimeStamp = "2:00 pm"});

			NoteListView.ItemsSource = noteList;*/
		}
	}
}

