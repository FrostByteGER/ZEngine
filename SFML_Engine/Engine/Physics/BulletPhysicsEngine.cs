using System;
using System.Collections.Generic;
using BulletSharp;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public class BulletPhysicsEngine
	{

		public CollisionConfiguration CollisionConfig { get; private set; }
		public CollisionDispatcher CollisionDispatcher { get; private set; }
		public BroadphaseInterface Broadphase { get; private set; }
		public DynamicsWorld PhysicsWorld { get; private set; }
		public List<CollisionShape> CollisionShapes { get; } = new List<CollisionShape>();
		private Vector2f gravity;

		public Vector2f Gravity
		{
			get => PhysicsWorld != null ? EngineMath.Vec3ToVec2f(PhysicsWorld.Gravity) : gravity;
			set
			{
				if (PhysicsWorld != null)
				{
					PhysicsWorld.Gravity = EngineMath.Vec2fToVec3(value);
					return;
				}
				gravity = value;
			}
		}

		public static Vector3 AllowedMovementAxis { get; set; } = new Vector3(1, 1, 0);
		public static Vector3 AllowedRotationAxis { get; set; } = new Vector3(0, 0, 1);

		public BulletPhysicsEngine(Vector2f gravity)
		{
			Gravity = gravity;
		}

		public BulletPhysicsEngine()
		{
			Gravity = new Vector2f(0.0f, -9.81f);
		}

		public void InitPhysicsEngine()
		{
			CollisionConfig = new DefaultCollisionConfiguration();
			CollisionDispatcher = new CollisionDispatcher(CollisionConfig);
			Broadphase = new DbvtBroadphase();
			PhysicsWorld = new DiscreteDynamicsWorld(CollisionDispatcher, Broadphase, null, CollisionConfig)
			{
				Gravity = EngineMath.Vec2fToVec3(Gravity)
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
				component.Position = EngineMath.Vec3ToVec2f(collisionObject.WorldTransform.Origin);
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

		public void AddRigidBody(RigidBody body)
		{
			PhysicsWorld.AddRigidBody(body);
		}

		public void RemoveRigidBody(RigidBody body)
		{
			PhysicsWorld.RemoveRigidBody(body);
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Matrix startTransform,
			BulletSharp.CollisionShape shape)
		{
			//rigidbody is dynamic if and only if mass is non zero, otherwise static
			bool isDynamic = Math.Abs(mass) > 0.00001f;
			var localInertia = Vector3.Zero;
			if (isDynamic) shape.CalculateLocalInertia(mass, out localInertia);

			//using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
			var myMotionState = new DefaultMotionState(startTransform);

			var rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
			var body = new RigidBody(rbInfo)
			{
				UserObject = parent

			};
			body.LinearFactor = AllowedMovementAxis;
			body.AngularFactor = AllowedRotationAxis;
			rbInfo.Dispose();
			return body;
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Vector2f position, float angle,
			CollisionShape shape)
		{
			return ConstructRigidBody(parent, mass, position, angle, new Vector2f(1.0f, 1.0f), shape);
		}

		public static RigidBody ConstructRigidBody(object parent, float mass, Vector2f position, float angle, Vector2f scale,
			CollisionShape shape)
		{
			Matrix t = Matrix.Translation(position.X, position.Y, 0.0f) * Matrix.RotationZ(angle) *
			           Matrix.Scaling(scale.X, scale.Y, 1.0f);
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


	}
}