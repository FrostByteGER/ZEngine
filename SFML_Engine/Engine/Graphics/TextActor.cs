using SFML.Graphics;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Graphics
{
	public class TextActor : Actor
	{

		public TextComponent TextComp { get; set; }
		public TextActor(string toDisplay, Font textFont, Level level) : base(level)
		{
			TextComp = new TextComponent(toDisplay, textFont);
			SetRootComponent(TextComp);
		}
	}
}