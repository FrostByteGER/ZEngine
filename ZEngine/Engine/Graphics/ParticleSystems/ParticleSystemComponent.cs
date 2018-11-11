using SFML.Graphics;

namespace ZEngine.Engine.Graphics.ParticleSystems
{
	public class ParticleSystemComponent : RenderComponent
	{

		public ParticleSystem ParticleSystem { get; set; }
		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
		}
	}
}