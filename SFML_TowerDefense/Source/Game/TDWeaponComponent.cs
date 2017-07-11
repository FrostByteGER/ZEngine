namespace SFML_TowerDefense.Source.Game
{
	public class TDWeaponComponent : TDActorComponent
	{

		public int Damage = 1;
		public float FireRate = 2;
		public float CoolDown = 0;

		public uint Range { get; private set; }




	}
}