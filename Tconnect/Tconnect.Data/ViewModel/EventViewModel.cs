using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Tconnect.Data;
using Microsoft.Practices.ServiceLocation;
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
	public class EventViewModel : ViewModelBase
	{
		private IMyNavigationService navigationService;
		/*Everything below here is place holder*/
		private Note _event = null;
		public Note Event {
			get{ return _event; }
			set { _event = value;}
		}

		private int id = 0;
		public int ID {
			set {
				id = value;
			}
		}

		private List<Person> people = new List<Person>();
		public ObservableCollection<Person> People{
			get{
				return new ObservableCollection<Person> (people);
			}
		}

		public void OnAppearing(){
			if (id > 0) {
				var database = new NoteDatabase ();
				Event = database.GetNote (id);
				//Debug.WriteLine (Event.TimeStamp);
				people = new List<Person> ();
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
				RaisePropertyChanged(() => People);
				RaisePropertyChanged (() => Event);
			}
			//RaisePropertyChanged (() => Event);
		}

		public ICommand EditCommand { get; private set; }
		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public EventViewModel(IMyNavigationService navigationService)
		{
			this.navigationService = navigationService;
			EditCommand = new Command (() => this.navigationService.NavigateTo (ViewModelLocator.EventCreatePageKey,id));

		}

		private Person _selectedPerson;
		public Person SelectedPerson {
			get{ return _selectedPerson;}
			set {
				if (value == _selectedPerson)
					return;
				_selectedPerson = value;
				RaisePropertyChanged ("SelectedPerson");
				Debug.WriteLine (_selectedPerson.NoteId);

				navigationService.NavigateTo (ViewModelLocator.UserAccountPageKey,_selectedPerson.NoteId);
			}
		}

	}
}