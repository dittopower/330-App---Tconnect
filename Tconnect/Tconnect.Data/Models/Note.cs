﻿using System;
using SQLite.Net.Attributes;
using System.Diagnostics;

namespace Tconnect.Data
{
	public class Note
	{
		[PrimaryKey, AutoIncrement]
		public int NoteId { get; set; }
		[NotNull, MaxLength(128)]
		public string titleText { get; set; }
		public DateTime TimeStamp { get; set; }
		public string LocationText { get; set; }
		public string NoteDetail { get; set; }

		public Note(){
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="NoteTaker1.Notes"/> class.
		/// </summary>
		/// <param name="titleText">Title text.</param>
		/// <param name="timeStamp">Time stamp.</param>
		/// <param name="actionRequiredFlag">Action required flag.</param>
		public Note (string titleText, DateTime timeStamp, string locationText = "", string noteDetail = "")
		{
			this.titleText = titleText;
			TimeStamp = timeStamp;
			LocationText = locationText;
			NoteDetail = noteDetail;
			Debug.WriteLine (timeStamp);
		}
		public Note (int id, string titleText, DateTime timeStamp, string locationText = "", string noteDetail = "")
		{
			NoteId = id;
			this.titleText = titleText;
			TimeStamp = timeStamp;
			LocationText = locationText;
			NoteDetail = noteDetail;
			Debug.WriteLine (timeStamp);
		}
	}
}

