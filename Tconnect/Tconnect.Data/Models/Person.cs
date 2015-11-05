using System;
using SQLite.Net.Attributes;

namespace Tconnect.Data
{
	public class Person
	{
		[PrimaryKey, AutoIncrement]
		public int NoteId { get; set; }
		[NotNull]
		public string Name { get; set; }
		public string Lname { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Yammer { get; set; }
		public string Org { get; set; }
		public string ProfilePicture { get; set; }
		public string MiniProfilePicture { get; set; }

		public Person(){
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="NoteTaker1.Notes"/> class.
		/// </summary>
		/// <param name="titleText">Title text.</param>
		/// <param name="timeStamp">Time stamp.</param>
		/// <param name="actionRequiredFlag">Action required flag.</param>
		public Person (string name, string lname = "", string email = "", string phone = "", string org = "", string yammer = "", string profilepicture="ProfileImage.jpg", string miniprofilepicture="ProfileImage.jpg")
		{
			Name = name;
			Email = email;
			Phone = phone;
			Lname = lname;
			Yammer = yammer;
			Org = org;
			ProfilePicture = profilepicture;
			MiniProfilePicture = miniprofilepicture;
		}
	}
}