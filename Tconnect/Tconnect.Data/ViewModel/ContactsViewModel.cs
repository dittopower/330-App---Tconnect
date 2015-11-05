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
	public class ContactsViewModel : ViewModelBase
	{
		private IMyNavigationService navigationService;

		public ObservableCollection<Person> PeopleView {
			get {
				var database = new NoteDatabase ();
				//database.tempPeople ();//Delete this line when we can input real data
				var x = database.GetAllPeople ();
				return new ObservableCollection<Person> (x);
			}
		}

		public ContactsViewModel (IMyNavigationService navigationService)
		{
			this.navigationService = navigationService;
			RaisePropertyChanged (() => PeopleView);
		}

		public void OnAppearing(){
			//RaisePropertyChanged (() => PeopleView);
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