using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Game
{
    public class PlayerController : Transformable, ITickable, IGameInterface
    {
	    
	    public string Name { get; set; } = "PlayerController";
        public uint ID { get; internal set; } = 0;
        public View PlayerCamera { get; set; } = new View(new TVector2f(), new TVector2f(400.0f));

		public Level LevelReference { get; set; } = null;

        public SpriteActor PlayerPawn { get; set; }

		public InputManager Input { get; set; }
	    public bool CanTick { get; set; } = true;

		private bool _isActive = true;
		public bool IsActive
	    {
		    get => _isActive;
			set
			{
				_isActive = value;
				if (value && LevelReference != null)
				{
					RegisterInput();
				}
				else if(!value && LevelReference != null)
				{
					UnregisterInput();
				}
			}
	    }

	    public PlayerController()
        {
        }

        public PlayerController(SpriteActor playerPawn)
        {
            PlayerPawn = playerPawn;
        }

        public virtual void RegisterInput()
        {
	        Input = LevelReference.EngineReference.InputManager;
			Input.RegisterInput(OnMouseButtonPressed, OnMouseButtonReleased, OnMouseMoved, OnMouseScrolled, 
				OnKeyPressed, OnKeyReleased, OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved,
				OnTouchBegan, OnTouchEnded, OnTouchMoved);
		}

		public virtual void UnregisterInput()
		{
			Input = LevelReference.EngineReference.InputManager;
			Input.UnregisterInput(OnMouseButtonPressed, OnMouseButtonReleased, OnMouseMoved, OnMouseScrolled,
				OnKeyPressed, OnKeyReleased, OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved,
				OnTouchBegan, OnTouchEnded, OnTouchMoved);
		}

		protected virtual void OnMouseButtonPressed(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Button Pressed: " + mouseButtonEventArgs.Button + " at X: " + mouseButtonEventArgs.X + " Y: " + mouseButtonEventArgs.Y);
		}

		protected virtual void OnMouseButtonReleased(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Button Released: " + mouseButtonEventArgs.Button + " at X: " + mouseButtonEventArgs.X + " Y: " + mouseButtonEventArgs.Y);
		}

		protected virtual void OnMouseMoved(object sender, MouseMoveEventArgs mouseMoveEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Moved to X: " + mouseMoveEventArgs.X + " Y: " + mouseMoveEventArgs.Y);
		}

		protected virtual void OnMouseScrolled(object sender, MouseWheelScrollEventArgs mouseWheelScrollEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Scrolled Wheel: " + mouseWheelScrollEventArgs.Wheel + " at X: " + mouseWheelScrollEventArgs.X + " Y: " + mouseWheelScrollEventArgs.Y + " by Scroll Amount: " + mouseWheelScrollEventArgs.Delta);
		}

		protected virtual void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
        {
           //Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Keyboard Key Pressed: " + keyEventArgs.Code);
		}

		protected virtual void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Keyboard Key Released: " + keyEventArgs.Code);
		}

		protected virtual void OnJoystickConnected(object sender, JoystickConnectEventArgs joystickConnectEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Joystick Connected: JoystickID: " + joystickConnectEventArgs.JoystickId);
		}

		protected virtual void OnJoystickDisconnected(object sender, JoystickConnectEventArgs joystickConnectEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Joystick Disconnected: JoystickID: " + joystickConnectEventArgs.JoystickId);
		}

		protected virtual void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			Console.WriteLine("PlayerController: " + Name + "-" + PlayerPawn.ActorID + " Input Event: Joystick Button Pressed: JoystickID: " + joystickButtonEventArgs.JoystickId + " Button: " + joystickButtonEventArgs.Button);
		}

		protected virtual void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Joystick Button Released: JoystickID: " + joystickButtonEventArgs.JoystickId + " Button: " + joystickButtonEventArgs.Button);
		}

		protected virtual void OnJoystickMoved(object sender, JoystickMoveEventArgs joystickMoveEventArgs)
		{
			Console.WriteLine("PlayerController: " + Name + "-" + PlayerPawn.ActorID + " Input Event: Joystick Moved: JoystickID: " + joystickMoveEventArgs.JoystickId + " Axis: " + joystickMoveEventArgs.Axis + " to Position: " + joystickMoveEventArgs.Position);
		}

		protected virtual void OnTouchBegan(object sender, TouchEventArgs touchEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Touch Pressed: Finger: " + touchEventArgs.Finger + " at X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}

		protected virtual void OnTouchEnded(object sender, TouchEventArgs touchEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Touch Released: Finger: " + touchEventArgs.Finger + " at X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}

		protected virtual void OnTouchMoved(object sender, TouchEventArgs touchEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Touch Moved: Finger: " + touchEventArgs.Finger + " to X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}

		public virtual void Tick(float deltaTime)
        {
            if (PlayerPawn != null)
            {
                
            }
        }

	    public virtual void OnGameStart()
	    {
		    
	    }

	    public virtual void OnGamePause()
	    {
		    CanTick = false;
	    }

	    public virtual void OnGameResume()
	    {
		    CanTick = true;
	    }

	    public virtual void OnGameEnd()
	    {
		    
	    }

	    public void SetCameraSize(float x, float y)
	    {
		    PlayerCamera.Size = new Vector2f(x, y);
	    }

		public void SetCameraSize(float size)
		{
			PlayerCamera.Size = new Vector2f(size, size);
		}

		public void SetCameraSize(TVector2f size)
		{
			PlayerCamera.Size = size;
		}
	}
}