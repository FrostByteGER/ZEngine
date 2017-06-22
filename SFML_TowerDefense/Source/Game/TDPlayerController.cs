using SFML.Window;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;

namespace SFML_TowerDefense.Source.Game
{
	public class TDPlayerController : PlayerController
	{

		private float DeltaTime { get; set; } = 0;

		public override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyPressed(sender, keyEventArgs);
		}

		public override void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
		{
			if (Input.IsKeyDown(Keyboard.Key.W))
			{
				PlayerCamera.Move(new TVector2f(0, -100 * DeltaTime));
			}
			if (Input.IsKeyDown(Keyboard.Key.S))
			{
				PlayerCamera.Move(new TVector2f(0, 100 * DeltaTime));
			}
			if (Input.IsKeyDown(Keyboard.Key.A))
			{
				PlayerCamera.Move(new TVector2f(-100 * DeltaTime, 0));
			}
			if (Input.IsKeyDown(Keyboard.Key.D))
			{
				PlayerCamera.Move(new TVector2f(100 * DeltaTime, 0));
			}
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			DeltaTime = deltaTime;
		}
	}
}