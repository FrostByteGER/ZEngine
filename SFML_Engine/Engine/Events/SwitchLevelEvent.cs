namespace SFML_Engine.Engine.Events
{
	public class SwitchLevelEvent<T> : EngineEvent<T> where T : SwitchLevelParams
	{
		public SwitchLevelEvent(T parameters) : base(parameters)
		{
		}

		public override void ExecuteEvent()
		{
			Core.Engine.Instance.LoadLevel(Parameters.NewLevel);
		}
	}
}