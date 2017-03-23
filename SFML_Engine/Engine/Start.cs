namespace SFML_Engine.Engine
{
    class Start
    {
        public static void Main(string[] args)
        {
            Engine engine = new Engine(800, 600, "Engine");
            engine.StartEngine();
        }
    }
}
