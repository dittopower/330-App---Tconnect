using System;
using System.Collections.Generic;

namespace Tconnect.Data
{
	public interface ICalendarInterface
	{
		List<String[]> getEvents();

		List<String[]> getCalendars();

		void addToSystemCal(DateTime dstart, String title, String desc, String loc, int calID);

		List<String[]> contactRequest (String token);
	}
}