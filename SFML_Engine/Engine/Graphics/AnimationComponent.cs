using System;
using SFML.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Graphics
{
	public class AnimationComponent : RenderComponent
	{
		private AnimationSprite _animSprite;

		public AnimationSprite AnimSprite
		{
			get => _animSprite;
			set
			{
				_animSprite = value;
				Origin = new TVector2f(AnimSprite.GetGlobalBounds().Width / 2.0f, AnimSprite.GetGlobalBounds().Height / 2.0f);
				ComponentBounds = Origin;
			}
		}

		public override TVector2f LocalPosition
		{
			get => AnimSprite.Position;
			set => base.LocalPosition = value;
		}

		public override float LocalRotation
		{
			get => AnimSprite.Rotation;
			set
			{
				base.LocalRotation = value;
				AnimSprite.Rotation = value;
			}
		}

		public override TVector2f LocalScale
		{
			get => AnimSprite.Scale;
			set
			{
				base.LocalScale = value;
				AnimSprite.Scale = value;
			}
		}

		public override TVector2f Origin
		{
			get => AnimSprite.Origin;
			set
			{
				base.Origin = value;
				AnimSprite.Origin = value;
			}
		}

		public AnimationComponent(Texture spriteSheet, int frameWidth, int frameHeight)
		{
			AnimSprite = new AnimationSprite(spriteSheet, frameWidth, frameHeight);
		}

		public AnimationComponent(AnimationSprite sprite)
		{
			AnimSprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			states.Shader = ComponentMaterial.MaterialShader;
			target.Draw(AnimSprite, states);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (AnimSprite.State == AnimationState.Running)
			{
				AnimSprite.RemainingTime -= deltaTime;
				if (AnimSprite.RemainingTime <= 0.0)
				{
					AnimSprite.NextFrame();
					AnimSprite.RemainingTime = AnimSprite.FrameDuration;
				}
			}
			AnimSprite.Position = WorldPosition;
		}

		public override void Destroy(bool disposing)
		{
			base.Destroy(disposing);
			AnimSprite?.Dispose();
			//Sprite?.Texture?.Dispose();
		}
	}
}