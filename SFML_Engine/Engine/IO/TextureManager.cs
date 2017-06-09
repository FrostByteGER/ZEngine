using SFML.Graphics;
using System.Collections.Generic;
using System;

namespace SFML_Engine.Engine.IO
{
	public class TextureManager : GenericIOManager
	{


		public Dictionary<string, Texture> Textures { get; private set; } = new Dictionary<string, Texture>();
		public uint PoolSize { get; set; } = 256;

		public TextureManager()
		{
		}

		public TextureManager(uint poolSize)
		{
			PoolSize = poolSize;
		}

		public Texture LoadTexture(string textureName)
		{
			Texture t = null;
			if (!Textures.TryGetValue(textureName, out t))
			{
				t = new Texture(textureName);
				Textures.Add(textureName, t);
			}
			return t;
		}

		public Texture LoadTexture(string textureName, IntRect textureArea)
		{
			Texture t = null;
			if (!Textures.TryGetValue(textureName, out t))
			{
				t = new Texture(textureName, textureArea);
				Textures.Add(textureName, t);
			}
			return t;
		}

		protected override T Load<T>(string path)
		{
			throw new NotImplementedException();
		}

		protected override void Save<T>(string path, T data)
		{
			throw new NotImplementedException();
		}
	}
}