using System;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.Game;

namespace TestProject
{
	public class TestPlayerController : PlayerController
	{
		public override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			if (Input.IsKeyPressed(Keyboard.Key.A))
			{
				LevelReference.PhysicsEngine.Gravity += new Vector2f(-9.81f, 0.0f);
			}
			else if (Input.IsKeyPressed(Keyboard.Key.D))
			{
				LevelReference.PhysicsEngine.Gravity += new Vector2f(9.81f, 0.0f);
			}

			if (Input.IsKeyPressed(Keyboard.Key.W))
			{
				LevelReference.PhysicsEngine.Gravity += new Vector2f(0.0f, -9.81f);
			}
			else if (Input.IsKeyPressed(Keyboard.Key.S))
			{
				LevelReference.PhysicsEngine.Gravity += new Vector2f(0.0f, 9.81f);
			}

			Console.WriteLine("Gravity set to: " + LevelReference.PhysicsEngine.Gravity);
		}
	}
}