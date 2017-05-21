using System;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.Core;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;

namespace VelcroTest
{
	public class VelcroTest
	{
		public static EngineClock Clock = new EngineClock();
		public static float FrameDelta { get; set; }
		public static float FramesPerSecond { get; set; }
		public static float FrameAccumulator;
		public static void Main(string[] args)
		{

			Engine test = Engine.Instance;


			// Full extent, no half extent!
			var groundSprite = new RectangleShape(new Vector2f(200, 20));
			groundSprite.FillColor = Color.White;
			groundSprite.Origin = groundSprite.Size / 2.0f;

			var groundSprite3 = new RectangleShape(new Vector2f(5, 5));
			groundSprite3.FillColor = Color.White;
			groundSprite3.Origin = groundSprite3.Size / 2.0f;
			groundSprite3.Position = new Vector2f(0, 100);

			// Full extent, no half extent!
			var boxSprite = new RectangleShape(new Vector2f(20, 20));
			boxSprite.FillColor = Color.Red;
			boxSprite.Origin = boxSprite.Size / 2.0f;

			// Full extent, no half extent!
			var triggerBoxSprite = new RectangleShape(new Vector2f(40, 40));
			triggerBoxSprite.FillColor = Color.Yellow;
			triggerBoxSprite.Origin = triggerBoxSprite.Size / 2.0f;

			var renderwindow = new RenderWindow(new VideoMode(800, 800), "Physics Test");
			var view = new View(new Vector2f(0, 0), new Vector2f(500, 500));
			view.Viewport = new FloatRect(0, 0, 1, 1);
			renderwindow.SetView(view);

			// Velcro Physics Test
			
			World velcroWorld = new World(Vector2.UnitY * 9.81f);

			Body testBody = BodyFactory.CreateRectangle(velcroWorld, 20, 20, 10.0f, new Vector2(105.0f, -140.0f), 0.0f, BodyType.Dynamic);
			testBody.UserData = boxSprite;
			Body triggerBody = BodyFactory.CreateRectangle(velcroWorld, 40, 40, 10.0f, new Vector2(105.0f, -60.0f), 0.0f, BodyType.Static);
			triggerBody.UserData = triggerBoxSprite;
			triggerBody.IsSensor = true;
			triggerBody.OnCollision += TestBody2_OnCollision;
			triggerBody.OnSeparation += TestBody2_OnSeparation;
			Body floor = BodyFactory.CreateRectangle(velcroWorld, 200, 20, 10.0f, new Vector2(), 0.0f, BodyType.Static);
			floor.OnCollision += Floor_OnCollision;
			floor.OnSeparation += Floor_OnSeparation;
			floor.UserData = groundSprite;
			floor.Friction = 0.5f;
			floor.Restitution = 0.0f;
			Console.WriteLine("Moving-Box ID: " + testBody.BodyId + " Trigger-Box ID: " + triggerBody.BodyId + " Floor ID: " + floor.BodyId);
			while (true)
			{
				FrameDelta = Clock.GetFrameDelta();
				FrameAccumulator += FrameDelta;
				if (FrameAccumulator >= 1.0f)
				{
					FramesPerSecond = Clock.FrameCount / FrameAccumulator;
					//Console.Clear();
					//Console.WriteLine(FramesPerSecond + " | " + Clock.RenderAverage + " | " + Clock.PhysicsAverage);
					FrameAccumulator = 0.0f;
					Clock.Reset();
				}

				if (velcroWorld != null)
				{
					Clock.StartPhysics();
					velcroWorld.Step(FrameDelta);
					Clock.StopPhysics();
					triggerBoxSprite.Position = new Vector2f(triggerBody.Position.X, triggerBody.Position.Y);
					triggerBoxSprite.Rotation = EngineMath.RadiansToDegrees(triggerBody.Rotation);
					boxSprite.Position = new Vector2f(testBody.Position.X, testBody.Position.Y);
					boxSprite.Rotation = EngineMath.RadiansToDegrees(testBody.Rotation);
					groundSprite.Position = new Vector2f(floor.Position.X, floor.Position.Y);
					groundSprite.Rotation = EngineMath.RadiansToDegrees(floor.Rotation);

					Clock.StartRender();
					renderwindow.Clear();
					renderwindow.DispatchEvents();
					renderwindow.Draw(triggerBoxSprite);
					renderwindow.Draw(groundSprite);
					renderwindow.Draw(boxSprite);
					renderwindow.Draw(groundSprite3);
					renderwindow.Display();
					Clock.StopRender();
				}
				//Console.WriteLine(testBody.Position + " | " + floor.Position);
			}
			
		}

		private static void Floor_OnSeparation(Fixture otherActor, Fixture self, VelcroPhysics.Collision.ContactSystem.Contact contact)
		{
			Console.WriteLine("ON COLLIDE END EVENT: " + self.Body.BodyId + " ENDED COLLISION WITH " + otherActor.Body.BodyId);
		}

		private static void Floor_OnCollision(Fixture otherActor, Fixture self, VelcroPhysics.Collision.ContactSystem.Contact contact)
		{
			Console.WriteLine("ON COLLIDE EVENT: " + self.Body.BodyId + " COLLIDED WITH " + otherActor.Body.BodyId);
		}

		private static void TestBody2_OnSeparation(Fixture otherActor, Fixture self, VelcroPhysics.Collision.ContactSystem.Contact contact)
		{
			Console.WriteLine("ON OVERLAP END EVENT: " + self.Body.BodyId + " ENDED OVERLAP WITH " + otherActor.Body.BodyId);
			((Shape)otherActor.Body.UserData).FillColor = Color.Yellow;
		}

		private static void TestBody2_OnCollision(Fixture otherActor, Fixture self, VelcroPhysics.Collision.ContactSystem.Contact contact)
		{
			Console.WriteLine("ON OVERLAP BEGIN EVENT " + self.Body.BodyId + " BEGAN OVERLAP WITH " + otherActor.Body.BodyId);
			((Shape)otherActor.Body.UserData).FillColor = Color.Green;
		}
	}
}
