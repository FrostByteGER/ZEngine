using System;
using SFML.System;

namespace SFML_Engine.Engine
{
    class SpriteActor : ITickableInterface ,IMovable ,IPhysicsProperties
    {
        public Vector2f GetAcceleration()
        {
            throw new NotImplementedException();
        }

        public float GetMass()
        {
            throw new NotImplementedException();
        }

        public bool GetMovable()
        {
            throw new NotImplementedException();
        }

        public Vector2f GetVelocity()
        {
            throw new NotImplementedException();
        }

        public void SetAcceleration(Vector2f acceleration)
        {
            throw new NotImplementedException();
        }

        public void SetMass(float mass)
        {
            throw new NotImplementedException();
        }

        public void SetMovable(bool movable)
        {
            throw new NotImplementedException();
        }

        public void SetVelocity(Vector2f velocity)
        {
            throw new NotImplementedException();
        }

        public void Tick(double deltaTime)
        {
            Console.WriteLine("Actor Tick!");
        }


    }
}
