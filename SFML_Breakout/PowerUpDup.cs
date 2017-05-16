using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Breakout
{
	class PowerUpDup : PowerUp
	{
		public override void Apply()
		{

			base.Apply();

			BreakoutBall mainBall = (BreakoutBall)LevelReference.FindActorInLevel("Ball");

			BreakoutBall newBall = new BreakoutBall();

			newBall.ActorName = "Ball2";
			newBall.CollisionShape = new SphereShape(30.0f);
			newBall.Position = mainBall.Position;
			newBall.CollisionShape.ShowCollisionShape = true;
			newBall.Velocity = mainBall.Velocity + new Vector2f(10f,-10f);
			LevelReference.EngineReference.PhysicsEngine.AddActorToGroup("Balls", newBall);

			LevelReference.EngineReference.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, newBall, LevelReference.LevelID)));
			
		}
	}
}
