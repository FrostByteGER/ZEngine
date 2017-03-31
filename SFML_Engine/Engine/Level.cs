using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.Window;

namespace SFML_Engine.Engine
{
    public class Level
    {

        public List<Transformable> Actors{ get; set; } = new List<Transformable>();

        public Engine Engine { get; set; }

        public GameMode GameMode { get; private set; } = new GameMode();

        internal bool LevelTicking { get; set; }

        public Level()
        {
        }


        internal void LevelTick(float deltaTime)
        {
            //Console.WriteLine("Level Tick!");
            foreach (var actor in Actors)
            {
                var tickableActor = actor as ITickable;
                tickableActor?.Tick(deltaTime);
            }
        }

        internal void LevelDraw(ref RenderWindow renderWindow)
        {
            foreach (var actor in Actors)
            {
                var drawableActor = actor as Drawable;
                if (drawableActor != null)
                {
                    renderWindow.Draw(drawableActor);

                }
            }
        }

        public void RegisterActor(Transformable actor)
        {
            Actors.Add(actor);

        }
    }
}
