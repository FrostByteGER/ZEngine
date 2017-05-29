using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine.JUI
{
	class JChackboxGroup
	{
		public IList<JCheckbox> CheckBoxes { get; set; } = new List<JCheckbox>();

		public JCheckbox SelectedBox { get; private set; }

		public void AddBox(JCheckbox box)
		{
			if (!CheckBoxes.Contains(box))
			{
				CheckBoxes.Add(box);
				box.Group = this;
			}
		}

		public void Update(JCheckbox trigger)
		{
			foreach (JCheckbox box in CheckBoxes)
			{
				if (!object.ReferenceEquals(box, trigger))
				{
					box.IsSelected = false;
					box.Box.FillColor = box.CheckBoxColor;
				}
			}
		}
	}
}
