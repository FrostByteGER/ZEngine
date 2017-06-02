using SFML.Graphics;

namespace SFML_Engine.Engine.Graphics.ParticleSystems
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