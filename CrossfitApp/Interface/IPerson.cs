using System;
namespace CrossfitApp
{
	public interface IPerson
	{
		string FirstName { get; }
		string LastName { get; }
		string FullName { get; }
	}
}
