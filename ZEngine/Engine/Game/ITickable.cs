namespace ZEngine.Engine.Game
{
    public interface ITickable
    {
	    bool CanTick { get; set; }
        void Tick(float deltaTime);
    }
}
