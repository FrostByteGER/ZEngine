﻿using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using SFML_Roguelike.Source.Game.Core;

namespace SFML_Roguelike.Source.Game.TileMap
{
	public class TDFieldActor : TDActor
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
				Position = TDLevelRef.TileCoordsToWorldCoords(value);
			}
		}

		public TDFieldActor(Level level) : base(level)
		{
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}