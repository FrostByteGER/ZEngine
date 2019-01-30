﻿using Exofinity.Source.Game.Core;
using Exofinity.Source.Game.Units;
using SFML.Graphics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Dynamics;

namespace Exofinity.Source.Game.Buildings.Towers
{
	public class TDRailgunProjectile : TDProjectile
	{
		public TDRailgunProjectile()
		{
			var projectileSprite = new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("RailgunRod"));
			//var comp = level.PhysicsWorld.ConstructRectangleOverlapComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f), 1.0f, projectileSprite.Scale, BodyType.Dynamic);
			//comp.CollisionCallbacksEnabled = true;
			Projectile = new RWeaponComponent(projectileSprite);
			MovementSpeed = 2000.0f;
			AddComponent(Projectile);
		}

		public override void OnOverlapBegin(Fixture self, Fixture other, Contact contactInfo)
		{
			var hitActor = (other.Body.UserData as ActorComponent)?.ParentActor as RUnit;
			if (hitActor != null && hitActor == Target)
			{
				RLevelRef.DestroyActor(this);
			}
		}
	}
}