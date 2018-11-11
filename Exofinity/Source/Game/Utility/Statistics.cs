using System.Collections.Generic;

namespace Exofinity.Source.Game.Utility
{
	/// <summary>
	/// This class was written for one of my old Projects, After Dead which was supervised by Philipp Kolhoff. 
	/// I reuse it here to provide a generic Statistics System.
	/// Original Source(PRIVATE REPOSITORY!) https://github.com/FrostByteGER/AfterDead/blob/master/Assets/AfterDead/Scripts/Statistics.cs
	/// </summary>
	public class Statistics
	{
		public List<StatisticsValue> Stats { get; set; } = new List<StatisticsValue>();


		/// <summary>
		/// Calculates the CalculatedValue from BaseValue for all statisticsvalues.
		/// </summary>
		public void CalculateStatisticsValues()
		{
			foreach (var stat in Stats)
			{
				stat.CalculateValue();
			}
		}

		/// <summary>
		/// Searches the statistics array with the given string through. Returns the first found StatisticsValue object or returns null.
		/// </summary>
		/// <param id="id"></param>
		/// <returns>The first found object or null if none was found.</returns>
		public StatisticsValue FindStatByID(string id)
		{
			foreach (var stat in Stats)
			{
				if (stat.StatId == id)
				{
					return stat;
				}
			}
			return null;
		}

		public StatisticsValue FindStatByName(string name)
		{
			foreach (var stat in Stats)
			{
				if (stat.StatName == name)
				{
					return stat;
				}
			}
			return null;
		}
	}


}