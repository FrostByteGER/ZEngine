using SFML.Graphics;
using SFML_Engine.Engine;

namespace SFML_SpaceSEM
{
	public class SpaceSEMPlayer : SpriteActor
	{
		public uint Score { get; set; } = 0;

		public SpaceSEMPlayer()
		{
		}

		public SpaceSEMPlayer(Texture texture) : base(texture)
		{
		}
	}
}