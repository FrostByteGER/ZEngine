using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Events
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
				Parameters.RemovableActor.OnGameEnd();
				actor.LevelReference.UnregisterActor(actor);
				actor.Dispose();
				return;
			}
			Debug.LogError("Failed to Remove Actor", DebugLogCategories.Engine);
		}
	}
}