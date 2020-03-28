using System;
using SFML.Graphics;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Graphics
{
	public class BarComponent : RenderComponent
	{
		private RectangleShape _bar;


		public RectangleShape Bar
		{
			get => _bar;
			set
			{
				_bar = value;
				Origin = _bar.Size / 2.0f;
				ComponentBounds = _bar.Size;
			}
		}

		public override Vector2 LocalPosition
		{
			get => _bar.Position;
			set => base.LocalPosition = value;
		}

		public override float LocalRotation
		{
			get => _bar.Rotation;
			set
			{
				base.LocalRotation = value;
				_bar.Rotation = value;
			}
		}

		public override Vector2 LocalScale
		{
			get => _bar.Scale;
			set
			{
				base.LocalScale = value;
				_bar.Scale = value;
			}
		}

		public override Vector2 Origin
		{
			get => _bar.Origin;
			set
			{
				base.Origin = value;
				_bar.Origin = value;
			}
		}

		public BarComponent(RectangleShape bar, Color barColor)
		{
			Bar = bar ?? throw new ArgumentNullException(nameof(bar));
			Bar.FillColor = barColor;
		}

		public BarComponent(Vector2 size, Color barColor)
		{
			Bar = new RectangleShape(size);
			Bar.FillColor = barColor;
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			Bar.Draw(target, states);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			Bar.Position = WorldPosition;
		}
	}
}