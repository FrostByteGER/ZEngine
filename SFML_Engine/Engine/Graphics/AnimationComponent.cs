using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Graphics
{
	public class AnimationComponent : RenderComponent
	{
		public Dictionary<string, AnimationSprite> Animations { get; private set; } = new Dictionary<string, AnimationSprite>();

		private AnimationSprite _activeAnimSprite;
		public AnimationSprite ActiveAnimSprite
		{
			get => _activeAnimSprite;
			set
			{
				_activeAnimSprite = value;
				Origin = new TVector2f(ActiveAnimSprite.FrameWidth / 2.0f, ActiveAnimSprite.FrameHeight / 2.0f); // TODO: DOES NOT RESPECT SCALING
				ComponentBounds = Origin;
			}
		}

		public override TVector2f LocalPosition
		{
			get => ActiveAnimSprite.Position;
			set => base.LocalPosition = value;
		}

		public override float LocalRotation
		{
			get => ActiveAnimSprite.Rotation;
			set
			{
				base.LocalRotation = value;
				ActiveAnimSprite.Rotation = value;
			}
		}

		public override TVector2f LocalScale
		{
			get => ActiveAnimSprite.Scale;
			set
			{
				base.LocalScale = value;
				ActiveAnimSprite.Scale = value;
			}
		}

		public override TVector2f Origin
		{
			get => ActiveAnimSprite.Origin;
			set
			{
				base.Origin = value;
				ActiveAnimSprite.Origin = value;
			}
		}

		public AnimationComponent(Texture spriteSheet, int frameWidth, int frameHeight)
		{
			ActiveAnimSprite = new AnimationSprite(spriteSheet, frameWidth, frameHeight);
			ActiveAnimSprite.StartAnimation();
		}

		public AnimationComponent(AnimationSprite sprite)
		{
			ActiveAnimSprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
		}




		public override void Draw(RenderTarget target, RenderStates states)
		{
			states.Shader = ComponentMaterial.MaterialShader;
			target.Draw(ActiveAnimSprite, states);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (ActiveAnimSprite.State == AnimationState.Running)
			{
				ActiveAnimSprite.RemainingTime -= deltaTime;
				if (ActiveAnimSprite.RemainingTime <= 0.0)
				{
					ActiveAnimSprite.NextFrame();
					ActiveAnimSprite.RemainingTime = ActiveAnimSprite.FrameDuration;
				}
			}
			ActiveAnimSprite.Position = WorldPosition;
		}

		public AnimationSprite GetAnimation(string animationName)
		{
			AnimationSprite anim = null;
			Animations.TryGetValue(animationName, out anim);
			return anim;
		}

		public T GetAnimation<T>(string animationName) where T : AnimationSprite
		{
			AnimationSprite anim = null;
			Animations.TryGetValue(animationName, out anim);
			return anim as T;
		}

		public T GetActiveAnimation<T>() where T : AnimationSprite
		{
			return ActiveAnimSprite as T;
		}

		public bool SetActiveAnimation(string animationName)
		{
			return SetActiveAnimation(animationName, true);
		}

		public bool SetActiveAnimation(string animationName, bool autoPlay)
		{
			if (!ContainsAnimation(animationName)) return false;
			ActiveAnimSprite = Animations[animationName];
			ActiveAnimSprite.StartAnimation();
			return true;
		}

		public bool SetAnimation(string animationName, AnimationSprite sprite)
		{
			if (!ContainsAnimation(animationName)) return false;
			Animations[animationName] = sprite;
			return true;
		}

		public bool AddAnimation(string animationName, AnimationSprite sprite)
		{
			if (ContainsAnimation(animationName)) return false;
			Animations.Add(animationName, sprite);
			return true;
		}

		public bool RemoveAnimation(string animationName)
		{
			GetAnimation(animationName).StopAnimation();
			return Animations.Remove(animationName);
		}

		public bool ContainsAnimation(string animationName)
		{
			return Animations.ContainsKey(animationName);
		}

		public bool ContainsAnimation(AnimationSprite sprite)
		{
			return Animations.ContainsValue(sprite);
		}

		public override void Destroy(bool disposing)
		{
			base.Destroy(disposing);
			ActiveAnimSprite?.Dispose();
			//Sprite?.Texture?.Dispose();
		}
	}
}