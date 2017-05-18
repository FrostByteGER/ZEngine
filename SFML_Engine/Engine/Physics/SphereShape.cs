using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public class SphereShape : CollisionShape
    {
		/// <summary>
		/// Diameter of this Shape. Used for Scale Calculations, never change after constructor assignment!
		/// TODO. Make custom set that updates SphereRadius
		/// </summary>
		public float SphereDiameter { get; private set; } = 1f;
		/// <summary>
		/// Used for Scale Calculations, never change after constructor assignment!
		/// </summary>
		public float SphereRadius { get; } = 0.5f;

        public SphereShape()
        {
		}

        public SphereShape(float sphereDiameter)
        {
            SphereDiameter = sphereDiameter;
	        SphereRadius = sphereDiameter / 2.0f;
        }

		public Vector2f GetMid(Vector2f actorPosition)
		{
			return new Vector2f(actorPosition.X + SphereDiameter/2f, actorPosition.Y + SphereDiameter/2f);
		}

	    public override void ScaleActor(float x, float y)
	    {
		    base.ScaleActor(x, y);
		    SphereDiameter = SphereRadius * 2.0f * Scale.X;
	    }

	    public override void ScaleActor(Vector2f scale)
	    {
		    base.ScaleActor(scale);
		    SphereDiameter = SphereRadius * 2.0f * Scale.X;
		}

	    public override void ScaleAbsolute(float x, float y)
	    {
		    base.ScaleAbsolute(x, y);
		    SphereDiameter = SphereRadius * 2.0f * x;
		}

	    public override void ScaleAbsolute(Vector2f scale)
	    {
		    base.ScaleAbsolute(scale);
		    SphereDiameter = SphereRadius * 2.0f * scale.X;
		}


    }
}