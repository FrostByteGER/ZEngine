using SFML.System;

namespace SFML.Graphics.Engine
{
    public interface IMovable
    {

        bool Movable { get; set; }
        Vector2f Velocity { get; set; }
        Vector2f Acceleration { get; set; }

	    void Move(float x, float y);
	    void Move(Vector2f position);

    }
}
