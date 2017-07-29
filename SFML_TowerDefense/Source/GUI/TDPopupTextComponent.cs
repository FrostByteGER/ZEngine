using SFML.Graphics;
using SFML_Engine.Engine.Graphics;

namespace SFML_TowerDefense.Source.GUI
{
	public class TDPopupTextComponent : TextComponent
	{
		public TDPopupTextComponent(Text renderText) : base(renderText)
		{
		}

		public TDPopupTextComponent(string drawableText, Font textFont) : base(drawableText, textFont)
		{
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}