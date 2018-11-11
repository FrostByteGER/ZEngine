using System;
using Exofinity.Source.Game.Core;
using Exofinity.Source.Game.Units;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;

namespace Exofinity.Source.Game.Buildings.Towers
{
	public abstract class TDProjectile : TDActor
	{
		
		public TDWeaponComponent Projectile { get; set; }
		public TDUnit Target { get; set; }
		public float MovementSpeed { get; set; } = 300.0f;
		public float TargetThreshold { get; set; } = 2.0f;
		protected TDProjectile(Level level) : base(level)
		{

		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (Target == null || Target.UnitState == TDUnitState.Dead || Target.MarkedForRemoval)
			{
				TDLevelRef.DestroyActor(this);
				return;
			}
			RotateProjectileTo();
			if ((Position - Target.Position).LengthSquared > TargetThreshold * TargetThreshold)
			{
				Position = EngineMath.VInterpToConstant(Position, Target.Position, deltaTime, MovementSpeed);
			}
		}

		public void RotateProjectileTo()
		{
			var dic = Position - Target.Position;

			dic = new TVector2f(dic.X / (Math.Abs(dic.X) + Math.Abs(dic.Y)), dic.Y / (Math.Abs(dic.X) + Math.Abs(dic.Y)));

			Rotation = (float)(Math.Atan2(dic.X, -dic.Y) * 180 / Math.PI);
			Projectile.Sprite.Rotation = Rotation;
		}
	}
}