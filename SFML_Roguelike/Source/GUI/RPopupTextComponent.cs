using System;
using SFML.Graphics;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_Roguelike.Source.GUI
{
	public class RPopupTextComponent : TextComponent
	{
		public TVector2f TargetPosition { get; set; } = new TVector2f(0,50);
		public float TargetThreshold { get; set; } = 5.0f;
		
		public RPopupTextComponent(Text renderText) : base(renderText)
		{
		}

		public RPopupTextComponent(string drawableText, Font textFont) : base(drawableText, textFont)
		{
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if ((LocalPosition - TargetPosition).LengthSquared > TargetThreshold * TargetThreshold)
			{
				LocalPosition = EngineMath.VInterpTo(LocalPosition, TargetPosition, deltaTime, 3.0f);
			}
			else
			{
				ParentActor.RemoveComponent(this);
			}

		}
	}
}