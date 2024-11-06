using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Events
{
	public class RemoveActorEvent<T> : EngineEvent<T> where  T : RemoveActorParams
	{
		public RemoveActorEvent(T parameters) : base(parameters)
		{
			parameters.RemovableActorOld.MarkedForRemoval = true;
			parameters.RemovableActorOld.Visible = false;
		}

		public override void ExecuteEvent()
		{
			if (Parameters.RemovableActorOld != null)
			{
				var actor = Parameters.RemovableActorOld;
				Parameters.RemovableActorOld.OnGameEnd();
				actor.LevelOldReference.UnregisterActor(actor);
				actor.Dispose();
				return;
			}
			Debug.LogError("Failed to Remove Actor", DebugLogCategories.Engine);
		}
	}
}