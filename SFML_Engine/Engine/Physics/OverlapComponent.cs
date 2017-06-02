using SFML.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public class OverlapComponent : PhysicsComponent
	{

		public override Color ComponentColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));

		public OverlapComponent()
		{
			CanOverlap = true;
		}
	}
}