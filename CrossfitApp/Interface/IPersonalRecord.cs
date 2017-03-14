using System;
using System.Collections.Generic;

namespace CrossfitApp
{
	public interface IPersonalRecord
	{
		string Name { get; set; }
		DateTime Date { get; set; }
		IList<string> ExerciseType { get; set; }
		int MaximumReps { get; set; }
		int Weight { get; set; }
		string Image { get; set; }
		string Note { get; set; }
	}
}
