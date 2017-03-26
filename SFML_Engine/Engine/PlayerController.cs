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
            PlayerPawn.Position = new Vector2f(Position.X + 10.0f, Position.Y); //TODO
        }

        public void Tick(float deltaTime)
        {
            if (PlayerPawn != null)
            {
                
            }
        }
    }
}