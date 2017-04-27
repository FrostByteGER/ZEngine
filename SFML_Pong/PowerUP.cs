using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Utility;

namespace SFML_Pong
{
	public class PowerUp : SpriteActor
	{

		public float MaxSize { get; set; } = 1.5f;
		public override void IsOverlapping(Actor actor)
		{
			if (!MarkedForRemoval && actor.GetType() == typeof(PongBall))
			{
				var ball = (PongBall)actor;


				if (ball.Scale.X < MaxSize || ball.Scale.Y < MaxSize)
				{
					ball.ScaleActor(.1f, .1f);
				}
				else if(ball.Scale.X >= MaxSize || ball.Scale.Y >= MaxSize)
				{
					ball.ScaleActor(-.1f, -.1f);
				}
				else if(EngineMath.EngineRandom.NextDouble() > 0.5f)
				{
					ball.ScaleActor(.1f, .1f);
				}
				else
				{
					ball.ScaleActor(-.1f, -.1f);
				}
				Engine.Instance.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, this)));
			}
		}
	}
}
