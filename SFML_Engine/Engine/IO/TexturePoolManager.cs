using SFML.Graphics;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.IO
{
	public class TexturePoolManager
	{
		public static string TexturesPath { get; } = AssetManager.GameAssetsPath + "Textures/";

		private readonly Dictionary<string, Texture> _texturePool = new Dictionary<string, Texture>();
		public ReadOnlyDictionary<string, Texture> TexturePool => new ReadOnlyDictionary<string, Texture>(_texturePool);

		public uint PoolSize { get; set; } = 256;
		public uint PoolSizeAutoIncrementSize { get; set; } = 32;
		public uint PoolSizeHardLimit { get; } = ushort.MaxValue;

		public TexturePoolManager()
		{
		}

		public TexturePoolManager(uint poolSize)
		{
			PoolSize = poolSize;
		}

		public void ClearPool()
		{
			foreach (var item in _texturePool)
			{
				item.Value.Dispose();
			}
			_texturePool.Clear();
		}

		public Texture LoadTexture(string textureName)
		{
			Texture t;
			if (_texturePool.TryGetValue(textureName, out t)) return t;
			if (_texturePool.Count > PoolSize) PoolSize += PoolSizeAutoIncrementSize;
			if (PoolSize > PoolSizeHardLimit)
			{
				PoolSize = PoolSize.Clamp<uint>(0, PoolSizeHardLimit);
				throw new OverflowException("Pool-Size: " + PoolSize + " exceeded Hard Limit: " + PoolSizeHardLimit);
			}
			t = new Texture(textureName);
			_texturePool.Add(textureName, t);
			return t;
		}

		public Texture LoadTexture(string textureName, IntRect textureArea)
		{
			Texture t;
			if (_texturePool.TryGetValue(textureName, out t)) return t;
			if (_texturePool.Count > PoolSize) PoolSize += PoolSizeAutoIncrementSize;
			if (PoolSize > PoolSizeHardLimit)
			{
				PoolSize = PoolSize.Clamp<uint>(0, PoolSizeHardLimit);
				throw new OverflowException("Pool-Size: " + PoolSize + " exceeded Hard Limit: " + PoolSizeHardLimit);
			}
			t = new Texture(textureName);
			_texturePool.Add(textureName, t);
			return t;
		}

		public bool SaveTexture(string textureName)
		{
			Texture t;
			_texturePool.TryGetValue(textureName, out t);
			return t != null && t.CopyToImage().SaveToFile(textureName);
		}

		public static bool SaveTexture(string textureName, Texture texture)
		{
			return texture.CopyToImage().SaveToFile(textureName);
		}

		public static bool SaveTexture(string textureName, RenderTexture renderTexture)
		{
			return renderTexture.Texture.CopyToImage().SaveToFile(textureName);
		}
	}
}