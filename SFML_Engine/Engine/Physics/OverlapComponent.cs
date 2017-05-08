using System;
using BulletSharp;
using SFML.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public class OverlapComponent : ActorComponent
	{

		public GhostObject OverlapBody { get; set; }

		public Color ComponentColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));

		public OverlapComponent(GhostObject overlapBody)
		{
			OverlapBody = overlapBody ?? throw new ArgumentNullException(nameof(overlapBody));
		}
	}
}