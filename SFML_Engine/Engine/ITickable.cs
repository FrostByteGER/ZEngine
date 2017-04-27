namespace SFML_Engine.Engine
{
    public interface ITickable
    {
        void Tick(float deltaTime);
	    //bool CanTick { get; set; }
    }
}
