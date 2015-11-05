using System;
using System.Collections.Generic;

namespace Tconnect.Data
{
	public interface ICalendarInterface
	{
		List<String[]> getEvents();

		List<String[]> getCalendars();

		long addToSystemCal(DateTime dstart, String title, String desc, String loc, int calID);

		List<String[]> contactRequest (String token);
	}
}