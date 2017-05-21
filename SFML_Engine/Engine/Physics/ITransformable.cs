using SFML.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
    public interface ITransformable
    {
	    Transformable ComponentTransform { get; set; }
        bool Movable { get; set; }

	    void Move(float x, float y);
	    void SetPosition(float x, float y);
	    void Move(TVector2f position);
	    void SetPosition(TVector2f position);

		void Rotate(float angle);
		void SetRotation(float angle);

		void ScaleActor(float x, float y);
	    void ScaleActor(TVector2f scale);
		void SetScale(float x, float y);
		void SetScale(TVector2f scale);

	}
}
