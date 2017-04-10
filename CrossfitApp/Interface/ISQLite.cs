using System;
using SQLite.Net;
using SQLite.Net.Async;

namespace CrossfitApp
{
	public interface ISQLite
	{
		//void CloseConnection();
		SQLiteConnection GetConnection();
		//SQLiteAsyncConnection GetAsyncConnection();
		//void DeleteDatabase();
	}
}
