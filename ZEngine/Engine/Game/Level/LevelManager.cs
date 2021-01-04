using System.Diagnostics.CodeAnalysis;
using ZEngine.Engine.Core;
using ZEngine.Engine.Core.Messages;
using ZEngine.Engine.IO.Assets;
using ZEngine.Engine.Messaging;

namespace ZEngine.Engine.Game.Level
{
    internal class LevelManager : ILevelManager
    {
        public ulong LevelIDCounter { get; set; }
        public Level ActiveLevel { get; private set; }
        public bool CanTick { get; set; } = true;
        private IEngineMessageBus Bus { get; }
        private IAssetManager AssetManager { get; }

        internal LevelManager(IEngineMessageBus bus, IAssetManager assetManager)
        {
            Bus = bus;
            Bus.Subscribe<EngineFocusChangeMessage>(OnFocusChanged);
            Bus.Subscribe<EngineShutdownMessage>(OnEngineShutdown);
            AssetManager = assetManager;
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
            if(CanTick)
                ActiveLevel?.Tick(deltaTime);
        }

        /// <summary>
        /// Loads the given level.
        /// </summary>
        /// <param name="level"></param>
        public void LoadLevel([NotNull]Level level)
        {
            ActiveLevel?.OnGameEnd();
            ActiveLevel?.ShutdownLevel();

            ActiveLevel = level;

            level.LevelID = ++LevelIDCounter;
            level.Loaded = true;
            level.OnLevelLoad();
            level.Ticking = true;
        }

        /// <summary>
        /// Loads the given level.
        /// </summary>
        /// <param name="levelName"></param>
        /// <returns>Wether the level from the given string was loaded</returns>
        public bool LoadLevel([NotNull] string levelName)
        {
            if (string.IsNullOrWhiteSpace(levelName))
                return false;

            var lvl = AssetManager.LoadLevel<Level>(levelName);
            if (lvl == null)
                return false;

            LoadLevel(lvl);
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