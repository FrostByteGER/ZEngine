using System;
using System.Threading;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_Pong
{
	public class PongGameMode : GameMode
	{

		public PongPlayerController Player1 { get; set; }
		public PongPlayerController Player2 { get; set; }
		public uint WinScore { get; set; } = 3;

		public float PowerUPSpawnTimer = 0.0f;
		public float PowerUPSpawnFrequency = 20f;

		public override void OnGameStart()
		{
			base.OnGameStart();
			Player1 = LevelReference.Engine.Players[0] as PongPlayerController;
			Player2 = LevelReference.Engine.Players[1] as PongPlayerController;
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
			Console.WriteLine("GameMode ending");
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (Player1.Score == WinScore ^ Player2.Score == WinScore)
			{
				Console.WriteLine("GAME OVER!");
				//Thread.Sleep(3000);
				Console.WriteLine("RESTARTING GAME!");
				Player1.Score = 0;
				Player2.Score = 0;
			}

			if (PowerUPSpawnTimer >= PowerUPSpawnFrequency)
			{
				PowerUP powerUP = new PowerUP();
				powerUP.ActorName = "PowerUP";

				SphereShape ss = new SphereShape(10f);

				ss.ShowCollisionShape = true;
				//ss.SphereDiameter = 10f;

				powerUP.CollisionShape = ss;

				powerUP.Position = new SFML.System.Vector2f((float) (60 + (LevelReference.Engine.EngineWindowWidth - 120) * EngineMath.EngineRandom.NextDouble()), (float) (60 + (LevelReference.Engine.EngineWindowWidth - 120) * EngineMath.EngineRandom.NextDouble()));

				LevelReference.Actors.Add(powerUP);

				// TODO add to PhysicsEngine
				LevelReference.Engine.PhysicsEngine.AddActorToGroup("PowerUP", powerUP);

				PowerUPSpawnTimer = 0f;
			}
			PowerUPSpawnTimer += deltaTime;
		}
	}
}