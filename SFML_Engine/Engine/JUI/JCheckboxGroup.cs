using System.Collections.Generic;

namespace SFML_Engine.Engine.JUI
{
	public class JCheckboxGroup
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
					box.Deselect();
				}
			}
		}
	}
}
