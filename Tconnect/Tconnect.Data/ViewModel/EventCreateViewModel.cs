using System;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using Xamarin.Forms;
using Tconnect.Data.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Tconnect.Data
{
	public class EventCreateViewModel :ViewModelBase
	{
		public ICommand SaveNoteCommand { get; private set;}
		private String noteTitle;

		public String NoteTitle
		{
			get { return noteTitle; }
			set { noteTitle = value;
				RaisePropertyChanged(() => NoteTitle); }
		}


		private String noteDate;
		//make this a date at some point
		public String NoteDate
		{
			get { return noteDate; }
			set { noteDate = value;
				RaisePropertyChanged(() => NoteTitle); }
		}

		private string eventDetails;

		public string EventDetails
		{
			get { return eventDetails; }
			set { eventDetails = value;
				RaisePropertyChanged(() => EventDetails); }
		}

		private string noteLocationText;

		public string NoteLocationText
		{
			get { return noteLocationText; }
			set { noteLocationText = value;
				RaisePropertyChanged(() => NoteLocationText); }
		}


		public EventCreateViewModel (IMyNavigationService navigationService)
		{
			var database = new NoteDatabase();
			SaveNoteCommand = new Command (() => {
				database.InsertOrUpdateNote(new Note(NoteTitle,NoteDate,NoteLocationText,EventDetails));
				navigationService.GoBack();
			});
		}


	}
}

