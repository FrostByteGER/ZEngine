using SFML.System;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Physics;

namespace SFML_Breakout
{
	class PowerUpDup : PowerUp
	{

		public PowerUpDup()
		{
			CollisionShape.CollisionShapeColor = new SFML.Graphics.Color(255, 0, 255);
		}

		public PowerUpDup(PowerUpDup p)
		{
			Velocity = p.Velocity;
			CollisionShape = new SphereShape(p.CollisionShape.CollisionBounds.X);
			CollisionShape.CollisionShapeColor = p.CollisionShape.CollisionShapeColor;
		}

		public override void Apply()
		{

			base.Apply();

			BreakoutBall mainBall = (BreakoutBall)LevelReference.FindActorInLevel("Ball");

			BreakoutBall newBall = new BreakoutBall();

			newBall.ActorName = "Ball";
			newBall.CollisionShape = new SphereShape(mainBall.CollisionShape.CollisionBounds.X);
			newBall.Position = mainBall.Position;
			newBall.CollisionShape.ShowCollisionShape = true;
			newBall.Velocity = mainBall.Velocity + new Vector2f(10f,-10f);
			LevelReference.EngineReference.PhysicsEngine.AddActorToGroup("Balls", newBall);
			((BreakoutGameMode) LevelReference.GameMode).Balls.Add(newBall);

			LevelReference.EngineReference.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, newBall, LevelReference.LevelID)));
			
		}

		public override object Clone()
		{
			return new PowerUpDup(this);
		}

	}
}
