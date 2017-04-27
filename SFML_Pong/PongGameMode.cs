using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_Pong
{
	public class PongGameMode : GameMode
	{

		public PongPlayerController Player1 { get; set; }
		public PongPlayerController Player2 { get; set; }
		public uint WinScore { get; set; } = 3;

		public PongPlayerController Winner { get; set; }

		public bool GameEnded { get; set; } = false;
		public bool RestartGame { get; set; } = false;

		public float PowerUPSpawnTimer = 0.0f;
		public float PowerUPSpawnFrequency = 1f;

		public Music BGM_Main;
		public List<string> MusicTracks = new List<string> { "Assets/SFML_Pong/BGM_Main_1.wav", "Assets/SFML_Pong/BGM_Main_2.wav", "Assets/SFML_Pong/BGM_Main_3.wav", "Assets/SFML_Pong/BGM_Main_4.wav", "Assets/SFML_Pong/BGM_Main_5.wav" };

		public override void OnGameStart()
		{
			base.OnGameStart();
			Player1 = LevelReference.Engine.Players[0] as PongPlayerController;
			Player2 = LevelReference.Engine.Players[1] as PongPlayerController;
			BGM_Main = new Music(MusicTracks[EngineMath.EngineRandom.Next(0, MusicTracks.Count)]);
			BGM_Main.Loop = true;
			BGM_Main.Volume = 5;
			BGM_Main.Play();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
			BGM_Main.Stop();
			Console.WriteLine("GameMode ending");
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (Player1.Score == WinScore ^ Player2.Score == WinScore)
			{
				if (Player1.Score == WinScore)
				{
					Winner = Player1;
				}
				else if (Player2.Score == WinScore)
				{
					Winner = Player2;
				}
				if (!GameEnded)
				{
					Console.WriteLine("GAME OVER!");
					GameEnded = true;
				}
				if (RestartGame)
				{
					Console.WriteLine("RESTARTING GAME!");
					Player1.Score = 0;
					Player2.Score = 0;
					GameEnded = false;
					RestartGame = false;

				}

			}
			else
			{
				if (PowerUPSpawnTimer >= PowerUPSpawnFrequency)
				{
					PowerUp powerUp = new PowerUp();
					powerUp.ActorName = "PowerUp";

					SphereShape ss = new SphereShape(10f);

					ss.ShowCollisionShape = true;
					//ss.SphereDiameter = 10f;

					powerUp.CollisionShape = ss;

					powerUp.Position = new SFML.System.Vector2f((float)(60 + (LevelReference.Engine.EngineWindowWidth - 120) * EngineMath.EngineRandom.NextDouble()), (float)(60 + (LevelReference.Engine.EngineWindowWidth - 120) * EngineMath.EngineRandom.NextDouble()));

					Engine.Instance.RegisterEvent(new SpawnActorEvent<SpawnActorEventParams>(new SpawnActorEventParams(this, powerUp, LevelReference.LevelID)));

					// TODO add to PhysicsEngine
					LevelReference.Engine.PhysicsEngine.AddActorToGroup("PowerUP", powerUp);

					PowerUPSpawnTimer = 0f;
				}
				PowerUPSpawnTimer += deltaTime;
			}

			
		}
	}
}