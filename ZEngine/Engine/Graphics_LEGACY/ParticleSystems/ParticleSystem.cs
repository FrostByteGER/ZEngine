using SFML.Graphics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Graphics.ParticleSystems
{

	public class Particle
	{
		public Vector2 Position { get; set; }
		public float Rotation { get; set; }
		public Vector2 Scale { get; set; }
		public float CurrentLifeTime { get; set; }
		public Color ParticleColor { get; set; }

	}

	public class ParticleSystem : ITickable
	{
		public Vector2 Position { get; set; } = new Vector2();
		public float Rotation { get; set; } = 0.0f;
		public Vector2 Scale { get; set; } = new Vector2(1.0f, 1.0f);

		public float MaxLifeTime { get; set; } = 1.0f;

		public float MaxParticles { get; set; }

		/// <summary>
		/// Particles to spawn per tick, not per second!
		/// </summary>
		public float ParticleSpawnRate { get; set; }

		public bool CanTick { get; set; } = true;


		public ParticleSystem()
		{
		}

		public void Tick(float deltaTime)
		{
			throw new System.NotImplementedException();
		}


	}
}