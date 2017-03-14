using System;
using System.Collections.Generic;

namespace CrossfitApp
{
	public class PersonalRecord : IPersonalRecord
	{
		public PersonalRecord()
		{
		}

		public DateTime Date
		{
			get;
			set;
		}

		public IList<string> ExerciseType
		{
			get;
			set;
		}

		public string Image
		{
			get;
			set;
		}

		public int MaximumReps
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Note
		{
			get;
			set;
		}

		public int Weight
		{
			get;
			set;
		}
	}
}
