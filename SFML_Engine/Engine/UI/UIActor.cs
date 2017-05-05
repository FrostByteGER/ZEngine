using System;
using SFML.Graphics;

namespace SFML_Engine.Engine.UI
{
	public class UIActor : Actor
	{

		public uint LayerID { get; set; } = 0;

		public UIActor()
		{
		}

		protected UIActor(IntPtr cPointer) : base(cPointer)
		{
		}
	}
}