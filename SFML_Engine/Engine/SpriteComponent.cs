using System;
using SFML.Graphics;

namespace SFML_Engine.Engine
{
	public class SpriteComponent : ActorComponent
	{

		public Sprite Sprite { get; set; }

		public SpriteComponent(Sprite sprite)
		{
			Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			target.Draw(Sprite);
		}

		public override void Destroy(bool disposing)
		{
			base.Destroy(disposing);
			Sprite.Dispose();
		}
	}
}