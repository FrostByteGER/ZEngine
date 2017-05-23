namespace SFML_Engine.Engine.Physics
{
	public class BulletPhysicsEngine
	{
		/*


		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, Vector2f scale, CollisionShape shape, short collisionType, short collisionResponseChannels, Vector3 allowedMovementAxis, Vector3 allowedRotationAxis)
		{
			var comp = new CollisionComponent();
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, scale, shape, allowedMovementAxis, allowedRotationAxis);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			comp.ScaleAbsolute(scale);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, Vector2f scale, CollisionShape shape, short collisionType, short collisionResponseChannels, TVector2f allowedMovementAxis, bool allowRotation)
		{
			var comp = new CollisionComponent();
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, scale, shape, allowedMovementAxis, Convert.ToSingle(allowRotation));
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			comp.ScaleAbsolute(scale);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, Vector2f scale, CollisionShape shape, short collisionType, short collisionResponseChannels)
		{
			var comp = new CollisionComponent();
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, scale, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			comp.ScaleAbsolute(scale);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, Vector2f scale, CollisionShape shape, short collisionType)
		{
			var comp = new CollisionComponent();
			comp.CollisionType = collisionType;
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, scale, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			comp.ScaleAbsolute(scale);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, Vector2f scale, CollisionShape shape, Vector3 allowedMovementAxis, Vector3 allowedRotationAxis)
		{
			var comp =  new CollisionComponent();
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, scale, shape, allowedMovementAxis, allowedRotationAxis);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			comp.ScaleAbsolute(scale);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, Vector2f scale, CollisionShape shape, TVector2f allowedMovementAxis, bool allowRotation)
		{
			var comp = new CollisionComponent();
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, scale, shape, allowedMovementAxis, Convert.ToSingle(allowRotation));
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			comp.ScaleAbsolute(scale);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, Vector2f scale, CollisionShape shape)
		{
			var comp = new CollisionComponent();
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, scale, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			comp.ScaleAbsolute(scale);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, CollisionShape shape, short collisionType, short collisionResponseChannels, Vector3 allowedMovementAxis, Vector3 allowedRotationAxis)
		{
			var comp = new CollisionComponent();
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, shape, allowedMovementAxis, allowedRotationAxis);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, CollisionShape shape, short collisionType, short collisionResponseChannels, TVector2f allowedMovementAxis, bool allowRotation)
		{
			var comp = new CollisionComponent();
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, shape, allowedMovementAxis, Convert.ToSingle(allowRotation));
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, CollisionShape shape, short collisionType, short collisionResponseChannels)
		{
			var comp = new CollisionComponent();
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, CollisionShape shape, short collisionType, Vector3 allowedMovementAxis, Vector3 allowedRotationAxis)
		{
			var comp = new CollisionComponent();
			comp.CollisionType = collisionType;
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, shape, allowedMovementAxis, allowedRotationAxis);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, CollisionShape shape, short collisionType, TVector2f allowedMovementAxis, bool allowRotation)
		{
			var comp = new CollisionComponent();
			comp.CollisionType = collisionType;
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, shape, allowedMovementAxis, Convert.ToSingle(allowRotation));
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, CollisionShape shape, short collisionType)
		{
			var comp = new CollisionComponent();
			comp.CollisionType = collisionType;
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, CollisionShape shape, Vector3 allowedMovementAxis, Vector3 allowedRotationAxis)
		{
			var comp = new CollisionComponent();
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, shape, allowedMovementAxis, allowedRotationAxis);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, CollisionShape shape, TVector2f allowedMovementAxis, bool allowRotation)
		{
			var comp = new CollisionComponent();
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, shape, allowedMovementAxis, Convert.ToSingle(allowRotation));
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public CollisionComponent ConstructCollisionComponent(float mass, Vector2f position, float angle, CollisionShape shape)
		{
			var comp = new CollisionComponent();
			comp.CollisionObject = ConstructRigidBody(comp, mass, position, angle, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterRigidBody(comp.CollisionBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public OverlapComponent ConstructOverlapComponent(Vector2f position, float angle, Vector2f scale, CollisionShape shape, short collisionType, short collisionResponseChannels)
		{
			var comp = new OverlapComponent();
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			comp.CollisionObject = ConstructOverlapBody(comp, position, angle, scale, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			comp.ScaleAbsolute(scale);
			RegisterGhostObject(comp.OverlapBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public OverlapComponent ConstructOverlapComponent(Vector2f position, float angle, Vector2f scale, CollisionShape shape, short collisionType)
		{
			var comp = new OverlapComponent();
			comp.CollisionType = collisionType;
			comp.CollisionObject = ConstructOverlapBody(comp, position, angle, scale, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			comp.ScaleAbsolute(scale);
			RegisterGhostObject(comp.OverlapBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public OverlapComponent ConstructOverlapComponent(Vector2f position, float angle, Vector2f scale, CollisionShape shape)
		{
			var comp = new OverlapComponent();
			comp.CollisionObject = ConstructOverlapBody(comp, position, angle, scale, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			comp.ScaleAbsolute(scale);
			RegisterGhostObject(comp.OverlapBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public OverlapComponent ConstructOverlapComponent(Vector2f position, float angle, CollisionShape shape, short collisionType, short collisionResponseChannels)
		{
			var comp = new OverlapComponent();
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			comp.CollisionObject = ConstructOverlapBody(comp, position, angle, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterGhostObject(comp.OverlapBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public OverlapComponent ConstructOverlapComponent(Vector2f position, float angle, CollisionShape shape, short collisionType)
		{
			var comp = new OverlapComponent();
			comp.CollisionType = collisionType;
			comp.CollisionObject = ConstructOverlapBody(comp, position, angle, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterGhostObject(comp.OverlapBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public OverlapComponent ConstructOverlapComponent(Vector2f position, float angle, CollisionShape shape)
		{
			var comp = new OverlapComponent();
			comp.CollisionObject = ConstructOverlapBody(comp, position, angle, shape);
			comp.MoveAbsolute(position);
			comp.RotateAbsolute(angle);
			RegisterGhostObject(comp.OverlapBody, comp.CollisionType, comp.CollisionResponseChannels);
			return comp;
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Matrix startTransform, CollisionShape shape, Vector3 allowedMovementAxis, Vector3 allowedRotationAxis)
		{
			// rigidbody is dynamic if and only if mass is non zero, otherwise static
			bool isDynamic = Math.Abs(mass) > 0.00001f;
			var localInertia = Vector3.Zero;
			if (isDynamic) shape.CalculateLocalInertia(mass, out localInertia);
			// using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
			var myMotionState = new DefaultMotionState(startTransform);

			var rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
			var body = new RigidBody(rbInfo)
			{
				UserObject = parent,
				LinearFactor = allowedMovementAxis,
				AngularFactor = allowedRotationAxis
			};
			rbInfo.Dispose();
			return body;
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Matrix startTransform, CollisionShape shape)
		{
			// rigidbody is dynamic if and only if mass is non zero, otherwise static
			bool isDynamic = Math.Abs(mass) > 0.00001f;
			var localInertia = Vector3.Zero;
			if (isDynamic) shape.CalculateLocalInertia(mass, out localInertia);

			// using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
			var myMotionState = new DefaultMotionState(startTransform);

			var rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
			var body = new RigidBody(rbInfo)
			{
				UserObject = parent,
				LinearFactor = AllowedMovementAxis,
				AngularFactor = AllowedRotationAxis
			};
			rbInfo.Dispose();
			return body;
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Vector2f position, float angle, CollisionShape shape, Vector3 allowedMovementAxis, Vector3 allowedRotationAxis)
		{
			return ConstructRigidBody(parent, mass, position, angle, new Vector2f(1.0f, 1.0f), shape, allowedMovementAxis, allowedRotationAxis);
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Vector2f position, float angle, CollisionShape shape, TVector2f allowedMovementAxis, float allowRotation)
		{
			return ConstructRigidBody(parent, mass, position, angle, new Vector2f(1.0f, 1.0f), shape, new Vector3(allowedMovementAxis.X, allowedMovementAxis.Y, 0.0f), new Vector3(0.0f, 0.0f, allowRotation));
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Vector2f position, float angle, CollisionShape shape)
		{
			return ConstructRigidBody(parent, mass, position, angle, new Vector2f(1.0f, 1.0f), shape);
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Vector2f position, float angle, Vector2f scale, CollisionShape shape, Vector3 allowedMovementAxis, Vector3 allowedRotationAxis)
		{
			Matrix t = EngineMath.TransformFromPosRotScaleBt(position, angle, scale);
			return ConstructRigidBody(parent, mass, t, shape, allowedMovementAxis, allowedRotationAxis);
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Vector2f position, float angle, Vector2f scale, CollisionShape shape, TVector2f allowedMovementAxis, float allowRotation)
		{
			Matrix t = EngineMath.TransformFromPosRotScaleBt(position, angle, scale);
			return ConstructRigidBody(parent, mass, t, shape, new Vector3(allowedMovementAxis.X, allowedMovementAxis.Y, 0.0f), new Vector3(0.0f, 0.0f, allowRotation));
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Vector2f position, float angle, Vector2f scale, CollisionShape shape)
		{
			Matrix t = EngineMath.TransformFromPosRotScaleBt(position, angle, scale);
			return ConstructRigidBody(parent, mass, t, shape);
		}

		public static GhostObject ConstructOverlapBody(object parent, Matrix startTransform, CollisionShape shape)
		{
			GhostObject ghost = new GhostObject();
			ghost.CollisionShape = shape;
			ghost.WorldTransform = startTransform;
			ghost.CollisionFlags |= CollisionFlags.NoContactResponse;
			ghost.UserObject = parent;
			return ghost;
		}

		public static GhostObject ConstructOverlapBody(object parent, Vector2f position, float angle, CollisionShape shape)
		{
			return ConstructOverlapBody(parent, position, angle, new Vector2f(1.0f, 1.0f), shape);
		}

		public static GhostObject ConstructOverlapBody(object parent, Vector2f position, float angle, Vector2f scale,
			CollisionShape shape)
		{
			Matrix t = Matrix.Translation(position.X, position.Y, 0.0f) * Matrix.RotationZ(angle) *
			           Matrix.Scaling(scale.X, scale.Y, 1.0f);
			return ConstructOverlapBody(parent, t, shape);
		}

	*/
	}
}