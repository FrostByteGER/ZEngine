﻿using SFML.Graphics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Graphics;
using ZEngine.Engine.Physics;
using ZEngine.Engine.Utility;

namespace Exofinity.Source.Game.Buildings.Towers
{
	public class RPlasmaTower : RTower
	{
		public RPlasmaTower()
		{
			var gun = new RPlasmaWeaponComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("TowerGunT1")));
			OverlapComponent attackArea = LevelReference.PhysicsWorld.ConstructCircleOverlapComponent(this, true, new TVector2f(), 0, new TVector2f(1.0f), 1.0f, gun.WeaponRange, VelcroPhysics.Dynamics.BodyType.Static);
			var sprite = new SpriteComponent(new Sprite(LevelReference.EngineReference.AssetManager.LoadTexture("TowerBase")));

			this.AddComponent(sprite);
			this.AddComponent(gun);
			this.CollisionCallbacksEnabled = true;

			attackArea.CollisionBody.OnCollision += gun.OnOverlapBegin;
			attackArea.CollisionBody.OnSeparation += gun.OnOverlapEnd;

			gun.ParentTower = this;
		}

		protected override void CreateTower()
		{
		}
	}
}