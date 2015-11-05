using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
	public class TempMenuViewModel : ViewModelBase
	{
		private IMyNavigationService navigationService;

		public ICommand LogoutCommand { get; private set; }
		public ICommand ContactsCommand { get; private set; }
		public ICommand PurgeCommand { get; private set; }
		public ICommand CalendarCommand { get; private set; }

		NoteDatabase database = new NoteDatabase ();

		public ObservableCollection<String[]> Cals {
			get {
				var mcal = new MyCalendar();
				//database.tempPeople ();//Delete this line when we can input real data
				var x = mcal.getCalendars();
				Debug.WriteLine (x [0]);
				return new ObservableCollection<String[]> (x);
			}
		}


		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public TempMenuViewModel(IMyNavigationService navigationService)
		{
			this.navigationService = navigationService;
			LogoutCommand = new Command (() => {
				database.LoseToken("Yammer");
				navigationService.NavigateTo(ViewModelLocator.FeedPageKey);
			});
			ContactsCommand = new Command (() => {
				database.truncadePerson();
				MyCalendar.ImportContacts();
			});
			CalendarCommand = new Command (() => {
				database.truncade();
				MyCalendar.ImportCalendar();
			});
			PurgeCommand = new Command (() => {
				database.truncade();
				database.truncadePerson();
			});

		}



		private String[] _selectedCal;
		public String[] SelectedCal {
			get{ return _selectedCal;}
			set {
				if (value == _selectedCal)
					return;
				_selectedCal = value;
				RaisePropertyChanged ("SelectedPerson");
				Debug.WriteLine (_selectedCal[0]);

				database.InsertOrUpdateToken(new Tconnect.Data.Token("Calendar",_selectedCal[0]));
			}
		}

	}
}