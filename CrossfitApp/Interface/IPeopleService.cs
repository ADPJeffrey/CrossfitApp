using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossfitApp
{
	/// <summary>
	/// Interface which provices all neccessary methods for peoples.
	/// </summary>
	public interface IPeopleService
	{
		Task<IEnumerable<IPerson>> GetPeople();
	}
}
