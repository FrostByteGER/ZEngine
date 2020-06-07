using System;

namespace ZEngine.Engine.IO.UserInput
{
    /// <summary>
    /// Carbon copy of Silk.Net Position2D
    /// </summary>
    [Flags]
    public enum Position2D
    {
        /// <summary>
        /// The hat is centered.
        /// </summary>
        Centered = 0,

        /// <summary>
        /// The hat is pressed up.
        /// </summary>
        Up = 1,

        /// <summary>
        /// The hat is pressed down.
        /// </summary>
        Down = 2,

        /// <summary>
        /// The hat is pressed left.
        /// </summary>
        Left = 4,

        /// <summary>
        /// The hat is pressed right.
        /// </summary>
        Right = 8,

        /// <summary>
        /// The hat is pressed up and to the left.
        /// </summary>
        UpLeft = Up | Left,

        /// <summary>
        /// The hat is pressed up and to the right.
        /// </summary>
        UpRight = Up | Right,

        /// <summary>
        /// The hat is pressed down and to the left.
        /// </summary>
        DownLeft = Down | Left,

        /// <summary>
        /// The hat is pressed down and to the right.
        /// </summary>
        DownRight = Down | Right
    }
}