using Exofinity.Source.Game.Core;
using SFML.Graphics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Graphics;

namespace Exofinity.Source.Game.Buildings
{
	public class RNexus : RBuilding
	{

		public uint NexusID { get; set; } = 0;
		public TDNexusState NexusState { get; set; } = TDNexusState.Alive;
		public RNexus()

		{
			//var nexusSprite = new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("Nexus")));
			//SetRootComponent(nexusSprite);
			//Origin = nexusSprite.Origin;
		}

		public override void ApplyDamage(RActor instigator, float damage)
		{
			if (NexusState == TDNexusState.Dead) return;
			base.ApplyDamage(instigator, damage);
			if (damage >= Health)
			{
				Health = 0;
				NexusState = TDNexusState.Dead;
				if(RGameModeRef.NexusLost.Status != SFML.Audio.SoundStatus.Playing) RGameModeRef.NexusLost.Play();
				CanTick = false;
			}
			else
			{
				Health -= (uint)damage;
				if (RGameModeRef.NexusUnderAttack.Status != SFML.Audio.SoundStatus.Playing) RGameModeRef.NexusUnderAttack.Play();
			}
			
		}


        protected override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}

	public enum TDNexusState
	{
		Alive,
		Dead
	}
}