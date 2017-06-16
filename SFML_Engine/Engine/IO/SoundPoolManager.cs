using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SFML.Audio;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.IO
{
	public class SoundPoolManager
	{
		public static string SFXPath { get; } = AssetManager.GameAssetsPath + "SFX/";

		private readonly Dictionary<string, SoundBuffer> _soundBufferPool = new Dictionary<string, SoundBuffer>();
		public ReadOnlyDictionary<string, SoundBuffer> SoundBufferPool => new ReadOnlyDictionary<string, SoundBuffer>(_soundBufferPool);

		public uint PoolSize { get; set; } = 64;
		public uint PoolSizeAutoIncrementSize { get; set; } = 32;

		/// <summary>
		/// Maximum Soundfilelimit is 255 due to OS reasons.
		/// </summary>
		public uint PoolSizeHardLimit { get; } = byte.MaxValue;

		public SoundPoolManager()
		{
		}

		public SoundPoolManager(uint poolSize)
		{
			PoolSize = poolSize;
		}

		public void ClearPool()
		{
			foreach (var item in _soundBufferPool)
			{
				item.Value.Dispose();
			}
			_soundBufferPool.Clear();
		}

		public SoundBuffer LoadSoundBuffer(string soundName)
		{
			SoundBuffer sb;
			if (_soundBufferPool.TryGetValue(soundName, out sb)) return sb;
			if (_soundBufferPool.Count > PoolSize) PoolSize += PoolSizeAutoIncrementSize;
			if (PoolSize > PoolSizeHardLimit)
			{
				PoolSize = PoolSize.Clamp<uint>(0, PoolSizeHardLimit);
				throw new OverflowException("Pool-Size: " + PoolSize + " exceeded Hard Limit: " + PoolSizeHardLimit);
			}
			sb = new SoundBuffer(soundName);
			_soundBufferPool.Add(soundName, sb);
			return sb;
		}

		public Sound LoadSound(string soundName)
		{
			SoundBuffer sb;
			if (_soundBufferPool.TryGetValue(soundName, out sb)) return new Sound(sb);
			if (_soundBufferPool.Count > PoolSize) PoolSize += PoolSizeAutoIncrementSize;
			if (PoolSize > PoolSizeHardLimit)
			{
				PoolSize = PoolSize.Clamp<uint>(0, PoolSizeHardLimit);
				throw new OverflowException("Pool-Size: " + PoolSize + " exceeded Hard Limit: " + PoolSizeHardLimit);
			}
			sb = new SoundBuffer(soundName);
			_soundBufferPool.Add(soundName, sb);
			return new Sound(sb);
		}

		public bool SaveSound(string soundName)
		{
			SoundBuffer sb;
			_soundBufferPool.TryGetValue(soundName, out sb);
			return sb != null && sb.SaveToFile(soundName);
		}

		public static bool SaveSound(string soundName, SoundBuffer soundBuffer)
		{
			return soundBuffer.SaveToFile(soundName);
		}

		public static bool SaveSound(string soundName, Sound sound)
		{
			return SaveSound(soundName, sound.SoundBuffer);
		}

		public static Music LoadMusic(string musicName)
		{
			return new Music(musicName);
		}
	}
}