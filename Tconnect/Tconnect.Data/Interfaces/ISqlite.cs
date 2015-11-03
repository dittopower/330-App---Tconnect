using System;
using SQLite.Net;

namespace Tconnect.Data
{
	public interface ISqlite
	{
		SQLiteConnection GetConnection();
	}
}