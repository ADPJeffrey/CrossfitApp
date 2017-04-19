using System;
using System.Collections.Generic;
using SQLite;

namespace CrossfitApp
{
	public class PersonalRecord : IPersonalRecord
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public DateTime Date
		{
			get;
			set;
		}

		public int ExerciseTypeID
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

		public int Reps
		{
			get;
			set;
		}

		public int Meters
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
