using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossfitApp
{
	public interface IDataService
	{
		void AddPersonalRecords(IList<IPersonalRecord> personalRecords);

		void AddPersonalRecord(IPersonalRecord personalRecord);

		IEnumerable<PersonalRecord> GetPersonalRecords();
	}
}
