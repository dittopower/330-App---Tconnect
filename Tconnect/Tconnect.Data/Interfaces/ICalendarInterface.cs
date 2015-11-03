using System;
using System.Collections.Generic;

namespace Tconnect.Data
{
	public interface ICalendarInterface
	{
		List<String[]> getEvents();

		List<String[]> getCalendars();
	}
}