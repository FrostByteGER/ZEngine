using System;
using SFML.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Graphics
{
	public class TextComponent : RenderComponent
	{
		private Text _renderText;


		public Text RenderText
		{
			get => _renderText;
			set
			{
				_renderText = value;
				FloatRect textRect = RenderText.GetLocalBounds();
				Origin = new SFML.System.Vector2f(textRect.Left + textRect.Width / 2.0f, textRect.Top + textRect.Height / 2.0f);
				ComponentBounds = Origin;
			}
		}

		public override TVector2f LocalPosition
		{
			get => RenderText.Position;
			set => base.LocalPosition = value;
		}

		public override float LocalRotation
		{
			get => RenderText.Rotation;
			set
			{
				base.LocalRotation = value;
				RenderText.Rotation = value;
			}
		}

		public override TVector2f LocalScale
		{
			get => RenderText.Scale;
			set
			{
				base.LocalScale = value;
				RenderText.Scale = value;
			}
		}

		public override TVector2f Origin
		{
			get => RenderText.Origin;
			set
			{
				base.Origin = value;
				RenderText.Origin = value;
			}
		}

		public TextComponent(Text renderText)
		{
			RenderText = renderText ?? throw new ArgumentNullException(nameof(renderText));
		}

		public TextComponent(string drawableText, Font textFont)
		{
			RenderText = new Text(drawableText, textFont);
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			RenderText.Draw(target, states);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			RenderText.Position = WorldPosition;
		}
	}
}