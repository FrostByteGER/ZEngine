using System;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Game
{
    public class PlayerController : Transformable, ITickable, IGameInterface, IControllable
    {
	    
	    public string Name { get; set; } = "PlayerController";
        public uint ID { get; internal set; } = 0;
        public View PlayerCamera { get; set; } = new View(new TVector2f(), new TVector2f(400.0f));

		public Level LevelReference { get; set; } = null;

        public SpriteActor PlayerPawn { get; set; }

	    [JsonIgnore]
		public InputManager Input { get; set; }
	    public bool CanTick { get; set; } = true;

	    internal bool MarkedForInputRegistering { get; set; } = false;
	    public bool DisableInputWhenPaused { get; set; } = false;

		private bool _isActive = false;

	    [JsonIgnore]
		public bool IsActive
	    {
		    get => _isActive;
			set
			{
				
				if (!value && LevelReference != null && LevelReference.LevelLoaded)
				{
					UnregisterInput();
				}
				else if (!_isActive && LevelReference != null && LevelReference.LevelLoaded)
				{
					RegisterInput();
				}

				_isActive = value;
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
				OnKeyPressed, OnKeyDown, OnKeyReleased, OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved,
				OnTouchBegan, OnTouchEnded, OnTouchMoved);
		}

		public virtual void UnregisterInput()
		{
			Input = LevelReference.EngineReference.InputManager;
			Input.UnregisterInput(OnMouseButtonPressed, OnMouseButtonReleased, OnMouseMoved, OnMouseScrolled,
				OnKeyPressed, OnKeyDown, OnKeyReleased, OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved,
				OnTouchBegan, OnTouchEnded, OnTouchMoved);
		}

		public virtual void OnMouseButtonPressed(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Button Pressed: " + mouseButtonEventArgs.Button + " at X: " + mouseButtonEventArgs.X + " Y: " + mouseButtonEventArgs.Y);
		}

	    public virtual void OnMouseButtonReleased(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Button Released: " + mouseButtonEventArgs.Button + " at X: " + mouseButtonEventArgs.X + " Y: " + mouseButtonEventArgs.Y);
		}

	    public virtual void OnMouseMoved(object sender, MouseMoveEventArgs mouseMoveEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Moved to X: " + mouseMoveEventArgs.X + " Y: " + mouseMoveEventArgs.Y);
		}

	    public virtual void OnMouseScrolled(object sender, MouseWheelScrollEventArgs mouseWheelScrollEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Scrolled Wheel: " + mouseWheelScrollEventArgs.Wheel + " at X: " + mouseWheelScrollEventArgs.X + " Y: " + mouseWheelScrollEventArgs.Y + " by Scroll Amount: " + mouseWheelScrollEventArgs.Delta);
		}

	    public virtual void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
        {
           //Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Keyboard Key Pressed: " + keyEventArgs.Code);
		}

		public virtual void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Keyboard Key Pressed: " + keyEventArgs.Code);
		}

		public virtual void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Keyboard Key Released: " + keyEventArgs.Code);
		}

	    public virtual void OnJoystickConnected(object sender, JoystickConnectEventArgs joystickConnectEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Joystick Connected: JoystickID: " + joystickConnectEventArgs.JoystickId);
		}

	    public virtual void OnJoystickDisconnected(object sender, JoystickConnectEventArgs joystickConnectEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Joystick Disconnected: JoystickID: " + joystickConnectEventArgs.JoystickId);
		}

	    public virtual void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			Console.WriteLine("PlayerController: " + Name + "-" + PlayerPawn.ActorID + " Input Event: Joystick Button Pressed: JoystickID: " + joystickButtonEventArgs.JoystickId + " Button: " + joystickButtonEventArgs.Button);
		}

	    public virtual void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Joystick Button Released: JoystickID: " + joystickButtonEventArgs.JoystickId + " Button: " + joystickButtonEventArgs.Button);
		}

	    public virtual void OnJoystickMoved(object sender, JoystickMoveEventArgs joystickMoveEventArgs)
		{
			Console.WriteLine("PlayerController: " + Name + "-" + PlayerPawn.ActorID + " Input Event: Joystick Moved: JoystickID: " + joystickMoveEventArgs.JoystickId + " Axis: " + joystickMoveEventArgs.Axis + " to Position: " + joystickMoveEventArgs.Position);
		}

	    public virtual void OnTouchBegan(object sender, TouchEventArgs touchEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Touch Pressed: Finger: " + touchEventArgs.Finger + " at X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}

	    public virtual void OnTouchEnded(object sender, TouchEventArgs touchEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Touch Released: Finger: " + touchEventArgs.Finger + " at X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}

	    public virtual void OnTouchMoved(object sender, TouchEventArgs touchEventArgs)
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
		    if (DisableInputWhenPaused) IsActive = false;
	    }

	    public virtual void OnGameResume()
	    {
		    CanTick = true;
		    if (DisableInputWhenPaused) IsActive = true;
		}

	    public virtual void OnGameEnd()
	    {
		    IsActive = false;
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