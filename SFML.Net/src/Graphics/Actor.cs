using System;
using SFML.Graphics;

namespace SFML
{
	public class Actor : Transformable, IAc
	{
		public Actor()
		{
		}

		public Actor(Transformable transformable) : base(transformable)
		{
		}

		protected Actor(IntPtr cPointer) : base(cPointer)
		{
		}
	}
}