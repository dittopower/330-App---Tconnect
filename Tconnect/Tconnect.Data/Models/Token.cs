using System;
using SQLite.Net.Attributes;
using System.Diagnostics;

namespace Tconnect.Data
{
	public class Token
	{
		[PrimaryKey]
		public string Service { get; set; }
		public string Value { get; set; }

		public Token(){
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="NoteTaker1.Notes"/> class.
		/// </summary>
		/// <param name="titleText">Title text.</param>
		/// <param name="timeStamp">Time stamp.</param>
		/// <param name="actionRequiredFlag">Action required flag.</param>
		public Token (string service, string value)
		{
			Service = service;
			Value = value;
		}
	}
}

