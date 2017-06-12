using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;

namespace SFML_SpaceSEM.Game.Actors.Enemies
{
	public class SpaceShipEnemyFighter : SpaceShipEnemy
	{

		private float timer = 0.0f;

		public SpaceShipEnemyFighter(Sprite sprite, Level level) : base(sprite, level)
		{
			Healthpoints = MaxHealthpoints;
			var weapon1 = new WeaponComponent(new Sprite());
			weapon1.BulletSpeed = weapon1.BulletSpeed * -1;
			weapon1.LocalPosition += new TVector2f(0.0f, 30.0f);
			AddComponent(weapon1);
			WeaponSystems.Add(weapon1);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			FireWeapons();
		}

		public override void OnDeath()
		{
			var killSound = LevelReference.EngineReference.AssetManager.AudioManager.LoadSound(SoundPoolManager.SFXPath + "SFX_Explosion_01.ogg");
			//var fireSound = new Sound(new SoundBuffer(AssetManager.AssetsPath + "SFX_Laser_01.ogg"));
			killSound.Volume = LevelReference.EngineReference.GlobalSoundVolume;
			killSound.Play();
			LevelReference.DestroyActor(this);
			base.OnDeath();
		}

		public override void FireWeapons()
		{
			base.FireWeapons();
		}
	}
}