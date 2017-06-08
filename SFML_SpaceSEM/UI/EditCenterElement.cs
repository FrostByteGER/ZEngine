using SFML_Engine.Engine.JUI;
using SFML_SpaceSEM.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SFML_SpaceSEM.UI
{
	public class EditCenterElement : JElement
	{

		private SpaceLevelDataWrapper SpawnData { get; set; }

		// Spawner with Time
		private List<SpaceLevelSpawnerDataWrapper> Spawners;

		private SpaceLevelSpawnerDataWrapper SelectedSpawner;

		//public float Vector2f

		public EditCenterElement(JGUI gui) : base(gui)
		{

		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);



		}
	}
}
