using Exofinity.Source.Game.Core;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;

namespace Exofinity.Source.Game.TileMap
{
	public class RFieldActor : RActor
	{

		private TVector2i _tilePosition = new TVector2i();
		/// <summary>
		/// Set the Tile-Position of this actor. 
		/// DO NOT ATTEMPT TO SET ACTOR LOCATION VIA POSITION PROPERTY AS IT DESYNCS ITS WORLD POSITION AND TILE POSITION
		/// </summary>
		public TVector2i TilePosition
		{
			get => _tilePosition;
			set
			{
				_tilePosition = value;
				Position = RLevelRef.TileCoordsToWorldCoords(value);
			}
		}

		public RFieldActor(Level level) : base(level)
		{
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}