using System;
using System.Linq;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Physics;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Dynamics;
using SFML_Engine.Engine.Utility;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public class TDLaserTower : TDTower
	{

		public PhysicsComponent PhysComp { get; set; }

		public TDLaserTower(Level level) : base(level)
		{
			TDLaserWeaponComponent gun = new TDLaserWeaponComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("TowerGunT3")));
			OverlapComponent attackArea = LevelReference.PhysicsEngine.ConstructCircleOverlapComponent(this, true, new TVector2f(), 0, new TVector2f(1.0f), 1.0f, gun.WeaponRange, VelcroPhysics.Dynamics.BodyType.Static);
			SpriteComponent sprite = new SpriteComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("TowerBase")));

			this.AddComponent(sprite);
			this.AddComponent(gun);
			this.CollisionCallbacksEnabled = true;

			attackArea.CollisionBody.OnCollision += gun.OnOverlapBegin;
			attackArea.CollisionBody.OnSeparation += gun.OnOverlapEnd;

			gun.ParentTower = this;
			gun.Laser = new SpriteComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("TowerBullet3")));
		}

		protected override void InitializeActor()
		{
			base.InitializeActor();
		}

		protected override void CreateTower()
		{
			TowerBase = new TDTowerBaseComponent(new Sprite(new Texture("")));
			SetRootComponent(TowerBase);
			
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			PhysComp = RootComponent as PhysicsComponent;
		}

		public override void OnOverlapBegin(Fixture self, Fixture other, Contact contactInfo)
		{
			base.OnOverlapBegin(self, other, contactInfo);
		}

		public override void OnOverlapEnd(Fixture self, Fixture other, Contact contactInfo)
		{
			base.OnOverlapEnd(self, other, contactInfo);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}