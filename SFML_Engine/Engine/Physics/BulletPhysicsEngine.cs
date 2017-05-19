using System;
using System.Collections.Generic;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public class BulletPhysicsEngine
	{
		/*
		public CollisionConfiguration CollisionConfig { get; private set; }
		public CollisionDispatcher CollisionDispatcher { get; private set; }
		public BroadphaseInterface Broadphase { get; private set; }
		public DynamicsWorld PhysicsWorld { get; private set; }
		public List<CollisionShape> CollisionShapes { get; } = new List<CollisionShape>();
		private TVector2f gravity;

		public TVector2f Gravity
		{
			get => PhysicsWorld != null ? (TVector2f)PhysicsWorld.Gravity : gravity;
			set
			{
				if (PhysicsWorld != null)
				{
					if (value != new TVector2f() || value != Gravity)
					{
						foreach (var collisionObject in PhysicsWorld.CollisionObjectArray)
						{
							collisionObject.Activate();
						}
					}
					PhysicsWorld.Gravity = value;
					return;
				}
				gravity = value;
			}
		}

		/// <summary>
		/// Restrict movement to given axis. Default is X and Y.
		/// </summary>
		public static Vector3 AllowedMovementAxis { get; set; } = new Vector3(1, 1, 0);
		/// <summary>
		/// Restrict rotation to given axis. Default is Z.
		/// </summary>
		public static Vector3 AllowedRotationAxis { get; set; } = new Vector3(0, 0, 0);

		public BulletPhysicsEngine(TVector2f gravity)
		{
			Gravity = gravity;
		}

		public BulletPhysicsEngine()
		{
			Gravity = new TVector2f(0.0f, 9.81f);
		}

		public void InitPhysicsEngine()
		{
			CollisionConfig = new DefaultCollisionConfiguration();
			CollisionDispatcher = new CollisionDispatcher(CollisionConfig);
			Broadphase = new DbvtBroadphase();
			PhysicsWorld = new DiscreteDynamicsWorld(CollisionDispatcher, Broadphase, null, CollisionConfig)
			{
				Gravity = Gravity
			};
		}

		public void PhysicsTick(float deltaTime)
		{
			PhysicsWorld.StepSimulation(deltaTime);

			// Update ActorComponent transforms to match CollisionObject transforms.
			foreach (var collisionObject in PhysicsWorld.CollisionObjectArray)
			{
				var actorComponent = collisionObject.UserObject as ActorComponent;
				if (actorComponent == null) continue;
				var body = collisionObject as RigidBody;
				var component = actorComponent;
				component.Position = collisionObject.WorldTransform.Origin;
				if (body != null)
				{
					component.Rotation = EngineMath.QuatToEulerDegrees(body.Orientation);
				}
			}
		}

		public void PhysicsTick(float deltaTime, int maxSubsteps, float timestep)
		{
			PhysicsWorld.StepSimulation(deltaTime, maxSubsteps, timestep);
		}

		public void ShutdownPhysicsEngine()
		{
			if (PhysicsWorld != null)
			{
				//remove/dispose constraints
				int i;
				for (i = PhysicsWorld.NumConstraints - 1; i >= 0; i--)
				{
					TypedConstraint constraint = PhysicsWorld.GetConstraint(i);
					PhysicsWorld.RemoveConstraint(constraint);
					constraint.Dispose();
				}

				//remove the rigidbodies from the dynamics world and delete them
				for (i = PhysicsWorld.NumCollisionObjects - 1; i >= 0; i--)
				{
					CollisionObject obj = PhysicsWorld.CollisionObjectArray[i];
					RigidBody body = obj as RigidBody;
					body?.MotionState?.Dispose();
					PhysicsWorld.RemoveCollisionObject(obj);
					obj.Dispose();
				}

				foreach (CollisionShape shape in CollisionShapes)
				{
					shape.Dispose();
				}
				CollisionShapes.Clear();

				PhysicsWorld.Dispose();
				Broadphase.Dispose();
				CollisionDispatcher.Dispose();
				CollisionConfig.Dispose();
				return;
			}

			Broadphase?.Dispose();
			CollisionDispatcher?.Dispose();
			CollisionConfig?.Dispose();
		}

		public void ResetPhysicsWorld()
		{
			ShutdownPhysicsEngine();
			InitPhysicsEngine();
		}

		public void ModifyPhysicsComponent(PhysicsComponent comp)
		{
			var rigid = (RigidBody) comp.CollisionObject;
			if (rigid != null)
			{
				ModifyRigidBody(rigid, comp.CollisionType, comp.CollisionResponseChannels);
				return;
			}
			ModifyCollisionObject(comp.CollisionObject, comp.CollisionType, comp.CollisionResponseChannels);
		}

		public void ModifyCollisionObject(CollisionObject obj, short collisionType, short collisionMask)
		{
			UnregisterCollisionObject(obj);
			RegisterCollisionObject(obj, collisionType, collisionMask);
		}

		public void ModifyRigidBody(RigidBody body, short collisionType, short collisionMask)
		{
			UnregisterRigidBody(body);
			RegisterRigidBody(body, collisionType, collisionMask);
		}

		public void RegisterRigidBody(RigidBody body, short collisionType, short collisionMask)
		{
			PhysicsWorld.AddRigidBody(body, collisionType, collisionMask);
		}

		public void UnregisterRigidBody(RigidBody body)
		{
			PhysicsWorld.RemoveRigidBody(body);
		}

		public void RegisterCollisionObject(CollisionObject obj, short collisionType, short collisionMask)
		{
			PhysicsWorld.AddCollisionObject(obj, collisionType, collisionMask);
		}

		public void UnregisterCollisionObject(CollisionObject obj)
		{
			PhysicsWorld.RemoveCollisionObject(obj);
		}

		public void ModifyGhostObject(GhostObject ghost, short collisionType, short collisionMask)
		{
			UnregisterCollisionObject(ghost);
			RegisterCollisionObject(ghost, collisionType, collisionMask);
		}

		public void RegisterGhostObject(GhostObject ghost, short collisionType, short collisionMask)
		{
			PhysicsWorld.AddCollisionObject(ghost, collisionType, collisionMask);
		}

		public void UnregisterGhostObject(GhostObject ghost)
		{
			PhysicsWorld.RemoveCollisionObject(ghost);
		}

		public void RegisterPhysicsComponent(PhysicsComponent comp)
		{
			var rigid = (RigidBody) comp.CollisionObject;
			if (rigid != null)
			{
				RegisterRigidBody(rigid, comp.CollisionType, comp.CollisionResponseChannels);
				return;
			}
			RegisterCollisionObject(comp.CollisionObject, comp.CollisionType, comp.CollisionResponseChannels);
		}

		public void UnregisterPhysicsComponent(PhysicsComponent comp)
		{
			var rigid = (RigidBody) comp.CollisionObject;
			if (rigid != null)
			{
				UnregisterRigidBody(rigid);
				return;
			}
			UnregisterCollisionObject(comp.CollisionObject);
		}

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