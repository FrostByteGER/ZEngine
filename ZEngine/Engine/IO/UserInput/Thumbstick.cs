﻿using System;

namespace ZEngine.Engine.IO.UserInput
{
    /// <summary>
    /// Carbon copy of Silk.Net Thumbstick
    /// </summary>
    public struct Thumbstick
    {
        /// <summary>
        /// The index of this stick.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// The x-axis of the stick, from -1.0 to 1.0
        /// </summary>
        public float X { get; }

        /// <summary>
        /// The y-axis of the stick, from -1.0 to 1.0
        /// </summary>
        public float Y { get; }

        /// <summary>
        /// The current position of the stick, from 0.0 to 1.0.
        /// </summary>
        public float Position => (float)Math.Sqrt(X * X + Y * Y);

        /// <summary>
        /// The current direction of the stick, from 0.0 to 360.0.
        /// </summary>
        public float Direction => (float)Math.Atan2(Y, X);

        /// <summary>
        /// Creates a new instance of the Thumbstick struct.
        /// </summary>
        /// <param name="index">The index of the stick.</param>
        /// <param name="x">The position of the stick.</param>
        /// <param name="y">The direction of the stick.</param>
        public Thumbstick(int index, float x, float y)
        {
            Index = index;
            X = x;
            Y = y;
        }
    }
}