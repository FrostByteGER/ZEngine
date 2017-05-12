using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
<<<<<<< HEAD
	public class CollisionShape : Transformable, ITransformable
	{
=======
    public class CollisionShape
    {
>>>>>>> 4719a280819cca9b8f1a4701d974b3815ccdb8f2
		public bool ShowCollisionShape { get; set; } = false;
		public Color CollisionShapeColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));

<<<<<<< HEAD
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

		public Vector2f GetMid(Vector2f actorPosition)
		{
			return actorPosition;
		}
	}
=======
		public virtual Vector2f CollisionBounds { get; set; }
	    public bool Movable { get; set; }

		public Vector2f Velocity { get; set; }

	    public Vector2f Acceleration { get; set; }
    }
>>>>>>> 4719a280819cca9b8f1a4701d974b3815ccdb8f2
}