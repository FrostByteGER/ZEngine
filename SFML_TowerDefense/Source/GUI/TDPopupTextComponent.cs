using SFML.Graphics;
using ZEngine.Engine.Graphics;
using ZEngine.Engine.Utility;

namespace Exofinity.Source.GUI
{
	public class TDPopupTextComponent : TextComponent
	{
		public TVector2f TargetPosition { get; set; } = new TVector2f(0,50);
		public float TargetThreshold { get; set; } = 5.0f;
		
		public TDPopupTextComponent(Text renderText) : base(renderText)
		{
		}

		public TDPopupTextComponent(string drawableText, Font textFont) : base(drawableText, textFont)
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