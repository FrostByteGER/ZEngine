using System;
using Microsoft.Xna.Framework;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;
using VelcroPhysics.Utils;

namespace BulletTest
{
	public class BulletTest
	{
		public static EngineClock clock = new EngineClock();
		public static float FrameDelta { get; set; }
		public static float FramesPerSecond { get; set; }
		public static float _frameAccumulator;
		public static void Main(string[] args)
		{

			Engine test = Engine.Instance;

			World velcroWorld = new World(-Vector2.UnitY * 9.81f);

			Body testBody = BodyFactory.CreateRectangle(velcroWorld, ConvertUnits.ToSimUnits(480), ConvertUnits.ToSimUnits(50), 10.0f, ConvertUnits.ToSimUnits(0, 2000), 0.0f, BodyType.Dynamic);
			Body floor = BodyFactory.CreateRectangle(velcroWorld, ConvertUnits.ToSimUnits(480), ConvertUnits.ToSimUnits(50), 10.0f, ConvertUnits.ToSimUnits(0,-2000), 0.0f, BodyType.Static);
			floor.Friction = 0.5f;
			floor.Restitution = 0.5f;

			while (true)
			{
				FrameDelta = clock.GetFrameDelta();
				_frameAccumulator += FrameDelta;
				if (_frameAccumulator >= 1.0f)
				{
					FramesPerSecond = clock.FrameCount / _frameAccumulator;
					Console.Clear();
					Console.WriteLine(FramesPerSecond + " | " + clock.RenderAverage + " | " + clock.PhysicsAverage);
					_frameAccumulator = 0.0f;
					clock.Reset();
				}

				if (velcroWorld != null)
				{
					clock.StartPhysics();
					velcroWorld.Step(FrameDelta);
					clock.StopPhysics();
				}
				Console.WriteLine(testBody.Position + " | " + floor.Position);
			}
			/*
			List<CollisionShape> shapes = new List<CollisionShape>();
			
			CollisionConfiguration conf = new DefaultCollisionConfiguration();
			CollisionDispatcher dispatcher = new CollisionDispatcher(conf);
			BroadphaseInterface broadphase = new DbvtBroadphase();

			DynamicsWorld world = new DiscreteDynamicsWorld(dispatcher, broadphase, null, conf);
			world.Gravity = new Vector3(0.0f, -9.81f, 0.0f);
			BoxShape ground = new BoxShape(50,1,50);
			shapes.Add(ground);
			ground.UserObject = "Ground";

			const float mass = 1.0f;

			BoxShape box = new BoxShape(1);
			Vector3 localInertia = box.CalculateLocalInertia(mass);

			RigidBodyConstructionInfo info = new RigidBodyConstructionInfo(mass,null, box,localInertia);
			Matrix t = Matrix.Translation(10, 10, 10);
			info.MotionState = new DefaultMotionState(t);
			RigidBody body = new RigidBody(info);
			body.Translate(new Vector3(0,20,0));
			world.AddRigidBody(body);
			info.Dispose();
			while (true)
			{
				FrameDelta = clock.GetFrameDelta();
				_frameAccumulator += FrameDelta;
				if (_frameAccumulator >= 1.0f)
				{
					FramesPerSecond = clock.FrameCount / _frameAccumulator;

					_frameAccumulator = 0.0f;
					clock.Reset();
				}

				if (world != null)
				{
					clock.StartPhysics();
					world.StepSimulation(FrameDelta);
					clock.StopPhysics();
				}
			}*/
		}
	}
}
