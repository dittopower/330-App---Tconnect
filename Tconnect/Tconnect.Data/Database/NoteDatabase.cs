using System;
using SQLite.Net;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using Tconnect.Data;

namespace Tconnect.Data
{
	public class NoteDatabase
	{
		SQLiteConnection database;
		public NoteDatabase ()
		{
			database = DependencyService.Get<ISqlite> ().GetConnection ();
			//TODO show IOC vs Dependency injection
//			database = SimpleIoc.Default.GetInstance<ISqlite> ().GetConnection ();
			if (database.TableMappings.All(t => t.MappedType.Name != typeof(Note).Name)) {
				database.CreateTable<Note> ();
				database.Commit ();
			}
			if (database.TableMappings.All(t => t.MappedType.Name != typeof(Person).Name)) {
				database.CreateTable<Person> ();
				database.Commit ();
			}
			if (database.TableMappings.All(t => t.MappedType.Name != typeof(Token).Name)) {
				database.CreateTable<Token> ();
				database.Commit ();
			}
		}


	//Notes/Events
		public List<Note> GetAll(){
			var items = database.Table<Note> ().OrderBy(t => t.TimeStamp).ToList<Note>();

			return items;
		}

		public int InsertOrUpdateNote(Note note){
			return database.Table<Note> ().Where (x => x.NoteId == note.NoteId).Count () > 0 
				? database.Update (note) : database.Insert (note);
		}

		public int CalendarInsertOrUpdateNote(Note note){
			if (database.Table<Note> ().Where (x => x.CalId == note.CalId).Count () > 0) {
				var n = database.Table<Note> ().First (t => t.CalId == note.CalId);
				note.NoteId = n.NoteId;
				note.Attendees = n.Attendees;
			}
			return InsertOrUpdateNote (note);
		}

		public Note GetNote(int key){
			return database.Table<Note> ().First (t => t.NoteId == key); 
		}

		public Note NextNote(){
			return database.Table<Note> ().Where (t => t.TimeStamp >= DateTime.Now).OrderBy (t => t.TimeStamp).First();
		}

		public List<Note> FutureNotes(){
			var items = database.Table<Note> ().Where (t => t.TimeStamp >= DateTime.Now).OrderBy (t => t.TimeStamp).ToList<Note>();

			return items;
		}

		public void truncade (){
			database.DropTable<Note> ();
			if (database.TableMappings.All(t => t.MappedType.Name != typeof(Note).Name)) {
				database.CreateTable<Note> ();
				database.Commit ();
			}
		}

		public void truncadePerson (){
			database.DropTable<Person> ();
			if (database.TableMappings.All(t => t.MappedType.Name != typeof(Person).Name)) {
				database.CreateTable<Person> ();
				database.Commit ();
			}
		}

		public List<Note> GetCommonEvents(string id){
			var items = database.Table<Note> ().Where(t => t.Attendees.Contains(id)).ToList<Note>();
			var res = new List<Note> ();
			foreach (var i in items) {
				if (i.Attendees.Split (',').Contains (id)) {
					res.Add (i);
				}
			}
			return res;
		}
		public List<Note> GetCommonEvents(int id){
			return GetCommonEvents(id.ToString());
		}
		public List<Note> GetCommonEvents(Person who){
			return GetCommonEvents(who.NoteId);
		}



	//Contacts

		public List<Person> GetAllPeople(){
			var items = database.Table<Person> ().OrderBy (t => t.Lname).OrderBy (t => t.Name).ToList<Person>();
			return items;
		}

		public Person GetPerson(int key){
			return database.Table<Person> ().First (t => t.NoteId == key); 
		}

		public int InsertOrUpdatePerson(Person person){
			return database.Table<Person> ().Where (x => x.NoteId == person.NoteId).Count () > 0 
				? database.Update (person) : database.Insert (person);
		}
		/*
		public void tempPeople(){
			int c = database.Table<Person> ().Count();
			if (c < 1) {
				InsertOrUpdatePerson(new Person ("Josh","Henley","roflmonsterjh@gmail.com","04*****","Adhesive Tech"));
				InsertOrUpdatePerson(new Person ("Damon","Jones","dittopower@gmail.com","0466971872","Adhesive Tech"));
				InsertOrUpdatePerson(new Person ("Jordan","Beak","mrsmiley95@gmail.com","04*****","Adhesive Tech"));
			}
		}*/



		//Tokens

		public int InsertOrUpdateToken(Token token){
			return database.Table<Token> ().Where (x => x.Service == token.Service).Count () > 0 
				? database.Update (token) : database.Insert (token);
		}

		public Token GetToken(string key){
			return database.Table<Token> ().Where(t => t.Service == key).First (); 
		}

		public bool HasToken(string key){
			return (database.Table<Token> ().Where (t => t.Service == key).Count () > 0);
		}

		public int LoseToken(string key){
			return database.Table<Token> ().Delete(t => t.Service == key); 
		}

	}
}

