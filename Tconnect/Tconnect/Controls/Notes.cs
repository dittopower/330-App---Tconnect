using System;

namespace Tconnect
{
	public class Notes
	{
		public string titleText { get; set; }
		public string TimeStamp { get; set; }
		public string ActionRequiredFlag { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Tconnect1.Notes"/> class.
		/// </summary>
		/// <param name="titleText">Title text.</param>
		/// <param name="timeStamp">Time stamp.</param>
		/// <param name="actionRequiredFlag">Action required flag.</param>
		public Notes (string titleText, string timeStamp = "", string actionRequiredFlag = "")
		{
			this.titleText = titleText;
			TimeStamp = timeStamp;
			ActionRequiredFlag = actionRequiredFlag;
		}
	}
}

