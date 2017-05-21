using System.Collections.Generic;
using SFML.System;

namespace SFML_Engine.Engine.Game
{

	public class ActorInformation
	{
		public uint ActorID { get; set; }
		public uint ActorLevelID { get; set; }
		public Vector2f ActorPosition { get; set; }
		public float ActorRotation { get; set; }
		public Vector2f ActorScale { get; set; }
		public Vector2f ActorOrigin { get; set; }
		public bool ActorMovable { get; set; }
		public Vector2f ActorVelocity { get; set; }
		public float ActorMaxVelocity { get; set; }
		public Vector2f ActorAcceleration { get; set; }
		public float ActorMaxAcceleration { get; set; }
		public float ActorMass { get; set; }
		public float ActorFriction { get; set; }
		public bool ActorHasGravity { get; set; }

		public ActorInformation(uint actorId, uint actorLevelId, Vector2f actorPosition, float actorRotation, Vector2f actorScale, Vector2f actorOrigin, bool actorMovable, Vector2f actorVelocity, float actorMaxVelocity, Vector2f actorAcceleration, float actorMaxAcceleration, float actorMass, float actorFriction, bool actorHasGravity)
		{
			ActorID = actorId;
			ActorLevelID = actorLevelId;
			ActorPosition = actorPosition;
			ActorRotation = actorRotation;
			ActorScale = actorScale;
			ActorOrigin = actorOrigin;
			ActorMovable = actorMovable;
			ActorVelocity = actorVelocity;
			ActorMaxVelocity = actorMaxVelocity;
			ActorAcceleration = actorAcceleration;
			ActorMaxAcceleration = actorMaxAcceleration;
			ActorMass = actorMass;
			ActorFriction = actorFriction;
			ActorHasGravity = actorHasGravity;
		}
	}

	public class GameLevelState
	{
		
		public Time Timestamp { get; set; }
		public List<ActorInformation> ActorStates { get; set; }

		public GameLevelState(Time timestamp, List<Actor> actors)
		{
			Timestamp = timestamp;
		}
	}
}