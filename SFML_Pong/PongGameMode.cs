using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.System;
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
		public PongBall Ball { get; set; }
		public bool AIEnabled { get; set; } = false;

		public PongPlayerController Winner { get; set; }

		public bool GameRunning { get; set; } = false;
		public bool GameEnded { get; set; } = false;

		public float PowerUPSpawnTimer = 0.0f;
		public float PowerUPSpawnFrequency = 5f;

		public Music BGM_Main;
		public List<string> MusicTracks = new List<string> { "Assets/SFML_Pong/BGM_Main_1.wav", "Assets/SFML_Pong/BGM_Main_2.wav", "Assets/SFML_Pong/BGM_Main_3.wav", "Assets/SFML_Pong/BGM_Main_4.wav", "Assets/SFML_Pong/BGM_Main_5.wav" };

		public override void OnGameStart()
		{
			base.OnGameStart();
			Player1 = LevelReference.EngineReference.Players[1] as PongPlayerController;
			var aiIndex = AIEnabled ? 3 : 2;
			Player2 = LevelReference.EngineReference.Players[aiIndex] as PongPlayerController;
			Player1.Score = 0;
			Player2.Score = 0;
			Player1.Position = new Vector2f();
			Player2.Position = new Vector2f();
			Winner = null;
			Ball = (PongBall)LevelReference.FindActorInLevel("Ball");
			BGM_Main = new Music(MusicTracks[EngineMath.EngineRandom.Next(0, MusicTracks.Count)]);
			BGM_Main.Loop = true;
			BGM_Main.Volume = Engine.Instance.GlobalVolume;
			BGM_Main.Play();
			SpawnBall();
			GameRunning = true;
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
			BGM_Main?.Stop();
			
			GameRunning = false;
			Console.WriteLine("GameMode ending");
		}

		public void OnPlayerScore(PongPlayerController scoringPlayer, int scoreToAdd)
		{
			if (!GameRunning)
			{
				return;
			}
			if (scoringPlayer.Score < WinScore)
			{
				scoringPlayer.Score += scoreToAdd;
				Console.WriteLine("New Score: " + Player1.Score + " | " + Player2.Score);
			}
			if(Player1.Score == WinScore ^ Player2.Score == WinScore)
			{
				if (Player1.Score == WinScore)
				{
					Winner = Player1;
					Console.WriteLine("Player 1 Wins!");
				}
				else if (Player2.Score == WinScore)
				{
					Winner = Player2;
					Console.WriteLine("Player 2 Wins!");
				}
				Ball.Position = new Vector2f(LevelReference.EngineReference.EngineWindowWidth / 2.0f, LevelReference.EngineReference.EngineWindowHeight / 2.0f);
				Ball.Velocity = new Vector2f();
				Ball.Acceleration = new Vector2f();
				Ball.MaxVelocity = 500;
				Ball.ScaleAbsolute(1.0f, 1.0f);
				GameRunning = false;
				GameEnded = true;
				return;
			}
			SpawnBall();
		}

		public void SpawnBall()
		{
			Ball.Position = new Vector2f(LevelReference.EngineReference.EngineWindowWidth / 2.0f, LevelReference.EngineReference.EngineWindowHeight / 2.0f);

			Vector2f vec;

			if (EngineMath.EngineRandom.NextDouble() <= 0.5)
			{
				vec = new Vector2f(300f, (float)(EngineMath.EngineRandom.NextDouble()-0.5D)*300);
			}
			else
			{
				vec = new Vector2f(-300f, (float)(EngineMath.EngineRandom.NextDouble() - 0.5D)*300); ;
			}

			Ball.Velocity = vec;
		}

		public void RestartGame()
		{
			Console.WriteLine("RESTARTING GAME!");
			Player1.Score = 0;
			Player2.Score = 0;
			GameEnded = false;
			GameRunning = true;
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (GameRunning)
			{
				if (PowerUPSpawnTimer >= PowerUPSpawnFrequency)
				{
					PowerUp powerUp = new PowerUp();
					powerUp.ActorName = "PowerUp";

					SphereShape ss = new SphereShape(10f);

					ss.ShowCollisionShape = true;
					//ss.SphereDiameter = 10f;

					powerUp.CollisionShape = ss;

					powerUp.Position = new SFML.System.Vector2f((float)(200+(LevelReference.EngineReference.EngineWindowWidth - 400) * EngineMath.EngineRandom.NextDouble()), (float)(60 + (LevelReference.EngineReference.EngineWindowWidth - 120) * EngineMath.EngineRandom.NextDouble()));

					Engine.Instance.RegisterEvent(new SpawnActorEvent<SpawnActorEventParams>(new SpawnActorEventParams(this, powerUp, LevelReference.LevelID)));

					// TODO add to PhysicsEngine
					LevelReference.EngineReference.PhysicsEngine.AddActorToGroup("PowerUP", powerUp);

					PowerUPSpawnTimer = 0f;
				}
				PowerUPSpawnTimer += deltaTime;
			}
		}
	}
}