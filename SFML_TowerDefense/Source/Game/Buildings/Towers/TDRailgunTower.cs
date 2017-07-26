using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public class TDRailgunTower : TDTower
	{
		public TDRailgunTower(Level level) : base(level)
		{
			TDLaserWeaponComponent gun = new TDLaserWeaponComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("TowerGunT2")));
			OverlapComponent attackArea = LevelReference.PhysicsEngine.ConstructCircleOverlapComponent(this, true, new TVector2f(), 0, new TVector2f(1.0f), 1.0f, gun.WeaponRange, VelcroPhysics.Dynamics.BodyType.Static);
			SpriteComponent sprite = new SpriteComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("TowerBase")));

			this.AddComponent(sprite);
			this.AddComponent(gun);
			this.CollisionCallbacksEnabled = true;

			attackArea.CollisionBody.OnCollision += gun.OnOverlapBegin;
			attackArea.CollisionBody.OnSeparation += gun.OnOverlapEnd;

			gun.ParentTower = this;
			gun.LaserSprite = new SpriteComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("TowerBullet3")));
		}

		protected override void CreateTower()
		{
			TowerBase = new TDTowerBaseComponent(new Sprite(new Texture("")));
			SetRootComponent(TowerBase);
		}
	}
}