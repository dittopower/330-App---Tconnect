using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tconnect
{
	public partial class EventView : ContentPage
	{
		public EventView ()
		{
			InitializeComponent ();

			Title = "Event View";

			var noteList = new List<Notes> ();

			noteList.Add (new Notes ("Steve Grove"){ TimeStamp = "CellPhase Rep."});
			noteList.Add (new Notes ("Stephanie Hixon"){ TimeStamp = "Sim Sellers. CEO" });
			noteList.Add (new Notes ("Gary Malcom"){ TimeStamp = "Tech Support"});
			noteList.Add (new Notes ("Stew Pickles"){ TimeStamp = "Marketing"});

			NoteListView.ItemsSource = noteList;
		}
	}
}

