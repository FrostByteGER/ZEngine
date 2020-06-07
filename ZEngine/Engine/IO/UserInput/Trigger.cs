namespace ZEngine.Engine.IO.UserInput
{
    /// <summary>
    /// Carbon copy of Silk.Net Trigger
    /// </summary>
    public struct Trigger
    {
        /// <summary>
        /// The index of this trigger.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// The position of this trigger; how far down it's currently pressed.
        /// </summary>
        public float Position { get; }

        /// <summary>
        /// Creates a new instance of the Trigger struct.
        /// </summary>
        /// <param name="index">The index of this trigger.</param>
        /// <param name="position">The position of this trigger.</param>
        public Trigger(int index, float position)
        {
            Index = index;
            Position = position;
        }
    }
}