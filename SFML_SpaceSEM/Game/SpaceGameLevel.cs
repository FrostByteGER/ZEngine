using System.Collections.Generic;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Dynamics;

namespace SFML_SpaceSEM.Game
{
	public class SpaceGameLevel : Level
	{


		public SpriteActor Player { get; set; } = null;

		public List<SpriteActor> Enemies { get; set; } = new List<SpriteActor>();

		public List<SpriteActor> Spawners { get; set; } = new List<SpriteActor>();

		protected override void InitLevel()
		{
			base.InitLevel();
			var leftBorder = new Actor(this);
			leftBorder.ActorName = "Left Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(leftBorder, true, new TVector2f(-450.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f, 400.0f), BodyType.Static);
			leftBorder.Visible = true;

			var rightBorder = new Actor(this);
			rightBorder.ActorName = "Right Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(rightBorder, true, new TVector2f(450.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f, 400.0f), BodyType.Static);
			rightBorder.Visible = true;

			var topBorder = new Actor(this);
			topBorder.ActorName = "Top Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(topBorder, true, new TVector2f(0.0f, -450.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(400.0f, 50.0f), BodyType.Static);
			topBorder.Visible = true;

			var bottomBorder = new Actor(this);
			bottomBorder.ActorName = "Bottom Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(bottomBorder, true, new TVector2f(0.0f, 450.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(400.0f, 50.0f), BodyType.Static);
			bottomBorder.Visible = true;

			RegisterActor(leftBorder);
			RegisterActor(rightBorder);
			RegisterActor(topBorder);
			RegisterActor(bottomBorder);
		}

		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
		}

		public override void OnLevelLoad()
		{
			base.OnLevelLoad();
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameResume()
		{
			base.OnGameResume();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}