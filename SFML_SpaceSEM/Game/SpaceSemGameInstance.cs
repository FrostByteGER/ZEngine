using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML_Engine.Engine.Core;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;

namespace SFML_SpaceSEM.Game
{
	public class SpaceSEMGameInstance : GameInstance
	{
		public Font MainGameFont { get; set; }

		public uint HighScore { get; set; } = 0;
		public uint AlltimeHighScore { get; set; } = 0;
		public uint SecretThreshold { get; } = 9999;
		public uint CurrentLevel { get; set; } = 1;
		public uint MaxLevels { get; set; } = 1;

		public static List<string> MusicTracks = new List<string> { "Assets/SFML_SpaceSEM/BGM_Main_1.wav", "Assets/SFML_SpaceSEM/BGM_Main_2.wav", "Assets/SFML_SpaceSEM/BGM_Main_3.wav", "Assets/SFML_SpaceSEM/BGM_Main_4.wav", "Assets/SFML_SpaceSEM/BGM_Main_5.wav" };

		public static string BGM_MLG_Main = "Assets/SFML_SpaceSEM/BGM_MLG_Main.wav";
		public static string BGM_MLG_Final = "Assets/SFML_SpaceSEM/BGM_MLG_Final.wav";
		public static string BGM_MLG_Secret = "Assets/SFML_SpaceSEM/BGM_MLG_Secret.wav";
		public static string BGM_MLG_Current = BGM_MLG_Main;

		public static Texture EnemyTexture { get; set; }

		public static Music BGM_Main;

		public static SoundBuffer PowerUpBuffer { get; set; }
		public static SoundBuffer BlockHitBuffer { get; set; }
		public SpaceSEMGameInstance()
		{	
			MainGameFont = new SFML.Graphics.Font("Assets/SFML_SpaceSEM/arial.ttf");

			PowerUpBuffer = new SoundBuffer("Assets/SFML_SpaceSEM/SFX_PowerUp.wav");
			Console.WriteLine(PowerUpBuffer.Duration.AsMilliseconds());
			BlockHitBuffer = new SoundBuffer("Assets/SFML_SpaceSEM/SFX_BlockHit.wav");
			Console.WriteLine(BlockHitBuffer.Duration.AsMilliseconds());

			BGM_Main = new Music(StartSpaceSEM.MountainDewMode ? BGM_MLG_Current : MusicTracks[EngineMath.EngineRandom.Next(0, MusicTracks.Count)]);
			BGM_Main.Loop = true;
			BGM_Main.Volume = Engine.Instance.GlobalVolume;

			EnemyTexture = new Texture("Assets/SFML_SpaceSEM/Enemy_01.png");
		}

		public static void SwitchMusic()
		{
			BGM_Main.Stop();
			BGM_Main = new Music(StartSpaceSEM.MountainDewMode ? BGM_MLG_Current : MusicTracks[EngineMath.EngineRandom.Next(0, MusicTracks.Count)]);
			BGM_Main.Loop = true;
			BGM_Main.Volume = Engine.Instance.GlobalVolume;
			BGM_Main.Play();
		}
	}
}