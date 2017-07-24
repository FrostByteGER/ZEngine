using System;
using System.Linq;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Physics;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Dynamics;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public class TDLaserTower : TDTower
	{

		public PhysicsComponent PhysComp { get; set; }

		public TDLaserTower(Level level) : base(level)
		{
		}

		protected override void CreateTower()
		{
			TowerBase = new TDTowerBaseComponent(new Sprite(new Texture("")));
			SetRootComponent(TowerBase);
			
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			PhysComp = RootComponent as PhysicsComponent;
		}

		public override void OnOverlapBegin(Fixture self, Fixture other, Contact contactInfo)
		{
			base.OnOverlapBegin(self, other, contactInfo);
		}

		public override void OnOverlapEnd(Fixture self, Fixture other, Contact contactInfo)
		{
			base.OnOverlapEnd(self, other, contactInfo);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}