using System;

namespace Tconnect.Data
{
	public interface IPageLifeCycleEvents
	{
		void OnAppearing();
		void OnDisappearing();
		void OnLayoutChanged();
	}
}

