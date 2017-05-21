using System;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Game
{
	public class SpriteComponent : RenderComponent
	{
		private Sprite _sprite;

		public Sprite Sprite
		{
			get => _sprite;
			set
			{
				_sprite = value;
				Origin = new TVector2f(Sprite.GetGlobalBounds().Width / 2.0f, Sprite.GetGlobalBounds().Height / 2.0f);
			}
		}

		public override TVector2f Position
		{
			get => base.Position;
			set
			{
				base.Position = value;
				Sprite.Position = value;
			}
		}

		public override float Rotation
		{
			get => base.Rotation;
			set
			{
				base.Rotation = value;
				Sprite.Rotation = value;
			}
		}

		public override TVector2f Scale
		{
			get => base.Scale;
			set
			{
				base.Scale = value;
				Sprite.Scale = value;
			}
		}

		public override TVector2f Origin
		{
			get => base.Origin;
			set
			{
				base.Origin = value;
				Sprite.Origin = value;
			}
		}

		public SpriteComponent(Sprite sprite)
		{
			Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			target.Draw(Sprite,states);
		}

		public override void Destroy(bool disposing)
		{
			base.Destroy(disposing);
			Sprite.Dispose();
		}
	}
}