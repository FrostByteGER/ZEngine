using System.Diagnostics.CodeAnalysis;
using ZEngine.Engine.Core.Messages;
using ZEngine.Engine.IO.Assets;
using ZEngine.Engine.Messaging;
namespace ZEngine.Engine.Game
{
    internal class LevelManager : ILevelManager
    {
        public Level ActiveLevel { get; private set; }
        public bool CanTick { get; set; } = true;
        private IMessageBus Bus { get; }
        private IAssetManager AssetManager { get; }
        
        internal LevelManager(IMessageBus bus, IAssetManager assetManager)
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
        
        public void LoadLevel([NotNull]Level level)
        {
            ActiveLevel?.OnGameEnd();
            ActiveLevel?.ShutdownLevel();
            ActiveLevel = level;
            level.Loaded = true;
            level.OnLevelLoad();
            level.CanTick = true;
        }

        public void InitializeService()
        {
            
        }
        
        public void DeinitializeService()
        {
            
        }
    }
}