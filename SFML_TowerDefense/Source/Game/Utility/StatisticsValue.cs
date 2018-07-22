namespace SFML_Roguelike.Source.Game.Utility
{
	/// <summary>
	/// This class was written for one of my old Projects, After Dead which was supervised by Philipp Kolhoff. 
	/// I reuse it here to provide a generic Statistics System.
	/// Original Source(PRIVATE REPOSITORY!) https://github.com/FrostByteGER/AfterDead/blob/master/Assets/AfterDead/Scripts/StatisticsValue.cs
	/// </summary>
	public class StatisticsValue
	{
		public string StatName { get; set; }

		public string StatId { get; set; }

		public string Description { get; set; }

		public float BaseValue { get; set; }

		public float CalculatedValue { get; set; }

		public void CalculateValue()
		{
			var value = BaseValue;
			CalculatedValue = value;
		}
	}
}