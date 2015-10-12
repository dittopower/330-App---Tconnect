using System;

namespace Tconnect.Data
{
	public class Person
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }

		public Person(){
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="NoteTaker1.Notes"/> class.
		/// </summary>
		/// <param name="titleText">Title text.</param>
		/// <param name="timeStamp">Time stamp.</param>
		/// <param name="actionRequiredFlag">Action required flag.</param>
		public Person (string name, string email = "", string phone = "")
		{
			Name = name;
			Email = email;
			Phone = phone;
		}
	}
}

