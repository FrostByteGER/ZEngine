using System;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;
using VelcroPhysics.Utils;

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
			
			World velcroWorld = new World(-Vector2.UnitY * 9.81f);

			Body testBody = BodyFactory.CreateRectangle(velcroWorld, ConvertUnits.ToSimUnits(480), ConvertUnits.ToSimUnits(50), 10.0f, ConvertUnits.ToSimUnits(0, 2000), 0.0f, BodyType.Dynamic);
			Body floor = BodyFactory.CreateRectangle(velcroWorld, ConvertUnits.ToSimUnits(480), ConvertUnits.ToSimUnits(50), 10.0f, ConvertUnits.ToSimUnits(0,-2000), 0.0f, BodyType.Static);
			floor.Friction = 0.5f;
			floor.Restitution = 0.5f;

			while (true)
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
			
		}
	}
}
