using SFML.Graphics;

namespace SFML_Engine.Engine.Graphics
{
	public class AnimationSprite : Sprite
	{
		public Texture AnimationSheet
		{
			get => Texture;
			private set => Texture = value;
		}

		public int FrameWidth { get; } = 0;
		public int FrameHeight { get; } = 0;
		public int FrameRowPosition { get; protected set; } = 0;
		public int FrameColumnPosition { get; protected set; } = 0;
		public float FrameDuration { get; set; } = 0.05f;
		public float RemainingTime { get; protected internal set; } = 1.0f;
		public bool AutoRepeat { get; set; } = true;
		public bool PlayBackwards { get; set; } = false;

		public delegate void Start();
		public delegate void Pause();
		public delegate void Resume();
		public delegate void Finish();
		public delegate void Stop();

		public event Start Started;
		public event Pause Paused;
		public event Resume Resumed;
		public event Finish Finished;
		public event Stop Stopped;

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
					OnFinished();
					// When we are at the start and don't want to repeat, stop the animation and return.
					if (!AutoRepeat)
					{
						StopAnimation();
						return;
					}
					FrameRowPosition = (int) (AnimationSheet.Size.X / FrameWidth) - 1;
					FrameColumnPosition = (int) (AnimationSheet.Size.X / FrameHeight) - 1;
				}
				else
				{
					// If we reached the row start, move a row up.
					if (FrameRowPosition * FrameWidth == 0)
					{
						FrameRowPosition = (int)(AnimationSheet.Size.X / FrameWidth) - 1;
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
					OnFinished();
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
			SetSpriteTexture();
			OnStarted();
		}

		public void PauseAnimation()
		{
			State = AnimationState.Paused;
			OnPaused();
		}

		public void ResumeAnimation()
		{
			State = AnimationState.Running;
			OnResumed();
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
			OnStopped();
		}

		public void RestartAnimation()
		{
			StopAnimation();
			StartAnimation();
		}

		protected virtual void OnStarted()
		{
			Started?.Invoke();
		}
		protected virtual void OnPaused()
		{
			Paused?.Invoke();
		}
		protected virtual void OnResumed()
		{
			Resumed?.Invoke();
		}
		protected virtual void OnFinished()
		{
			Finished?.Invoke();
		}
		protected virtual void OnStopped()
		{
			Stopped?.Invoke();
		}
	}

	public enum AnimationState
	{
		None,
		Stopped,
		Running,
		Paused,
		Finished
	}
}