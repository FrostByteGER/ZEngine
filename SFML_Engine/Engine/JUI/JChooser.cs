using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine.JUI
{
	class JChooser : JLabel
	{

		public List<String> Choose { get; set; } = new List<string>();
		public int SelectedIndex = 0;

		public JChooser(JGUI gui) : base(gui)
		{
		}

		public void Next()
		{
			if (SelectedIndex + 1 > Choose.Count)
			{
				SelectedIndex = 0;
			}
			else
			{
				SelectedIndex++;
			}
			Text.DisplayedString = Choose[SelectedIndex];
		}

		public void Back()
		{
			if (SelectedIndex - 1 < Choose.Count)
			{
				SelectedIndex = Choose.Count-1;
			}
			else
			{
				SelectedIndex--;
			}
			Text.DisplayedString = Choose[SelectedIndex];
		}
	}
}
