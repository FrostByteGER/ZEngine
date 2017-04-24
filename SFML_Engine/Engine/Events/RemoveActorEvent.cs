using System;

namespace SFML_Engine.Engine.Events
{
	public class RemoveActorEvent<T> : EngineEvent<T> where  T : RemoveActorParams
	{
		public RemoveActorEvent(T parameters) : base(parameters)
		{
			parameters.RemovableActor.MarkedForRemoval = true;
		}

		public override void ExecuteEvent()
		{
			var actor = Parameters.RemovableActor;
			var result = Engine.Instance.FindLevel((uint) actor.LevelID).UnregisterActor(actor);
			Console.WriteLine("Removing Actor: " + Parameters.RemovableActor.ActorName + " | Result: " + result);
		}
	}
}