using System;
using SFML.Graphics;
using SFML.Graphics.Engine;
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

		public InputManager Input { get; set; }

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

        protected virtual void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
        {
           Console.WriteLine("PlayerController: " + Name + "-" + ID + " Input Event: " + keyEventArgs.Code + " pressed!");
        }

		protected virtual void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			Console.WriteLine("PlayerController: " + Name + "-" + ID + " Input Event: " + keyEventArgs.Code + " released!");
		}

		public void Tick(float deltaTime)
        {
            if (PlayerPawn != null)
            {
                
            }
        }
    }
}