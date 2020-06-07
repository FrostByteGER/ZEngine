namespace ZEngine.Engine.IO.UserInput
{
    /// <summary>
    /// Carbon copy of Silk.Net Button
    /// </summary>
    public struct Button
    {
        /// <summary>
        /// The name of this button. Only guaranteed to be valid if this comes from an <see cref="IGamepad"/>.
        /// </summary>
        public ButtonType Name { get; }

        /// <summary>
        /// The index of this button. Use this if this button comes from an <see cref="IJoystick"/>.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Whether or not this button is currently pressed.
        /// </summary>
        public bool Pressed { get; }

        /// <summary>
        /// Creates a new instance of the Button struct.
        /// </summary>
        /// <param name="name">The name of this button.</param>
        /// <param name="index">The index of this button.</param>
        /// <param name="pressed">Whether or not this button is currently pressed.</param>
        public Button(ButtonType name, int index, bool pressed)
        {
            Name = name;
            Index = index;
            Pressed = pressed;
        }
    }
}