using SFML_Engine.Engine.JUI;
using SFML.System;

namespace SFML_SpaceSEM.UI
{
	public class EditorSlider : JSlider
	{

		public JLabel LinkedLable { set; get; }

		public EditCenterElement EditorCenter { set; get; }

		public EditorSlider(JGUI gui) : base(gui)
		{
		}

		public override void Drag(object sender, Vector2i position)
		{
			base.Drag(sender, position);

			if (LinkedLable != null)
			{
				LinkedLable.setTextString("Time :"+((int)(600*SliderValue)).ToString());
			}

			if (EditorCenter != null)
			{
				EditorCenter.TimeOffset = (int) (600 * SliderValue);
			}
		}
	}
}
