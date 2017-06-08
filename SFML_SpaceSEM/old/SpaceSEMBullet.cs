using SFML.Graphics;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;

namespace SFML_SpaceSEM
{
	public class SpaceSEMBullet : SpriteActor
	{
		public uint Damage { get; set; } = 1;

		public SpaceSEMBullet()
		{
		}

		public SpaceSEMBullet(Texture texture) : base(texture)
		{
		}

		private void OnHit(Actor other)
		{
			if (!(other is SpaceSEMPlayer || other is SpaceSEMEnemy))
			{
				LevelReference.EngineReference.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(other, this)));
			}
		}

		public override void AfterCollision(Actor actor)
		{
			base.AfterCollision(actor);
			OnHit(actor);
		}

		public override void BeforeCollision(Actor actor)
		{
			base.BeforeCollision(actor);
		}

		public override void IsOverlapping(Actor actor)
		{
			base.IsOverlapping(actor);
			OnHit(actor);
		}
	}
}