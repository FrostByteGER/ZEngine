using SFML.Graphics;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.Graphics
{
	public class TextActor : Actor
	{

		public TextComponent TextComp { get; set; }
		public TextActor(string toDisplay, Font textFont)
		{
			TextComp = new TextComponent(toDisplay, textFont);
			SetRootComponent(TextComp);
		}
	}
}