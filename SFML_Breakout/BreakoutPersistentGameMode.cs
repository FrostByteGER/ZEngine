﻿using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;

namespace SFML_Breakout
{
	public class BreakoutPersistentGameMode : PersistentGameMode
	{
		public uint HighScore { get; set; } = 0;
		public uint AlltimeHighScore { get; set; } = 0;
		public uint CurrentLevel { get; set; } = 1;
		public uint MaxLevels { get; set; } = 1;

		public static List<string> MusicTracks = new List<string> { "Assets/SFML_Breakout/BGM_Main_1.wav", "Assets/SFML_Breakout/BGM_Main_2.wav", "Assets/SFML_Breakout/BGM_Main_3.wav", "Assets/SFML_Breakout/BGM_Main_4.wav", "Assets/SFML_Breakout/BGM_Main_5.wav" };

		public static Music BGM_Main;

		public static SoundBuffer PowerUpBuffer { get; set; }
		public static SoundBuffer BlockHitBuffer { get; set; }
		public BreakoutPersistentGameMode()
		{
			PowerUpBuffer = new SoundBuffer("Assets/SFML_Breakout/SFX_PowerUp.wav");
			Console.WriteLine(PowerUpBuffer.Duration.AsMilliseconds());
			BlockHitBuffer = new SoundBuffer("Assets/SFML_Breakout/SFX_BlockHit.wav");
			Console.WriteLine(BlockHitBuffer.Duration.AsMilliseconds());

			BGM_Main = new Music(StartBreakout.MountainDewMode ? "Assets/SFML_Breakout/BGM_Main_MLG.wav" : MusicTracks[EngineMath.EngineRandom.Next(0, MusicTracks.Count)]);
			BGM_Main.Loop = true;
			BGM_Main.Volume = Engine.Instance.GlobalVolume;
			BGM_Main.Play();
		}

		public static void SwitchMusic()
		{
			BGM_Main.Stop();
			BGM_Main = new Music(StartBreakout.MountainDewMode ? "Assets/SFML_Breakout/BGM_Main_MLG.wav" : MusicTracks[EngineMath.EngineRandom.Next(0, MusicTracks.Count)]);
			BGM_Main.Loop = true;
			BGM_Main.Volume = Engine.Instance.GlobalVolume;
			BGM_Main.Play();
		}
	}
}