﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tconnect
{
	public partial class CreateEvent : ContentPage
	{
		public CreateEvent ()
		{
			InitializeComponent ();

			Title = "Create Event";

			var noteList = new List<Notes> ();

			noteList.Add (new Notes ("Steve Grove"){ TimeStamp = "CellPhase Rep."});
			noteList.Add (new Notes ("Stephanie Hixon"){ TimeStamp = "Sim Sellers. CEO" });
			noteList.Add (new Notes ("Gary Malcom"){ TimeStamp = "Tech Support"});
			noteList.Add (new Notes ("Stew Pickles"){ TimeStamp = "Marketing"});

			NoteListView.ItemsSource = noteList;

		}

	}
}