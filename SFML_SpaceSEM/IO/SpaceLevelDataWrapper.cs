using System;
using System.Collections.Generic;
using SFML_Engine.Engine.Utility;

namespace SFML_SpaceSEM.IO
{

	public struct SpaceLevelShipDataWrapper
	{
		public Type ShipType { get; set; }
		public uint Healthpoints { get; set; }
		public uint Score { get; set; }
		public TVector2f Position { get; set; }

		public TVector2f Velocity { get; set; }

		public float BulletSpread { get; set; }

		public uint BulletsPerShot { get; set; }

		public float BulletSpeed { get; set; }

		public float CooldownTime { get; set; }
		public uint BulletDamage { get; set; }
	}

	public struct SpaceLevelSpawnerDataWrapper
	{
		public float ActivationTime { get; set; }
		public List<SpaceLevelShipDataWrapper> Ships { get; set; }
	}

	/// <summary>
	/// A simple wrapper that loads and saves SpaceGameLevel information like spawners. This is not the generic JSON solution
	/// the SFML_Engine will provide in the future with LevelLoad(string json_file).
	/// </summary>
	public class SpaceLevelDataWrapper
	{
		public uint Highscore { get; set; } = 0;
		public bool LevelBeat { get; set; } = false;
		public float BeatTime { get; set; } = 0.0f;

		public List<SpaceLevelSpawnerDataWrapper> Spawners { get; set; } = new List<SpaceLevelSpawnerDataWrapper>();
	}

}