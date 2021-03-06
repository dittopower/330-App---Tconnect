﻿using System;
using System.Collections.Generic;

namespace Tconnect.Data
{
	public interface ICalendarInterface
	{
		List<String[]> getEvents(int CalID);

		List<String[]> getCalendars();

		long addToSystemCal(DateTime dstart, String title, String desc, String loc, int calID);

		void removeSystemCal (long calid);

		String[] getUserDeets(String token);

		List<String[]> contactRequest (String token);
	}
}