using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace SFML_Engine.Engine.Physics
{
    public class PhysicsEngine
    {

        public readonly float Gravity = 9.81f;

        internal void PhysicsTick(float deltaTime, ref List<Transformable> actors)
        {
            Console.WriteLine("Physics Tick");
        }
    }
}