using SFML_Engine.Engine.JUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_SpaceSEM.UI
{
	class OnOffCheckbox : JCheckbox
	{
		public OnOffCheckbox(JGUI gui) : base(gui)
		{
		}

		public override void OnSelect()
		{
			base.OnSelect();
			setTextString("ON");
		}

		public override void OnDeselect()
		{
			base.OnDeselect();
			setTextString("OFF");
		}
	}
}
