using SFML_Engine.Engine.JUI;
using SFML_SpaceSEM.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace SFML_SpaceSEM.UI
{
	public class EditCenterElement : JElement
	{

		public SpaceLevelDataWrapper SpawnData { get; set; }

		// Spawner with Time
		private List<SpaceLevelSpawnerDataWrapper> Spawners;

		private SpaceLevelSpawnerDataWrapper SelectedSpawner;

		//public float Vector2f

		private RectangleShape Rec = new RectangleShape();

		public int TimeOffset = 0;

		public EditCenterElement(JGUI gui) : base(gui)
		{
			Rec.Size = new Vector2f(5f,5f);
			Rec.FillColor = Color.Red;
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			if (SpawnData == null)
			{
				return;
			}
			foreach (SpaceLevelSpawnerDataWrapper spawner in SpawnData.Spawners)
			{
				if (TimeOffset - Size.X < spawner.ActivationTime && TimeOffset + Size.X > spawner.ActivationTime)
				{

					Rec.Position = Position + new Vector2f(spawner.ActivationTime + TimeOffset,0);
					Console.WriteLine(spawner.ActivationTime+" "+ TimeOffset);
					target.Draw(Rec);
				}
			}
		}
	}
}
