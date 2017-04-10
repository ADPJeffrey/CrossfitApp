using System;
using System.Collections.Generic;

namespace CrossfitApp
{
	public interface IPersonalRecord
	{
		int ID { get; set; }
		string Name { get; set; }
		DateTime Date { get; set; }
		int ExerciseTypeID { get; set; }
		int MaximumReps { get; set; }
		int Weight { get; set; }
		string Image { get; set; }
		string Note { get; set; }
	}
}
