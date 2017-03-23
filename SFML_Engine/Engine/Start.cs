namespace SFML_Engine.Engine
{
    class Start
    {
        public static void Main(string[] args)
        {
            Engine engine = new Engine(800, 600, "Engine");
            var Level = new Level();
            var actor = new SpriteActor();
            Level.RegisterActor(actor);
            engine.RegisterLevel(ref Level);
            engine.StartEngine();
        }
    }
}
