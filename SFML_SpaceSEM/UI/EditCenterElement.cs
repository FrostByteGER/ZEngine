using SFML_Engine.Engine.JUI;
using SFML_SpaceSEM.IO;
using System;
using SFML.Graphics;
using SFML.System;
using SFML_SpaceSEM.Game;

namespace SFML_SpaceSEM.UI
{
	public class EditCenterElement : JElement
	{

		public SpaceLevelDataWrapper SpawnData { get; set; }

		public SpaceLevelSpawnerDataWrapper SelectedSpawner { get; set; }

		private RectangleShape SelectedRec = new RectangleShape();

		private RectangleShape SpawnerRec = new RectangleShape();

		private RectangleShape ShipRec = new RectangleShape();

		public SpaceEditorLevel Level; 

		public int TimeOffset = 0;

		public EditCenterElement(JGUI gui, SpaceEditorLevel level) : base(gui)
		{
			SelectedRec.Size = new Vector2f(5f,5f);
			SelectedRec.FillColor = Color.Yellow;

			SpawnerRec.Size = new Vector2f(5f,5f);
			SpawnerRec.FillColor = Color.Red;

			ShipRec.Size = new Vector2f(5f,5f);
			ShipRec.FillColor = Color.Blue;

			Box.FillColor = new Color(30,30,30);

			SelectedSpawner = level.SelectedSpawner;

			Level = level;

		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			if (SpawnData == null)
			{
				return;
			}
			
			getNearestSpawner();

			foreach (SpaceLevelSpawnerDataWrapper spawner in SpawnData.Spawners)
			{

				if (Size.Y/2f > spawner.ActivationTime - TimeOffset && -Size.Y/2f < spawner.ActivationTime - TimeOffset)
				{
					SpawnerRec.Position = Position + new Vector2f(-5, Size.Y/2f - (spawner.ActivationTime - TimeOffset));
					target.Draw(SpawnerRec);
					
					if (SelectedSpawner.Equals(spawner))
					{
						SelectedRec.Position = Position + new Vector2f(-5, Size.Y / 2f - (SelectedSpawner.ActivationTime - TimeOffset));
						target.Draw(SelectedRec);

						if (!SelectedSpawner.Equals(Level.SelectedSpawner))
						{
							Level.SelectedSpawner = SelectedSpawner;
						}

						foreach (SpaceLevelShipDataWrapper ship in spawner.Ships)
						{
							ShipRec.Position = Position + new Vector2f(ship.Position.X, Size.Y / 2f - ship.Position.Y);
							target.Draw(ShipRec);
						}
					}
				}
			}
		}

		private void getNearestSpawner()
		{
			if (SpawnData == null)
			{
				return;
			}

			float distance = 600f;

			foreach (SpaceLevelSpawnerDataWrapper spawner in SpawnData.Spawners)
			{
				if (Math.Abs(TimeOffset - spawner.ActivationTime) < distance)
				{
					SelectedSpawner = spawner;

					distance = Math.Abs(TimeOffset - spawner.ActivationTime);
				}
			}
		}
	}
}
