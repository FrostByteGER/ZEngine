using ZEngine.Engine.Core;

namespace Sandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var engine = Engine.Instance;
            engine.EngineWindowWidth = 800;
            engine.EngineWindowHeight = 600;
            engine.Bootstrapper = new SandboxBootstrap();
            engine.GameInfo = new SandboxGameInfo();
            /*
            var level = new Level();
            var actor = new Actor();
            var actor2 = new Actor();
            var pc = new PlayerController();
            var pc2 = new PlayerController();
            level.RegisterActor(actor);
            level.RegisterActor(actor2);
            level.RegisterPlayer(pc);
            level.RegisterPlayer(pc2);
            engine.LoadLevel(level);
            */
            engine.StartEngine(args);

		}
    }
}
