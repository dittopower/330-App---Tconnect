using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

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
	public class MainFeedViewModel : ViewModelBase
	{
		private IMyNavigationService navigationService;

		public ObservableCollection<Note> EventView {
			get {
				var database = new NoteDatabase ();
				var x = database.FutureNotes ();
				return new ObservableCollection<Note> (x);
			}
		}

		public Note TopNote{
			get {
				var database = new NoteDatabase ();
				var x = database.NextNote ();
				return x;
			}
		}


		public ICommand NewNoteCommand { get; private set; }
		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainFeedViewModel(IMyNavigationService navigationService)
		{
			this.navigationService = navigationService;
			NewNoteCommand = new Command (() => this.navigationService.NavigateTo (ViewModelLocator.EventCreatePageKey));
		}

		public void OnAppearing(){
			RaisePropertyChanged (() => EventView);
			RaisePropertyChanged (() => TopNote);
		}

	}
}