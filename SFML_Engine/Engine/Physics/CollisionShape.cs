using SFML.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
    public class CollisionShape : Transformable
    {
		public bool ShowCollisionShape { get; set; } = false;
	    public Color CollisionShapeColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));
    }
}