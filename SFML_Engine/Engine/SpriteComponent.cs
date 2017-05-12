using System;
using SFML.Graphics;
using Sprite = SFML_Engine.Engine.SFML.Graphics.Sprite;

namespace SFML_Engine.Engine
{
	public class SpriteComponent : ActorComponent
	{

		public Sprite Sprite { get; set; }

		public SpriteComponent(Sprite sprite)
		{
			if (sprite == null) throw new ArgumentNullException(nameof(sprite));
			Sprite = sprite;
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			target.Draw(Sprite);
		}
	}
}