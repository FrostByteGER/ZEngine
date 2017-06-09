using System;
using System.Collections.Generic;

namespace SFML_Engine.Engine.JUI
{
	public class JChooser : JLabel
	{

		public List<String> Choose { get; set; } = new List<string>();
		public int SelectedIndex = 0;

		public JChooser(JGUI gui) : base(gui)
		{
		}

		public void Next()
		{
			if (SelectedIndex + 1 >= Choose.Count)
			{
				SelectedIndex = 0;
			}
			else
			{
				SelectedIndex++;
			}
			Text.DisplayedString = Choose[SelectedIndex];
			ReSize();
		}

		public void Back()
		{
			if (SelectedIndex - 1 < 0)
			{
				SelectedIndex = Choose.Count-1;
			}
			else
			{
				SelectedIndex--;
			}
			Text.DisplayedString = Choose[SelectedIndex];
			ReSize();
		}
	}
}
