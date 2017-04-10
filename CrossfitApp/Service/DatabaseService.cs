using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossfitApp;
using SQLite.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseService))]
namespace CrossfitApp
{
	public class DatabaseService : IDataService
	{
		//private static readonly AsyncLocker Locker = new AsyncLocker();

		//private SQLiteAsyncConnection Database { get; } = DependencyService.Get<ISQLite>().GetAsyncConnection();

		public SQLiteConnection _connection;

		public DatabaseService()
		{
			_connection = DependencyService.Get<ISQLite>().GetConnection();
			_connection.CreateTable<PersonalRecord>();
		}

		public IEnumerable<PersonalRecord> GetPersonalRecords()
		{
			return _connection.Table<PersonalRecord>().ToList();
		}

		public void AddPersonalRecord(IPersonalRecord personalRecord)
		{
			_connection.Insert(personalRecord);
		}

		public void AddPersonalRecords(IList<IPersonalRecord> personalRecords)
		{
			_connection.InsertAll(personalRecords);
		}

		//public async Task AddPersonalRecord(IPersonalRecord personalRecord)
		//{
		//	using (await Locker.LockAsync())
		//	{
		//		await Database.InsertAsync(personalRecord);
		//	}
		//}

		//public async Task AddPersonalRecords(IList<IPersonalRecord> personalRecords)
		//{
		//	using (await Locker.LockAsync())
		//	{
		//		await Database.InsertAllAsync(personalRecords);
		//	}
		//}

		//public async Task<IList<PersonalRecord>> GetPersonalRecords()
		//{
		//	using (await Locker.LockAsync())
		//	{
		//		return await Database.Table<PersonalRecord>().Where(x => x.ID > 0).ToListAsync();
		//	}
		//}
	}
}
