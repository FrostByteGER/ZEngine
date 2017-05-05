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

		public UIActor(Transformable transformable) : base(transformable)
		{
		}

		protected UIActor(IntPtr cPointer) : base(cPointer)
		{
		}
	}
}