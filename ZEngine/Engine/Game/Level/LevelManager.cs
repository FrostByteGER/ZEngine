using System.Diagnostics.CodeAnalysis;
using ZEngine.Engine.Core;
using ZEngine.Engine.Core.Messages;
using ZEngine.Engine.Messaging;

namespace ZEngine.Engine.Game.Level
{
    internal class LevelManager : ILevelManager
    {
        public ulong LevelIDCounter { get; set; }
        public Level ActiveLevel { get; private set; }
        public bool CanTick { get; set; }
        private IEngineMessageBus Bus { get; }

        internal LevelManager(IEngineMessageBus bus)
        {
            Bus = bus;
            Bus.Subscribe<EngineFocusChangeMessage>(OnFocusChanged);
            Bus.Subscribe<EngineShutdownMessage>(OnEngineShutdown);
        }

        private void OnEngineShutdown(EngineShutdownMessage msg)
        {
            ActiveLevel?.OnGameEnd();
            ActiveLevel?.ShutdownLevel();
        }

        private void OnFocusChanged(EngineFocusChangeMessage msg)
        {
            if (msg.NewFocusState)
            {
                ActiveLevel?.OnGameResume();
            }
            else
            {
                ActiveLevel?.OnGamePause();
            }
        }

        public void Tick(float deltaTime)
        {
            ActiveLevel.Tick(deltaTime);
        }

        /// <summary>
        /// Loads the given level.
        /// </summary>
        /// <param name="level"></param>
        public void LoadLevel([NotNull]Level level)
        {
            ActiveLevel?.OnGameEnd();
            ActiveLevel?.ShutdownLevel();
            //AssetManager.ClearPools();

            ActiveLevel = level;

            level.LevelID = ++LevelIDCounter;
            level.LevelLoaded = true;
            level.OnLevelLoad();
            level.LevelTicking = true;
        }

        /// <summary>
        /// Loads the given level.
        /// </summary>
        /// <param name="level"></param>
        /// <returns>Wether the level from the given string was loaded</returns>
        public bool LoadLevel([NotNull] string levelName)
        {
            if (string.IsNullOrWhiteSpace(levelName))
                return false;


            return true;
        }

        public void Initialize()
        {
            
        }

        public void Deinitialize()
        {
            
        }
    }
}