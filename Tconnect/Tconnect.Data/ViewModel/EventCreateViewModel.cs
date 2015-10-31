using System;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using Xamarin.Forms;
using Tconnect.Data.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics;

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
				if (id > 0) {
					var database = new NoteDatabase ();
					Event = database.GetNote (id);
				}else {
					Event = new Note ("",DateTime.Now,"","");
				}
				RaisePropertyChanged (() => NoteTitle);
				RaisePropertyChanged(() => EventDetails);
				RaisePropertyChanged(() => NoteLocationText);
				RaisePropertyChanged(() => NoteTime);
				RaisePropertyChanged(() => NoteDate);
			}
		}

		public EventCreateViewModel (IMyNavigationService navigationService)
		{
			var database = new NoteDatabase();
			SaveNoteCommand = new Command (() => {
				database.InsertOrUpdateNote(Event);
				navigationService.GoBack();
			});
		}


	}
}

