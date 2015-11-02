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

		private int id;
		public int ID{
			get{return id;}
			set{id = value;
				if (id > 0) {
					Debug.WriteLine (id);
					var database = new NoteDatabase ();
					Who = database.GetPerson (id);
				} else {
					Who = new Person ("John", "Smith", "you@email.com", "0466 666 666", "QUT", "");
				}
				RaisePropertyChanged (() => Name);
				RaisePropertyChanged (() => Email);
				RaisePropertyChanged (() => Phone);
				RaisePropertyChanged (() => Org);
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
			RaisePropertyChanged (() => Who);
		}

	}
}