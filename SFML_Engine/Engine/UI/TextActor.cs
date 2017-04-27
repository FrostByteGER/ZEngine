using SFML.Graphics;
using Text = SFML_Engine.Engine.SFML.Graphics.Text;

namespace SFML_Engine.Engine.UI
{
	public class TextActor : Text
	{
		public TextActor()
		{
		}

		public TextActor(string str, Font font) : base(str, font)
		{
		}

		public TextActor(string str, Font font, uint characterSize) : base(str, font, characterSize)
		{
		}

		public TextActor(Text copy) : base(copy)
		{
		}
	}
}