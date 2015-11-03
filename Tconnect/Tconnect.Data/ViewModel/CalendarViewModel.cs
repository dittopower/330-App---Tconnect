using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Tconnect.Data;
using System.Diagnostics;


namespace Tconnect.Data.ViewModel
{
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// <para>
	/// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
	/// </para>
	/// <para>
	/// You can also use Blend to data bind with the tool's support.
	/// </para>
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class CalendarViewModel : ViewModelBase
	{
		private IMyNavigationService navigationService;

		public ObservableCollection<Note> EventView {
			get {
				var database = new NoteDatabase ();
				var x = database.GetAll ();
				return new ObservableCollection<Note> (x);
			}
		}


		public ICommand NewNoteCommand { get; private set; }
		public ICommand Import { get; private set; }
		public ICommand Purge { get; private set; }
		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public CalendarViewModel(IMyNavigationService navigationService)
		{
			this.navigationService = navigationService;
			NewNoteCommand = new Command (() => this.navigationService.NavigateTo (ViewModelLocator.EventCreatePageKey));
			Import = new Command (() => joshing());
			Purge = new Command (() => {var database = new NoteDatabase (); database.truncade(); RaisePropertyChanged (() => EventView);} );
		}

		public void OnAppearing(){
			RaisePropertyChanged (() => EventView);
		}

		private Note _selectedEvent;
		public Note SelectedEvent {
			get{ return _selectedEvent;}
			set {
				if (value == _selectedEvent)
					return;
				_selectedEvent = value;
				RaisePropertyChanged ("SelectedEvent");

				navigationService.NavigateTo (ViewModelLocator.EventPageKey,SelectedEvent.NoteId);
			}
		}

		private void joshing(){
			///Do your test import stuff here josh

			var database = new NoteDatabase();

			List<String[]> cList = DependencyService.Get<ICalendarInterface> ().getEvents();

			foreach(String[] e in cList){
				//Debug.WriteLine (e);
				/*
				 * 0=id
				 * 1=title
				 * 2=dstart
				 * 3=dend
				 * 4=desc
				 * 5=loc
				 */

				Note n = new Note (Int32.Parse(e[0]),"Title: " + e[1], mstoDateTime(e[2]), "Location: " + e[5], "Description: " + e[4],"noone attending");
				//Debug.WriteLine ("DatE: "+e[2]);
				database.InsertOrUpdateNote(n);
			}
				
			//This next line triggers the screen to update displayed data.
			RaisePropertyChanged (() => EventView);

		}

		public DateTime mstoDateTime(String s){
			Double tt = Convert.ToDouble (s);
			DateTime UnixStartDate = new DateTime(1970, 1, 1);
			return UnixStartDate.AddMilliseconds (tt);
		}

	}
}