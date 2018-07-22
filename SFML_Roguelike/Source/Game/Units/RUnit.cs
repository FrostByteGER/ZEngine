using System;
using System.Linq;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;
using SFML_Roguelike.Source.Game.Buildings;
using SFML_Roguelike.Source.Game.Core;
using SFML_Roguelike.Source.GUI;
using VelcroPhysics.Dynamics;

namespace SFML_Roguelike.Source.Game.Units
{
	public class RUnit : RActor, ICloneable
	{

		public float HP { get; set; } = 1000.0f;
		public float MovmentSpeed { get; set; } = 1;
		public RWaypoint CurrentWaypoint { get; set; }
		public uint Damage { get; set; } = 5;

		public RDamageType ElementResistances { get; set; } = RDamageType.None;
		public float WaypointThreshold { get; set; } = 2.0f;
		public RUnitState UnitState { get; set; } = RUnitState.Walking;
		
		public PhysicsComponent PhysComp { get; set; }

		public RUnit(Level level) : base(level)
		{
			var spriteComp = new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("TowerBase2")));
			PhysComp = level.PhysicsEngine.ConstructCircleOverlapComponent(this, true, new TVector2f(), 0, new TVector2f(1.0f), 1.0f, spriteComp.ComponentBounds.X, BodyType.Dynamic);

			// HOLY SHIT... I SPENT FUCKING 4 HOURS TRACING THIS SHIT. IT FUCKING CAUSED WEIRD COLLISION/OVERLAP BEHAVIOUR AND DROVE ME TO INSANITY. ALL I HAD TO FIX WAS TO ENSURE THAT THIS MOTHERFUCKER DOESN'T EVER SLEEP. YOU SON OF A BITCH STAY AWAKE TILL U DIE. -Kevin
			PhysComp.CollisionBody.SleepingAllowed = false;

			PhysComp.CollisionCallbacksEnabled = true;

			AddComponent(spriteComp);

			PhysComp.Visible = true;
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
		}

		public override void ApplyDamage(RActor instigator, float damage, RDamageType damageType)
		{
			if (UnitState == RUnitState.Dead) return;
			Console.WriteLine("APPLYING DAMAGE FROM" + instigator.GenerateFullName());
			var resistanceMultiplier = 1.0f;
			if (ElementResistances != RDamageType.None && (ElementResistances & damageType) == damageType) resistanceMultiplier = 0.5f;
			var applyingDamage = damage * resistanceMultiplier;
			HP -= applyingDamage;
			var popupText = new Text(applyingDamage.ToString(), RGameModeRef.GameFont, 16);
			var textComp = new RPopupTextComponent(popupText);
			AddComponent(textComp);
			textComp.TargetPosition = textComp.LocalPosition - textComp.TargetPosition;
			if (HP <= 0.0f)
			{
				HP = 0.0f;
				UnitState = RUnitState.Dead;
				OnDeath();
				LevelReference.DestroyActor(this, this);
			}
		}

		public void Explode()
		{
			var target = LevelReference.FindActorsInLevel<RNexus>().FirstOrDefault(nexus => nexus.NexusID == CurrentWaypoint.TargetNexus);
			target?.ApplyDamage(this, (int) Damage);
			OnDeath();
			LevelReference.DestroyActor(this, this);
		}

		public void OnDeath()
		{
			--RGameModeRef.EnemiesLeftInCurrentWave;
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			
			if (CurrentWaypoint != null)
			{
				if ((Position - CurrentWaypoint.Position).LengthSquared > WaypointThreshold * WaypointThreshold)
				{
					Position = EngineMath.VInterpToConstant(Position, CurrentWaypoint.Position, deltaTime, 50.0f);
				}
				else
				{
					if (CurrentWaypoint.NextWaypoint == null)
					{
						Explode();
						return;
					}
					CurrentWaypoint = CurrentWaypoint.NextWaypoint;
					
				}
			}
			else
			{
				Explode();
			}
		}

		/// <summary>
		/// NOT WORKING CURRENTLY!
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}