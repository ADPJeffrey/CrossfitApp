using System;
using System.IO;
using CrossfitApp.Droid;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace CrossfitApp.Droid
{
	public class SQLite_Android : ISQLite
	{
		//private SQLiteConnectionWithLock _conn;

		public SQLite_Android()
		{

		}

		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var fileName = "PersonalRecords.db3";
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, fileName);

			var platform = new SQLitePlatformAndroid();
			var connection = new SQLiteConnection(platform, path);

			return connection;
		}

		//private static string GetDatabasePath()
		//{
		//	const string sqliteFilename = "mydatabase.db3";

		//	string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
		//	var path = Path.Combine(documentsPath, sqliteFilename);

		//	return path;
		//}

		//public SQLiteConnection GetConnection()
		//{
		//	var dbPath = GetDatabasePath();

		//	// Return the synchronous database connection 
		//	return new SQLiteConnection(new SQLitePlatformAndroid(), dbPath);
		//}

		//public SQLiteAsyncConnection GetAsyncConnection()
		//{
		//	var dbPath = GetDatabasePath();

		//	var platForm = new SQLitePlatformAndroid();

		//	var connectionFactory = new Func<SQLiteConnectionWithLock>(
		//		() =>
		//		{
		//			if (_conn == null)
		//			{
		//				_conn =
		//					new SQLiteConnectionWithLock(platForm,
		//						new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: true));
		//			}
		//			return _conn;
		//		});

		//	return new SQLiteAsyncConnection(connectionFactory);
		//}

		//public void DeleteDatabase()
		//{
		//	var path = GetDatabasePath();

		//	try
		//	{
		//		if (_conn != null)
		//		{
		//			_conn.Close();

		//		}
		//	}
		//	catch
		//	{
		//		// Best effort close. No need to worry if throws an exception
		//	}

		//	if (File.Exists(path))
		//	{

		//		File.Delete(path);
		//	}

		//	_conn = null;
		//}

		//public void CloseConnection()
		//{
		//	if (_conn != null)
		//	{
		//		_conn.Close();
		//		_conn.Dispose();
		//		_conn = null;

		//		// Must be called as the disposal of the connection is not released until the GC runs.
		//		GC.Collect();
		//		GC.WaitForPendingFinalizers();
		//	}
		//}
	}
}
