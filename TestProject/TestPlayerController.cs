using System;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Game;

namespace TestProject
{
	public class TestPlayerController : PlayerController
	{
		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			var gravity = LevelReference.EngineReference.PhysicsEngine.Gravity;
			if (Input.APressed)
			{
				LevelReference.EngineReference.PhysicsEngine.Gravity += new Vector2f(-9.81f, 0.0f);
			}else if (Input.DPressed)
			{
				LevelReference.EngineReference.PhysicsEngine.Gravity += new Vector2f(9.81f, 0.0f);
			}

			if (Input.WPressed)
			{
				LevelReference.EngineReference.PhysicsEngine.Gravity += new Vector2f(0.0f, -9.81f);
			}
			else if (Input.SPressed)
			{
				LevelReference.EngineReference.PhysicsEngine.Gravity += new Vector2f(0.0f, 9.81f);
			}

			Console.WriteLine("Gravity set to: " + LevelReference.EngineReference.PhysicsEngine.Gravity);
		}
	}
}