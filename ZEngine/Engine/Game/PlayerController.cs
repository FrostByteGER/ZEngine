using System;
using System.Drawing;
using System.Numerics;
using Newtonsoft.Json;
using Silk.NET.Input.Common;
using ZEngine.Engine.IO;

namespace ZEngine.Engine.Game
{
    public class PlayerController : Transform, ITickable, IControllable
    {

	    public string Name { get; set; } = "PlayerController";
        public uint ID { get; internal set; } = 0;
        public View PlayerCamera { get; set; }

		public Level LevelReference { get; set; } = null;

        public Actor PlayerPawn { get; set; }

		//public JGUI Hud { get; set; }

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
					//UnregisterInput();
				}
				else if (!_isActive && LevelReference != null && LevelReference.LevelLoaded)
				{
					//RegisterInput();
				}

				_isActive = value;
			}
	    }

	    public PlayerController()
        {
        }

        public PlayerController(Actor playerPawn)
        {
            PlayerPawn = playerPawn;
        }

        protected internal virtual void Tick(float deltaTime)
        {
            if (PlayerPawn != null)
            {
                
            }
        }

        protected internal virtual void OnGameStart()
	    {

	    }

        protected internal virtual void OnGamePause()
	    {
		    CanTick = false;
		    if (DisableInputWhenPaused) IsActive = false;
	    }

        protected internal virtual void OnGameResume()
	    {
		    CanTick = true;
		    if (DisableInputWhenPaused) IsActive = true;
		}

        protected internal virtual void OnGameEnd()
	    {
		    IsActive = false;
	    }

        protected internal void SetCameraSize(float x, float y)
	    {
		    //PlayerCamera.Size = new Vector2(x, y);
	    }

        protected internal void SetCameraSize(float size)
		{
			//PlayerCamera.Size = new Vector2(size, size);
		}

        protected internal void SetCameraSize(Vector2 size)
		{
			//PlayerCamera.Size = size;
		}

		public void OnMouseButtonPressed(object sender, MouseButton button)
		{
			throw new NotImplementedException();
		}

		public void OnMouseButtonReleased(object sender, MouseButton button)
		{
			throw new NotImplementedException();
		}

		public void OnMouseButtonClicked(object sender, MouseButton button)
		{
			throw new NotImplementedException();
		}

		public void OnMouseButtonDoubleClicked(object sender, MouseButton button)
		{
			throw new NotImplementedException();
		}

		public void OnMouseMoved(object sender, PointF coords)
		{
			throw new NotImplementedException();
		}

		public void OnMouseScrolled(object sender, ScrollWheel scrollArgs)
		{
			throw new NotImplementedException();
		}

        public void OnMouseConnected()
        {
            throw new NotImplementedException();
        }

        public void OnMouseDisconnected()
        {
            throw new NotImplementedException();
        }

        public void OnKeyDown(object sender, Key key)
		{
			throw new NotImplementedException();
		}

		public void OnKeyReleased(object sender, Key key)
		{
			throw new NotImplementedException();
		}

		public void OnGamepadButtonPressed(object sender, Button button)
		{
			throw new NotImplementedException();
		}

		public void OnGamepadButtonReleased(object sender, Button button)
		{
			throw new NotImplementedException();
		}

		public void OnGamepadThumbstickMoved(object sender, Thumbstick stick)
		{
			throw new NotImplementedException();
		}

		public void OnGamepadTriggerMoved(object sender, Trigger trigger)
		{
			throw new NotImplementedException();
		}

        public void OnGamepadConnected()
        {
            throw new NotImplementedException();
        }

        public void OnGamepadDisconnected()
        {
            throw new NotImplementedException();
        }

        public void OnJoystickButtonPressed(object sender, Button button)
		{
			throw new NotImplementedException();
		}

		public void OnJoystickButtonReleased(object sender, Button button)
		{
			throw new NotImplementedException();
		}

		public void OnJoystickMoved(object sender, Axis axisArgs)
		{
			throw new NotImplementedException();
		}

		public void OnJoystickHatMoved(object sender, Hat hatArgs)
		{
			throw new NotImplementedException();
		}

        public void OnJoystickConnected()
        {
            throw new NotImplementedException();
        }

        public void OnJoystickDisconnected()
        {
            throw new NotImplementedException();
        }

        public void OnDeviceDisconnected()
		{
			throw new NotImplementedException();
		}

		public void OnDeviceConnected()
		{
			throw new NotImplementedException();
		}

        public void OnKeyDown(object sender, Key key, int code)
        {
            throw new NotImplementedException();
        }

        public void OnKeyReleased(object sender, Key key, int code)
        {
            throw new NotImplementedException();
        }

        public void OnKeyboardConnected()
        {
            throw new NotImplementedException();
        }

        public void OnKeyboardDisconnected()
        {
            throw new NotImplementedException();
        }
    }
}