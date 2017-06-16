using System.Collections.Generic;
using SFML.Graphics;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Graphics
{
	public class AnimationSprite : Sprite
	{
		public Texture AnimationSheet { get; }
		public int FrameWidth { get; } = 0;
		public int FrameHeight { get; } = 0;
		public int FrameRowPosition { get; protected set; } = 0;
		public int FrameColumnPosition { get; protected set; } = 0;
		public float FrameDuration { get; set; } = 1.0f;
		public float RemainingTime { get; protected internal set; } = 1.0f;
		public bool AutoRepeat { get; set; } = true;
		public bool PlayBackwards { get; set; } = false;

		public AnimationSprite(Texture spriteSheet, int frameWidth, int frameHeight)
		{
			AnimationSheet = spriteSheet;
			FrameWidth = frameWidth;
			FrameHeight = frameHeight;
		}

		public void NextFrame()
		{
			if (PlayBackwards)
			{
				// If we are at the Texturesheets start, set the Positions to the end.
				if (FrameRowPosition * FrameWidth == 0 &&
					FrameColumnPosition * FrameHeight == 0)
				{
					// When we are at the start and don't want to repeat, stop the animation and return.
					if (!AutoRepeat)
					{
						StopAnimation();
						return;
					}
					FrameRowPosition = (int) (AnimationSheet.Size.X - FrameWidth);
					FrameColumnPosition = (int) (AnimationSheet.Size.X - FrameHeight);
				}
				else
				{
					// If we reached the row start, move a row up.
					if (FrameRowPosition * FrameWidth == 0)
					{
						FrameRowPosition = (int)(AnimationSheet.Size.X - FrameWidth);
						--FrameColumnPosition;
					}
					else
					{
						--FrameRowPosition;
					}
				}
			}
			else
			{
				// If we are at the Texturesheets end, set the Positions to the start.
				if (FrameRowPosition * FrameWidth == AnimationSheet.Size.X - FrameWidth &&
				    FrameColumnPosition * FrameHeight == AnimationSheet.Size.X - FrameHeight)
				{
					// When we are at the end and don't want to repeat, stop the animation and return.
					if (!AutoRepeat)
					{
						StopAnimation();
						return;
					}
					FrameRowPosition = 0;
					FrameColumnPosition = 0;
				}
				else
				{
					// If we reached the row end, move a row down.
					if (FrameRowPosition * FrameWidth == AnimationSheet.Size.X - FrameWidth)
					{
						FrameRowPosition = 0;
						++FrameColumnPosition;
					}
					else
					{
						++FrameRowPosition;
					}
				}
			}
			SetSpriteTexture();
		}

		private void SetSpriteTexture()
		{
			TextureRect = new IntRect(FrameRowPosition * FrameWidth, FrameColumnPosition * FrameHeight, FrameWidth, FrameHeight);
		}

		public AnimationState State { get; private set; } = AnimationState.None;

		public void StartAnimation()
		{
			State = AnimationState.Running;
		}

		public void PauseAnimation()
		{
			State = AnimationState.Paused;
		}

		public void ResumeAnimation()
		{
			State = AnimationState.Running;
		}

		public void TogglePauseAnimation()
		{
			switch (State)
			{
				case AnimationState.Running:
					PauseAnimation();
					break;
				case AnimationState.Paused:
					ResumeAnimation();
					break;
			}
		}

		public void StopAnimation()
		{
			State = AnimationState.Stopped;
		}

		public void RestartAnimation()
		{
			StopAnimation();
			StartAnimation();
		}
	}

	public enum AnimationState
	{
		None,
		Stopped,
		Running,
		Paused
	}
}