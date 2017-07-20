using SFML_Engine.Engine.Game;

namespace SFML_TowerDefense.Source.Game.Core
{
	public class TDActor : Actor
	{

		public TDLevel TDLevelRef { get; private set; } = null;

		public TDActor(Level level) : base(level)
		{
			TDLevelRef = level as TDLevel;
		}

		public virtual void ApplyDamage(int damage)
		{

		}

		public virtual void ApplyDamage(TDActor instigator, int damage)
		{

		}

		public virtual void ApplyDamage(TDActor instigator, int damage, TDDamageType damageType)
		{
			
		}
	}
}