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
	public class TempMenuViewModel : ViewModelBase
	{
		private IMyNavigationService navigationService;

		public ICommand NewEventCommand { get; private set; }
		public ICommand EventsViewCommand { get; private set; }
		public ICommand CalendarCommand { get; private set; }
	//	public ICommand NewEventCommand { get; private set; }
	//	public ICommand NewEventCommand { get; private set; }
	//	public ICommand NewEventCommand { get; private set; }

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public TempMenuViewModel(IMyNavigationService navigationService)
		{
			this.navigationService = navigationService;

			NewEventCommand = new Command (() => this.navigationService.NavigateTo (ViewModelLocator.EventCreatePageKey));
			EventsViewCommand = new Command (() => this.navigationService.NavigateTo (ViewModelLocator.EventViewPageKey));
			CalendarCommand = new Command (() => this.navigationService.NavigateTo (ViewModelLocator.CalendarPageKey));
			//NewNoteCommand = new Command (() => this.navigationService.NavigateTo (ViewModelLocator.EventCreatePageKey));
			//NewNoteCommand = new Command (() => this.navigationService.NavigateTo (ViewModelLocator.EventCreatePageKey));
			//NewNoteCommand = new Command (() => this.navigationService.NavigateTo (ViewModelLocator.EventCreatePageKey));
		}

	}
}