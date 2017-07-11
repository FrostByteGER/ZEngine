using SFML_Engine.Engine.Physics;
using SFML_TowerDefense.Source.Game.Units;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public class TDTowerWeaponComponent : TDWeaponComponent
	{
		public TDTower ParentTower { get; set; }
		public TDUnit CurrentTarget { get; private set; }
		private OverlapComponent AttackArea { get; set; }

		public TDDamageType DamageType { get; set; } = TDDamageType.Normal;

		protected virtual void OnEnemyEntersRange(TDUnit enemyInRange)
		{
			if (CurrentTarget != null) return;
			CurrentTarget = enemyInRange;
			ParentTower.TowerState = TDTowerState.Charging;
		}
		protected virtual void OnEnemyLeavesRange()
		{
			CurrentTarget = null;
			ParentTower.TowerState = TDTowerState.Idle;
		}

		public virtual void Fire()
		{
			
		}
	}
}