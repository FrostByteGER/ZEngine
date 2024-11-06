using System.Diagnostics.CodeAnalysis;
using ZEngine.Engine.Core;
using ZEngine.Engine.Core.Messages;
using ZEngine.Engine.IO.Assets;
using ZEngine.Engine.Messaging;

namespace ZEngine.Engine.Game.Level
{
    internal class LevelManager_OLD : ILevelManager_OLD
    {
        public ulong LevelIDCounter { get; set; }
        public Level_OLD ActiveLevelOld { get; private set; }
        public bool CanTick { get; set; } = true;
        private IEngineMessageBus Bus { get; }
        private IAssetManager AssetManager { get; }

        internal LevelManager_OLD(IEngineMessageBus bus, IAssetManager assetManager)
        {
            Bus = bus;
            Bus.Subscribe<EngineFocusChangeMessage>(OnFocusChanged);
            Bus.Subscribe<EngineShutdownMessage>(OnEngineShutdown);
            AssetManager = assetManager;
        }

        private void OnEngineShutdown(EngineShutdownMessage msg)
        {
            ActiveLevelOld?.OnGameEnd();
            ActiveLevelOld?.ShutdownLevel();
        }

        private void OnFocusChanged(EngineFocusChangeMessage msg)
        {
            if (msg.NewFocusState)
            {
                ActiveLevelOld?.OnGameResume();
            }
            else
            {
                ActiveLevelOld?.OnGamePause();
            }
        }

        public void Tick(float deltaTime)
        {
            if(CanTick)
                ActiveLevelOld?.Tick(deltaTime);
        }

        /// <summary>
        /// Loads the given level.
        /// </summary>
        /// <param name="levelOld"></param>
        public void LoadLevel([NotNull]Level_OLD levelOld)
        {
            ActiveLevelOld?.OnGameEnd();
            ActiveLevelOld?.ShutdownLevel();

            ActiveLevelOld = levelOld;

            levelOld.LevelID = ++LevelIDCounter;
            levelOld.Loaded = true;
            levelOld.OnLevelLoad();
            levelOld.Ticking = true;
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

            var lvl = AssetManager.LoadLevel<Level_OLD>(levelName);
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