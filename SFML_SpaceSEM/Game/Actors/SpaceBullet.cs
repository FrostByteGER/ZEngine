using System;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Dynamics;

namespace SFML_SpaceSEM.Game.Actors
{
	public class SpaceBullet : SpriteActor
	{
		public uint Damage { get; set; } = 1;
		public Actor Instigator { get; set; } = null;
		public SpaceBullet(Sprite sprite, Level level) : base(sprite, level)
		{
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public override void OnCollide(Fixture self, Fixture other, Contact contactInfo)
		{
			base.OnCollide(self, other, contactInfo);
			Console.WriteLine("COLLISION");
			LevelReference.DestroyActor(this);
		}

		public override void OnOverlapBegin(Fixture self, Fixture other, Contact contactInfo)
		{
			base.OnOverlapBegin(self, other, contactInfo);
			Console.WriteLine("OVERLAP");
			LevelReference.DestroyActor(this);
		}
	}
}