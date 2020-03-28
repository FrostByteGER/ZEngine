using System.Numerics;

namespace ZEngine.Engine.Game
{
    public interface ITransformable
    {
	    bool Movable { get; set; }

		Transform ComponentTransform { get; set; }
		Transform WorldTransform { get;}

	    Vector2 LocalPosition { get; set; }

		float LocalRotation { get; set; }

		Vector2 LocalScale { get; set; }

		Vector2 Origin { get; set; }

		Vector2 WorldPosition { get; set; }

		Vector2 ComponentBounds { get; set; }

		// Location
		void MoveLocal(float x, float y);
	    void MoveLocal(Vector2 position);
		void MoveWorld(float x, float y);
		void MoveWorld(Vector2 position);
		void SetLocalPosition(float x, float y);
		void SetLocalPosition(Vector2 position);
		void SetWorldPosition(float x, float y);
		void SetWorldPosition(Vector2 position);

		// Rotation
		void RotateLocal(float angle);
	    void RotateWorld(float angle);
		void SetLocalRotation(float angle);
	    void SetWorldRotation(float angle);

		// Scale
		void ScaleLocal(float x, float y);
	    void ScaleLocal(Vector2 scale);
		void ScaleWorld(float x, float y);
		void ScaleWorld(Vector2 scale);
		void SetLocalScale(float x, float y);
		void SetLocalScale(Vector2 scale);
		void SetWorldScale(float x, float y);
		void SetWorldScale(Vector2 scale);

	}
}
