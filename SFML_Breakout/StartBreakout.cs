using System;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;

namespace SFML_Breakout
{
	public sealed class StartBreakout
	{
		public static bool MountainDewMode { get; set; } = true;

		public static void Main(string[] args)
		{
			if (args.Length >= 1)
			{
				MountainDewMode = bool.Parse(args[0]);
			}
			Engine engine = Engine.Instance;
			engine.GameInfo = new BreakoutGameInfo();
			engine.EngineWindowWidth = 800;
			engine.EngineWindowHeight = 600;
			engine.InitEngine();
			var physics = engine.PhysicsEngine;

			physics.AddGroup("Pads");
			physics.AddGroup("Balls");
			physics.AddGroup("Borders");

			physics.AddCollidablePartner("Balls", "Pads");
			physics.AddCollidablePartner("Balls", "Borders");
			physics.AddCollidablePartner("Pads", "Borders");

			var topBorder = new SpriteActor();
			var bottomBorder = new SpriteActor();
			var leftBorder = new SpriteActor();
			var rightBorder = new SpriteActor();

			topBorder.Movable = false;
			bottomBorder.Movable = false;
			leftBorder.Movable = false;
			rightBorder.Movable = false;

			topBorder.ActorName = "Top Border";
			bottomBorder.ActorName = "Bottom Border";
			leftBorder.ActorName = "Left Border";
			rightBorder.ActorName = "Right Border";

			topBorder.Position = new Vector2f(0, -400);
			bottomBorder.Position = new Vector2f(0, engine.EngineWindowHeight);
			leftBorder.Position = new Vector2f(-20, 0);
			rightBorder.Position = new Vector2f(engine.EngineWindowWidth, 0);

			topBorder.CollisionShape = new BoxShape(engine.EngineWindowWidth, 400);
			bottomBorder.CollisionShape = new BoxShape(engine.EngineWindowWidth, 400);
			leftBorder.CollisionShape = new BoxShape(20, engine.EngineWindowHeight);
			rightBorder.CollisionShape = new BoxShape(20, engine.EngineWindowHeight);

			topBorder.CollisionShape.ShowCollisionShape = true;
			bottomBorder.CollisionShape.ShowCollisionShape = true;
			leftBorder.CollisionShape.ShowCollisionShape = true;
			rightBorder.CollisionShape.ShowCollisionShape = true;

			physics.AddActorToGroup("Borders", topBorder);
			physics.AddActorToGroup("Borders", bottomBorder);
			physics.AddActorToGroup("Borders", leftBorder);
			physics.AddActorToGroup("Borders", rightBorder);

			var testlvl = new Level();
			engine.RegisterLevel(testlvl);
			testlvl.RegisterActor(topBorder);
			testlvl.RegisterActor(bottomBorder);
			testlvl.RegisterActor(leftBorder);
			testlvl.RegisterActor(rightBorder);

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}