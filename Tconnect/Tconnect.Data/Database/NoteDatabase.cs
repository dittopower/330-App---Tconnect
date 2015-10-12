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
		}


	//Notes/Events
		public List<Note> GetAll(){
			var items = database.Table<Note> ().ToList<Note>();

			return items;
		}

		public int InsertOrUpdateNote(Note note){
			return database.Table<Note> ().Where (x => x.titleText == note.titleText).Count () > 0 
				? database.Update (note) : database.Insert (note);
		}

		public Note GetNote(string key){
			return database.Table<Note> ().First (t => t.titleText == key); 
		}

		public Note NextNote(){
			return database.Table<Note> ().Where (t => t.TimeStamp >= DateTime.Now).OrderBy (t => t.TimeStamp).First();
		}

		public List<Note> FutureNotes(){
			var items = database.Table<Note> ().Where (t => t.TimeStamp >= DateTime.Now).OrderBy (t => t.TimeStamp).Skip(1).ToList<Note>();

			return items;
		}


	//Contacts

		public List<Person> GetAllPeople(){
			var items = database.Table<Person> ().ToList<Person>();
			return items;
		}

		public int InsertOrUpdatePerson(Person person){
			return database.Table<Person> ().Where (x => x.Name == person.Name).Count () > 0 
				? database.Update (person) : database.Insert (person);
		}

		public void tempPeople(){
			int c = database.Table<Person> ().Count();
			if (c < 1) {
				InsertOrUpdatePerson(new Person ("Steve Grove","CellPhase Rep.","+61 5555 5555"));
				InsertOrUpdatePerson(new Person ("Stephanie Hixon","Sim Sellers. CEO", "3921111"));
				InsertOrUpdatePerson(new Person ("Gary Malcom","Tech Support","42"));
				InsertOrUpdatePerson(new Person ("Stew Pickles","Marketing","666"));
				InsertOrUpdatePerson(new Person ("Josh Henley","App Developer","04*****"));
			}
		}

	}
}

