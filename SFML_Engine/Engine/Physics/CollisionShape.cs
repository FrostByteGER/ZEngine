using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
    public class CollisionShape : Transformable, ITransformable
    {
		public bool ShowCollisionShape { get; set; } = false;
	    public Color CollisionShapeColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));

	    public bool Movable { get; set; }

		public Vector2f Velocity { get; set; }

	    public Vector2f Acceleration { get; set; }

		public virtual void Move(float x, float y)
		{
			Position += new Vector2f(x, y);
		}

		public virtual void MoveAbsolute(float x, float y)
		{
			Position = new Vector2f(x, y);
		}

		public virtual void Move(Vector2f position)
		{
			Position += position;
		}

		public virtual void MoveAbsolute(Vector2f position)
		{
			Position = position;
		}

		public virtual void Rotate(float angle)
		{
			Rotation += angle;
		}

		public virtual void Rotate(Quaternion angle)
		{
			throw new NotImplementedException();
		}

		public virtual void RotateAbsolute(float angle)
		{
			Rotation = angle;
		}

		public virtual void RotateAbsolute(Quaternion angle)
		{
			throw new NotImplementedException();
		}

		public virtual void ScaleActor(float x, float y)
		{
			base.Scale += new Vector2f(x, y);
		}

		public virtual void ScaleActor(Vector2f scale)
		{
			base.Scale += scale;
		}

		public virtual void ScaleAbsolute(float x, float y)
		{
			base.Scale = new Vector2f(x, y);
		}

		public virtual void ScaleAbsolute(Vector2f scale)
		{
			base.Scale = scale;
		}
	}
}