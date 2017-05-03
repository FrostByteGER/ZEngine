using System.Numerics;
using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public interface ITransformable
    {

        bool Movable { get; set; }

	    void Move(float x, float y);
	    void MoveAbsolute(float x, float y);
	    void Move(Vector2f position);
	    void MoveAbsolute(Vector2f position);

		void Rotate(float angle);
	    void Rotate(Quaternion angle);
		void RotateAbsolute(float angle);
	    void RotateAbsolute(Quaternion angle);

		void ScaleActor(float x, float y);
	    void ScaleActor(Vector2f scale);
		void ScaleAbsolute(float x, float y);
		void ScaleAbsolute(Vector2f scale);

	}
}
