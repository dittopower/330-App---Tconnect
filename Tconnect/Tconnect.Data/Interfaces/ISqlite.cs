using System;
using System.Collections.Generic;
using SQLite.Net;

namespace Tconnect.Data
{
	public interface ISqlite
	{
		SQLiteConnection GetConnection();

		List<String[]> getEvents();

		List<String[]> getCalendars();
	}
}