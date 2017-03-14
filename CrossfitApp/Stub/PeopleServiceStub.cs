using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossfitApp
{
	/// <summary>
	/// People service stub.
	/// </summary>
	public class PeopleServiceStub:IPeopleService
	{
		public Task<IEnumerable<IPerson>> GetPeople()
		{
			const int numberOfPeopleToGenerate = 100;
			return Task.Run(() => GeneratePeople(numberOfPeopleToGenerate));
		}

		private IEnumerable<IPerson> GeneratePeople(int personCount)
		{
			var people = new List<Person>(personCount);

			for (int i = 0; i < personCount; ++i)
			{
				people.Add(new Person(NameGenerator.GenRandomFirstName(), NameGenerator.GenRandomLastName()));
			}

			return people;
		}


	}
}
