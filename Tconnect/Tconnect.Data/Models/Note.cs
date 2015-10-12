using System;

namespace Tconnect.Data
{
	public class Note
	{
		public string titleText { get; set; }
		public DateTime TimeStamp { get; set; }
		public string NoteDetail { get; set; }
		public string LocationText { get; set; }

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
		}
	}
}

