using System.Numerics;
using SFML.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
    public interface ITransformable
    {
	    Transformable Transform { get; set; }
        bool Movable { get; set; }

	    void Move(float x, float y);
	    void MoveAbsolute(float x, float y);
	    void Move(TVector2f position);
	    void MoveAbsolute(TVector2f position);

		void Rotate(float angle);
	    void Rotate(Quaternion angle);
		void RotateAbsolute(float angle);
	    void RotateAbsolute(Quaternion angle);

		void ScaleActor(float x, float y);
	    void ScaleActor(TVector2f scale);
		void ScaleAbsolute(float x, float y);
		void ScaleAbsolute(TVector2f scale);

	}
}
