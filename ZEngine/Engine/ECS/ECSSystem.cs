// ReSharper disable InconsistentNaming
namespace ZEngine.Engine.ECS
{
    public interface IECSSystem
    {
        void Update(float deltaTime);
    }

    public class ECSSystem : IECSSystem
    {
        public void Update(float deltaTime)
        {
            
        }
    }
}