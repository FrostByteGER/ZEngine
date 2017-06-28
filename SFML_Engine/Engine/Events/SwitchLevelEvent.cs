namespace SFML_Engine.Engine.Events
{
	public class SwitchLevelEvent<T> : EngineEvent<T> where T : SwitchLevelParams
	{
		public SwitchLevelEvent(T parameters) : base(parameters)
		{
			Core.Engine.Instance.ActiveLevel.LevelTicking = false;
		}

		public override void ExecuteEvent()
		{
			Core.Engine.Instance.LoadLevel(Parameters.NewLevel);
		}
	}
}