using System;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using Xamarin.Forms;
using Tconnect.Data.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Tconnect.Data
{
	public class EventCreateViewModel :ViewModelBase
	{
		public ICommand SaveNoteCommand { get; private set;}
		public Note Event =  new Note ();

		public String NoteTitle
		{
			get { return Event.titleText; }
			set { Event.titleText = value;
				RaisePropertyChanged(() => NoteTitle); }
		}

		//make this a date at some point
		public DateTime NoteDate
		{
			get { return Event.TimeStamp.Date; }
			set { Event.TimeStamp = value.Date.Add(Event.TimeStamp.TimeOfDay);
				RaisePropertyChanged(() => NoteDate); }
		}
		public TimeSpan NoteTime{
			get{ return Event.TimeStamp.TimeOfDay; }
			set{ Event.TimeStamp = Event.TimeStamp.Date.Add (value);
				RaisePropertyChanged(() => NoteTime);
			}
		}

		public string EventDetails
		{
			get { return Event.NoteDetail; }
			set { Event.NoteDetail = value;
				RaisePropertyChanged(() => EventDetails); }
		}

		public string NoteLocationText
		{
			get { return Event.LocationText; }
			set { Event.LocationText = value;
				RaisePropertyChanged(() => NoteLocationText); }
		}

		private int id = 0;
		public int ID {
			set {
				id = value;
				people = new List<Person> ();
				if (id > 0) {
					var database = new NoteDatabase ();
					Event = database.GetNote (id);
				//	Debug.WriteLine ("This");
					//Debug.WriteLine (Event.Attendees);
					if (Event.Attendees.Contains (",")) {
						var p = Event.Attendees.Split (',');
						foreach (string me in p) {
							var who = database.GetPerson (int.Parse (me));
							people.Add (who);
						}
					} else {
						int p;
						if (int.TryParse (Event.Attendees,out p)) {
							var who = database.GetPerson (p);
							people.Add (who);
						}
					}
					//Debug.WriteLine (people.Count);
				}else {
					Event = new Note ("",DateTime.Now,"","");
				}
				RaisePropertyChanged (() => NoteTitle);
				RaisePropertyChanged(() => EventDetails);
				RaisePropertyChanged(() => NoteLocationText);
				RaisePropertyChanged(() => NoteTime);
				RaisePropertyChanged(() => NoteDate);
				RaisePropertyChanged(() => PeopleView);
				RaisePropertyChanged(() => People);
			}
		}

		public ObservableCollection<Person> PeopleView {
			get {
				var database = new NoteDatabase ();
				database.tempPeople ();//Delete this line when we can input real data
				var x = database.GetAllPeople ();
				return new ObservableCollection<Person> (x);
			}
		}

		private List<Person> people = new List<Person>();
		public ObservableCollection<Person> People{
			get{
				return new ObservableCollection<Person> (people);
			}
		}

		public EventCreateViewModel (IMyNavigationService navigationService)
		{
			var database = new NoteDatabase();
			SaveNoteCommand = new Command (() => {
				Event.Attendees = "";
				foreach(Person p in people){
					Event.Attendees += p.NoteId + ",";
				}
				Event.Attendees = Event.Attendees.Trim(',');
				//Debug.WriteLine (people.ToArray().ToString());
				//Debug.WriteLine (Event.Attendees);
				database.InsertOrUpdateNote(Event);

				DependencyService.Get<ICalendarInterface> ().addToSystemCal(Event.TimeStamp, Event.titleText, Event.NoteDetail, Event.LocationText, 1);

				navigationService.GoBack();
			});
		}

		private Person _selectedPerson;
		public Person SelectedPerson {
			get{ return _selectedPerson;}
			set {
				_selectedPerson = value;
				//RaisePropertyChanged ("SelectedPerson");
				if (people.Exists(x => x.NoteId == _selectedPerson.NoteId)) {
					people.RemoveAt (people.FindIndex (x => x.NoteId == _selectedPerson.NoteId));
					//people.Remove (_selectedPerson);
				} else {
					people.Add (_selectedPerson);
				}
				//Debug.WriteLine (people.Count);
				RaisePropertyChanged (() => People);
			}
		}
	}
}

