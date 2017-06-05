using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Game
{
    public interface ITransformable
    {
	    bool Movable { get; set; }

		TTransformable ComponentTransform { get; set; }
		TTransformable WorldTransform { get;}

	    TVector2f LocalPosition { get; set; }

		float LocalRotation { get; set; }

		TVector2f LocalScale { get; set; }

		TVector2f Origin { get; set; }

		TVector2f WorldPosition { get; set; }

		TVector2f ComponentBounds { get; set; }

		// Location
		void MoveLocal(float x, float y);
	    void MoveLocal(TVector2f position);
		void MoveWorld(float x, float y);
		void MoveWorld(TVector2f position);
		void SetLocalPosition(float x, float y);
		void SetLocalPosition(TVector2f position);
		void SetWorldPosition(float x, float y);
		void SetWorldPosition(TVector2f position);

		// Rotation
		void RotateLocal(float angle);
	    void RotateWorld(float angle);
		void SetLocalRotation(float angle);
	    void SetWorldRotation(float angle);

		// Scale
		void ScaleLocal(float x, float y);
	    void ScaleLocal(TVector2f scale);
		void ScaleWorld(float x, float y);
		void ScaleWorld(TVector2f scale);
		void SetLocalScale(float x, float y);
		void SetLocalScale(TVector2f scale);
		void SetWorldScale(float x, float y);
		void SetWorldScale(TVector2f scale);

	}
}
