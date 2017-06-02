using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Graphics.ParticleSystems
{

	public class Particle
	{
		public TVector2f Position { get; set; }
		public float Rotation { get; set; }
		public TVector2f Scale { get; set; }
		public float CurrentLifeTime { get; set; }
		public Color ParticleColor { get; set; }

	}

	public class ParticleSystem : ITickable
	{
		public TVector2f Position { get; set; } = new TVector2f();
		public float Rotation { get; set; } = 0.0f;
		public TVector2f Scale { get; set; } = new TVector2f(1.0f, 1.0f);

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