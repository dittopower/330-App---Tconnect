using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Tconnect.Data;
using Microsoft.Practices.ServiceLocation;

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
				if (id > 0) {
					var database = new NoteDatabase ();
					Event = database.GetNote (id);
					RaisePropertyChanged (() => Event);
				}
			}
		}

		public ICommand NewNoteCommand { get; private set; }
		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public EventViewModel(IMyNavigationService navigationService)
		{
			this.navigationService = navigationService;
			NewNoteCommand = new Command (() => this.navigationService.NavigateTo (ViewModelLocator.EventCreatePageKey));
		}

	}
}