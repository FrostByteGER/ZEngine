﻿using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine
{
    public class Actor : Transformable, IActorable 
    {
        public Shape CollisionShape { get; set; }
        public bool Movable { get; set; }
        public Vector2f Velocity { get; set; }
        public Vector2f Acceleration { get; set; }
        public float Mass { get; set; }
        public void Tick(float deltaTime)
        {
            throw new System.NotImplementedException();
        }
    }
}