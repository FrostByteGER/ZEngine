using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.IO;

namespace SFML_Engine.Engine
{
    public class PlayerController : Transformable, ITickable
    {
        public string Name { get; set; } = "PlayerController";
        public uint ID { get; internal set; } = 0;
        public View PlayerCamera { get; set; } = new View();

        public bool ReceiveInput { get; set; } = true;

        public SpriteActor PlayerPawn { get; set; }

		private InputManager Input { get; set; }

        public PlayerController()
        {
        }

        public PlayerController(SpriteActor playerPawn)
        {
            PlayerPawn = playerPawn;
        }

        public void RegisterInput(Engine engine)
        {
	        Input = engine.InputManager;
            engine.InputManager.KeyPressed += OnKeyPressed;
			engine.InputManager.KeyReleased += OnKeyReleased;
        }

        private void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
        {
            Console.WriteLine("PlayerController: " + Name + "-" +  ID + " Input Event: " + keyEventArgs.Code + " released!");
        }

        private void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
        {
           Console.WriteLine("PlayerController: " + Name + "-" + ID + " Input Event: " + keyEventArgs.Code + " pressed!");

            if (Input.APressed)
            {
                PlayerPawn.Position += new Vector2f(-10.0f, 0.0f);

            }

            if (Input.DPressed)
            {
                PlayerPawn.Position += new Vector2f(10.0f, 0.0f);

            }

            if (Input.WPressed)
            {
                PlayerPawn.Position += new Vector2f(0.0f, -10.0f);

            }

            if (Input.SPressed)
            {
                PlayerPawn.Position += new Vector2f(0.0f, 10.0f);

            }

            if (Input.QPressed)
            {
                PlayerPawn.Rotation -= 10.0f;

            }

            if (Input.EPressed)
            {
                PlayerPawn.Rotation += 10.0f;

            }
        }

        public void Tick(float deltaTime)
        {
            if (PlayerPawn != null)
            {
                
            }
        }
    }
}