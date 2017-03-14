using System;
namespace CrossfitApp
{
	public class Person : IPerson
	{
		public Person(string firstName, string lastName)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string FullName { get { return FirstName + " " + LastName; } }


	}
}
