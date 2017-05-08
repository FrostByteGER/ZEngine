using System;
using BulletSharp;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;
using VelcroPhysics.Utils;
using CollisionShape = BulletSharp.CollisionShape;
using Matrix = BulletSharp.Matrix;
using RectangleShape = SFML_Engine.Engine.SFML.Graphics.RectangleShape;
using Vector3 = BulletSharp.Vector3;

namespace BulletTest
{
	public class BulletTest
	{
		public static EngineClock Clock = new EngineClock();
		public static float FrameDelta { get; set; }
		public static float FramesPerSecond { get; set; }
		public static float FrameAccumulator;
		public static void Main(string[] args)
		{

			Engine test = Engine.Instance;


			// Full extent, no half extent!
			var groundSprite = new RectangleShape(new Vector2f(20, 20));
			groundSprite.FillColor = Color.White;
			groundSprite.Origin = groundSprite.Size / 2.0f;

			// Full extent, no half extent!
			var boxSprite = new RectangleShape(new Vector2f(10, 10));
			boxSprite.FillColor = Color.Red;
			boxSprite.Origin = boxSprite.Size / 2.0f;

			var renderwindow = new RenderWindow(new VideoMode(800, 600), "Physics Test");
			var view = new View(new Vector2f(0, 0), new Vector2f(500, -500));
			view.Viewport = new FloatRect(0, 0, 1, 1);

			// Velcro Physics Test
			/*
			World velcroWorld = new World(-Vector2.UnitY * 9.81f);

			Body testBody = BodyFactory.CreateRectangle(velcroWorld, ConvertUnits.ToSimUnits(480), ConvertUnits.ToSimUnits(50), 10.0f, ConvertUnits.ToSimUnits(0, 2000), 0.0f, BodyType.Dynamic);
			Body floor = BodyFactory.CreateRectangle(velcroWorld, ConvertUnits.ToSimUnits(480), ConvertUnits.ToSimUnits(50), 10.0f, ConvertUnits.ToSimUnits(0,-2000), 0.0f, BodyType.Static);
			floor.Friction = 0.5f;
			floor.Restitution = 0.5f;

			while (false)
			{
				FrameDelta = Clock.GetFrameDelta();
				FrameAccumulator += FrameDelta;
				if (FrameAccumulator >= 1.0f)
				{
					FramesPerSecond = Clock.FrameCount / FrameAccumulator;
					Console.Clear();
					Console.WriteLine(FramesPerSecond + " | " + Clock.RenderAverage + " | " + Clock.PhysicsAverage);
					FrameAccumulator = 0.0f;
					Clock.Reset();
				}

				if (velcroWorld != null)
				{
					Clock.StartPhysics();
					velcroWorld.Step(FrameDelta);
					Clock.StopPhysics();
				}
				Console.WriteLine(testBody.Position + " | " + floor.Position);
			}
			*/


			// Bullet Physics Test
			CollisionConfiguration conf = new DefaultCollisionConfiguration();
			CollisionDispatcher dispatcher = new CollisionDispatcher(conf);
			BroadphaseInterface broadphase = new DbvtBroadphase();

			DynamicsWorld world = new DiscreteDynamicsWorld(dispatcher, broadphase, null, conf);
			world.Gravity = new Vector3(0.0f, -9.81f, 0.0f);
			Box2DShape ground = new Box2DShape(10,10,10);
			RigidBody groundBody = LocalCreateRigidBody(0, Matrix.Identity, ground);
			groundBody.Restitution = 1.0f;
			world.AddRigidBody(groundBody);
			groundBody.UserObject = "Ground";

			const float mass = 1.0f;

			Box2DShape box = new Box2DShape(5);
			Vector3 localInertia = box.CalculateLocalInertia(mass);

			RigidBodyConstructionInfo info = new RigidBodyConstructionInfo(mass,null, box,localInertia);
			Matrix t = Matrix.Translation(0, 200, 0);
			info.MotionState = new DefaultMotionState(t);
			RigidBody body = new RigidBody(info);
			//body.Translate(new Vector3(0,20,0));
			world.AddRigidBody(body);
			info.Dispose();
			body.Restitution = 1.0f;
			body.LinearFactor = new Vector3(1, 1, 0);
			body.AngularFactor = new Vector3(0, 0, 1);
			while (true)
			{
				FrameDelta = Clock.GetFrameDelta();
				FrameAccumulator += FrameDelta;
				if (FrameAccumulator >= 1.0f)
				{
					FramesPerSecond = Clock.FrameCount / FrameAccumulator;

					FrameAccumulator = 0.0f;
					Clock.Reset();
				}

				if (world != null)
				{
					Clock.StartPhysics();
					world.StepSimulation(FrameDelta);
					Clock.StopPhysics();
					foreach (var rbody in world.CollisionObjectArray)
					{
						//rbody.WorldTransform.Origin;
					}
					groundSprite.Position = Vec3ToVec2f(groundBody.WorldTransform.Origin);
					groundSprite.Rotation = QuatToEulerDegrees(groundBody.Orientation).Z;
					boxSprite.Position = Vec3ToVec2f(body.WorldTransform.Origin);
					boxSprite.Rotation = QuatToEulerDegrees(body.Orientation).Z;
				}
				Clock.StartRender();
				renderwindow.Clear();
				renderwindow.DispatchEvents();
				renderwindow.SetView(view);
				renderwindow.Draw(groundSprite);
				renderwindow.Draw(boxSprite);
				Clock.StopRender();
				renderwindow.Display();
				Console.WriteLine("Ground Position: " + groundBody.WorldTransform.Origin + " Box Position: " + body.WorldTransform.Origin + " | " + QuatToEulerDegrees(body.Orientation));
				//Thread.Sleep(50);
			}
		}

		public static float RadiansToDegrees(float radAngle)
		{
			return (float) (radAngle * (180.0 / Math.PI));
		}

		public static Vector3 RadiansToDegrees(Vector3 radAngles)
		{
			float x = (float) (radAngles.X * (180.0 / Math.PI));
			float y = (float) (radAngles.Y * (180.0 / Math.PI));
			float z = (float) (radAngles.Z * (180.0 / Math.PI));
			return new Vector3(x, y, z);
		}

		public static Vector3 RadiansToDegrees(float radX, float radY, float radZ)
		{
			float x = (float)(radX * (180.0 / Math.PI));
			float y = (float)(radY * (180.0 / Math.PI));
			float z = (float)(radZ * (180.0 / Math.PI));
			return new Vector3(x, y, z);
		}

		public static Vector2f Vec3ToVec2f(Vector3 source)
		{
			return new Vector2f(source.X, source.Y);
		}

		public static Vector3 Vec2fToVec3(Vector2f source)
		{
			return new Vector3(source.X, source.Y, 0.0f);
		}

		public static Vector3 QuatToEulerDegrees(Quaternion quat)
		{
			double w = quat.W;
			double x = quat.X;
			double y = quat.Y;
			double z = quat.Z;
			double sqw = w * w;
			double sqx = x * x;
			double sqy = y * y;
			double sqz = z * z;
			float ez = (float) Math.Atan2(2.0 * (x* y + z* w),sqx - sqy - sqz + sqw);
			float ex = (float) Math.Atan2(2.0 * (y* z + x* w),-sqx - sqy + sqz + sqw);
			float ey = (float) Math.Asin(-2.0 * (x* z - y* w));
			return RadiansToDegrees(ex, ey, ez);
		}

		public static Vector3 QuatToEulerRadians(Quaternion quat)
		{
			double w = quat.W;
			double x = quat.X;
			double y = quat.Y;
			double z = quat.Z;
			double sqw = w * w;
			double sqx = x * x;
			double sqy = y * y;
			double sqz = z * z;
			float ez = (float)Math.Atan2(2.0 * (x * y + z * w), sqx - sqy - sqz + sqw);
			float ex = (float)Math.Atan2(2.0 * (y * z + x * w), -sqx - sqy + sqz + sqw);
			float ey = (float)Math.Asin(-2.0 * (x * z - y * w));
			return new Vector3(ex, ey, ez);
		}

		public static RigidBody LocalCreateRigidBody(float mass, Matrix startTransform, CollisionShape shape)
		{
			//rigidbody is dynamic if and only if mass is non zero, otherwise static
			bool isDynamic = (mass != 0.0f);

			Vector3 localInertia = Vector3.Zero;
			if (isDynamic)
				shape.CalculateLocalInertia(mass, out localInertia);

			//using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
			DefaultMotionState myMotionState = new DefaultMotionState(startTransform);

			RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
			RigidBody body = new RigidBody(rbInfo);
			rbInfo.Dispose();
			return body;
		}
	}
}
