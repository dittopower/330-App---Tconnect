using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NoteTaker1
{
	public partial class Contacts : ContentPage
	{
		public Contacts ()
		{
			InitializeComponent ();

			Title = "Contacts";

			var noteList = new List<Notes> ();

			noteList.Add (new Notes ("Steve Grove"){ TimeStamp = "CellPhase Rep."});
			noteList.Add (new Notes ("Stephanie Hixon"){ TimeStamp = "Sim Sellers. CEO" });
			noteList.Add (new Notes ("Gary Malcom"){ TimeStamp = "Tech Support"});
			noteList.Add (new Notes ("Stew Pickles"){ TimeStamp = "Marketing"});
			noteList.Add (new Notes ("Josh Henley"){ TimeStamp = "App Developer"});

			NoteListView.ItemsSource = noteList;
		}
	}
}

