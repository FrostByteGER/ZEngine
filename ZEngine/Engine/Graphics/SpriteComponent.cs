using System;
using SFML.Graphics;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Graphics
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
				Origin = new Vector2(Sprite.GetGlobalBounds().Width / 2.0f, Sprite.GetGlobalBounds().Height / 2.0f);
				ComponentBounds = Origin;
			}
		}

		public override Vector2 LocalPosition
		{
			get => Sprite.Position;
			set => base.LocalPosition = value;
		}

		public override float LocalRotation
		{
			get => Sprite.Rotation;
			set
			{
				base.LocalRotation = value;
				Sprite.Rotation = value;
			}
		}

		public override Vector2 LocalScale
		{
			get => Sprite.Scale;
			set
			{
				base.LocalScale = value;
				Sprite.Scale = value;
			}
		}

		public override Vector2 Origin
		{
			get => Sprite.Origin;
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
			states.Shader = ComponentMaterial.MaterialShader;
			target.Draw(Sprite,states);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			Sprite.Position = WorldPosition;
		}

		public override void Destroy(bool disposing)
		{
			base.Destroy(disposing);
			Sprite?.Dispose();
			//Sprite?.Texture?.Dispose();
		}
	}
}