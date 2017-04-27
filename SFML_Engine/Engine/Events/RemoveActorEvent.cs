﻿using System;

namespace SFML_Engine.Engine.Events
{
	public class RemoveActorEvent<T> : EngineEvent<T> where  T : RemoveActorParams
	{
		public RemoveActorEvent(T parameters) : base(parameters)
		{
			parameters.RemovableActor.MarkedForRemoval = true;
			parameters.RemovableActor.Visible = false;
		}

		public override void ExecuteEvent()
		{
			if (Parameters.RemovableActor != null)
			{
				var actor = Parameters.RemovableActor;
				Engine.Instance.FindLevel(actor.LevelID).UnregisterActor(actor);
				return;
			}
			Console.WriteLine("Failed to Remove Actor");
		}
	}
}