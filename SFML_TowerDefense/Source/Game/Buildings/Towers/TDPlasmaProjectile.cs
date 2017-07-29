using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using SFML_TowerDefense.Source.Game.Core;
using SFML_TowerDefense.Source.Game.Units;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Dynamics;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public class TDPlasmaProjectile : TDProjectile
	{
		public TDPlasmaProjectile(Level level) : base(level)
		{
			var projectileSprite = new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("PlasmaProjectile"));
			var comp = level.PhysicsEngine.ConstructRectangleOverlapComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f), 1.0f, projectileSprite.Scale, BodyType.Dynamic);
			comp.CollisionCallbacksEnabled = true;
			Projectile = new TDWeaponComponent(projectileSprite);
			MovementSpeed = 300.0f;
			AddComponent(Projectile);
		}

		public override void OnOverlapBegin(Fixture self, Fixture other, Contact contactInfo)
		{
			var hitActor = (other.Body.UserData as ActorComponent)?.ParentActor as TDUnit;
			if (hitActor != null && hitActor == Target)
			{
				hitActor.ApplyDamage(this, Projectile.WeaponDamage, Projectile.DamageType);
				TDLevelRef.DestroyActor(this);
			}
		}
	}
}