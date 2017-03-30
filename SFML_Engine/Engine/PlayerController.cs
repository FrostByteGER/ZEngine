using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Engine.Engine
{
    public class PlayerController : Transformable, ITickable
    {
        public string Name { get; set; } = "PlayerController";
        public uint ID { get; internal set; } = 0;
        public View PlayerCamera { get; set; } = new View();

        public bool ReceiveInput { get; set; } = true;

        public SpriteActor PlayerPawn { get; set; }

        public PlayerController()
        {
        }

        public PlayerController(SpriteActor playerPawn)
        {
            PlayerPawn = playerPawn;
        }

        public void RegisterInput(ref RenderWindow engineWindow)
        {
            engineWindow.KeyPressed += OnKeyPressed;
            engineWindow.KeyReleased += OnKeyReleased;
        }

        private void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
        {
            Console.WriteLine("PlayerController: " + Name + "-" +  ID + " Input Event: " + keyEventArgs.Code + " released!");
        }

        private void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
        {
            Console.WriteLine("PlayerController: " + Name + "-" + ID + " Input Event: " + keyEventArgs.Code + " pressed!");


            if (keyEventArgs.Code == Keyboard.Key.A)
            {
                PlayerPawn.Position += new Vector2f(-10.0f, 0.0f);

            }

            if (keyEventArgs.Code == Keyboard.Key.D)
            {
                PlayerPawn.Position += new Vector2f(10.0f, 0.0f);

            }

            if (keyEventArgs.Code == Keyboard.Key.W)
            {
                PlayerPawn.Position += new Vector2f(0.0f, -10.0f);

            }

            if (keyEventArgs.Code == Keyboard.Key.S)
            {
                PlayerPawn.Position += new Vector2f(0.0f, 10.0f);

            }

            if (keyEventArgs.Code == Keyboard.Key.Q)
            {
                PlayerPawn.Rotation -= 10.0f;

            }

            if (keyEventArgs.Code == Keyboard.Key.E)
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