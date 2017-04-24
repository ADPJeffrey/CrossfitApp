using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace CrossfitApp
{
	public class PersonalRecord : IPersonalRecord
	{
		private DateTime _date;

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public DateTime Date
		{
			get
			{
				if (_date == DateTime.MinValue)
					return DateTime.Now;

				return _date;
			}

			set { _date = value; }
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

		public TimeSpan Time
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
