namespace SFML_Engine.Engine.Game
{
    public interface ITickable
    {
        void Tick(float deltaTime);
	    bool CanTick { get; set; }
    }
}
