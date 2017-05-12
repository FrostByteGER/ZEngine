using System;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;

namespace TestProject
{
	public class TestPlayerController : PlayerController
	{
		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			var gravity = LevelReference.EngineReference.BulletPhysicsEngine.Gravity;
			if (Input.APressed)
			{
				LevelReference.EngineReference.BulletPhysicsEngine.Gravity = new Vector2f(-9.81f, gravity.Y);
			}else if (Input.DPressed)
			{
				LevelReference.EngineReference.BulletPhysicsEngine.Gravity = new Vector2f(9.81f, gravity.Y);
			}

			if (Input.WPressed)
			{
				LevelReference.EngineReference.BulletPhysicsEngine.Gravity = new Vector2f(gravity.X, -9.81f);
			}
			else if (Input.SPressed)
			{
				LevelReference.EngineReference.BulletPhysicsEngine.Gravity = new Vector2f(gravity.X, 9.81f);
			}

			Console.WriteLine("Gravity set to: " + LevelReference.EngineReference.BulletPhysicsEngine.Gravity);
		}
	}
}