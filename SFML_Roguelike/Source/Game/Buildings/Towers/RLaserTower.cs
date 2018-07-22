using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_Roguelike.Source.Game.Buildings.Towers
{
	public class RLaserTower : RTower
	{

		public RLaserTower(Level level) : base(level)
		{
			var gun = new RLaserWeaponComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("TowerGunT3")));
			OverlapComponent attackArea = LevelReference.PhysicsEngine.ConstructCircleOverlapComponent(this, true, new TVector2f(), 0, new TVector2f(1.0f), 1.0f, gun.WeaponRange, VelcroPhysics.Dynamics.BodyType.Static);
			var sprite = new SpriteComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("TowerBase")));

			this.AddComponent(sprite);
			this.AddComponent(gun);
			this.CollisionCallbacksEnabled = true;

			attackArea.CollisionBody.OnCollision += gun.OnOverlapBegin;
			attackArea.CollisionBody.OnSeparation += gun.OnOverlapEnd;

			gun.ParentTower = this;
			gun.LaserSprite = new SpriteComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("LaserBeam")));
		}

		protected override void CreateTower()
		{
		}
	}
}