using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tconnect
{
	public partial class Calendar : ContentPage
	{
		public Calendar ()
		{
			InitializeComponent ();

			Title = "Calendar";

			var noteList = new List<Notes> ();

			noteList.Add (new Notes ("Meeting with Cellcare"){ TimeStamp = "11:00 am"});
			noteList.Add (new Notes ("Recollection Meeting"){ TimeStamp = "12:00 pm" });
			noteList.Add (new Notes ("Visit Brisbane branch"){ TimeStamp = "1:00 pm"});

			NoteListView.ItemsSource = noteList;

		}

	}
}