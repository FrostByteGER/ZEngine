using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public class PhysicsEngine
    {

        public readonly float Gravity = 9.81f;

        private SpriteActor CollidableGroup;

        internal void PhysicsTick(float deltaTime, ref List<Transformable> actors)
        {
            Console.WriteLine("Physics Tick");
        }

        private void moveActors(float deltaTime, ref List<Transformable> actors)
        {

            Vector2f VelocityTemp;

            foreach (var actor in actors)
            {
                var OneActor = actor as IActorable;
                //tickableActor?.Tick(deltaTime);

                if (OneActor != null)
                {
                    if (OneActor.Movable)
                    {
                        
                    }
                }


            }
        }

    }
}