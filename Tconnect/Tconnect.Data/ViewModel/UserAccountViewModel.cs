using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
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
	public class UserAccountViewModel : ViewModelBase
	{
		private IMyNavigationService navigationService;
		/*Everything below here is place holder*/
		public Person Who = new Person ("John", "Smith", "you@email.com", "0466 666 666", "QUT", "");

		public string Name{
			get{return Who.Name + " " + Who.Lname; }
		}
		public string Email{
			get{return Who.Email; }
		}
		public string Phone{
			get{return Who.Phone; }
		}
		public string Org{
			get{return Who.Org; }
		}
		public string ProfilePicture{
			get{ return Who.ProfilePicture; }
		}

		private int id;
		public int ID{
			get{return id;}
			set{id = value;}
		}

		public ObservableCollection<Note> EventView {
			get {
				var database = new NoteDatabase ();
				var x = database.GetCommonEvents (id);
				return new ObservableCollection<Note> (x);
			}
		}

		public ICommand CallCommand { get; private set; }
		public ICommand EmailCommand { get; private set; }

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public UserAccountViewModel(IMyNavigationService navigationService)
		{
			this.navigationService = navigationService;
			CallCommand = new Command (() => Device.OpenUri(new Uri("tel:"+Who.Phone)));
			EmailCommand = new Command (() => Device.OpenUri(new Uri("mailto:"+Who.Email)));
			//facebook fb://messaging?id=
		}

		public void OnAppearing(){
			//Debug.WriteLine (id);
			if (id > 0) {
				var database = new NoteDatabase ();
				Who = database.GetPerson (id);
			} else {
				Who = new Person ("John", "Smith", "you@email.com", "0466 666 666", "QUT", "");
			}
			RaisePropertyChanged (() => Name);
			RaisePropertyChanged (() => Email);
			RaisePropertyChanged (() => Phone);
			RaisePropertyChanged (() => Org);
			RaisePropertyChanged (() => ProfilePicture);
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

	}
}