using System;
using BulletSharp;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Physics;
using Sprite = SFML_Engine.Engine.SFML.Graphics.Sprite;

namespace SFML_Engine.Engine
{
    public class SpriteActor : Actor
    {

	    public bool SnapOriginToCenter { get; set; } = true;

	    public SpriteActor()
        {
	        if (SnapOriginToCenter)
	        {
				//Origin = new Vector2f(bounds.Width / 2.0f, bounds.Height / 2.0f);
	        }
        }

	    public SpriteActor(SpriteComponent spriteComp)
	    {
		    SetRootComponent(new CollisionComponent(BulletPhysicsEngine.ConstructRigidBody(null, 1.0f, Matrix.Identity, new BoxShape(spriteComp.Sprite.TextureRect.Width, spriteComp.Sprite.TextureRect.Height, 1.0f))));
		    AddComponent(spriteComp);
	    }

	    public SpriteActor(Sprite sprite)
	    {
			SetRootComponent(new CollisionComponent(BulletPhysicsEngine.ConstructRigidBody(null, 1.0f, Matrix.Identity, new BoxShape(sprite.TextureRect.Width, sprite.TextureRect.Height, 1.0f))));
		    AddComponent(new SpriteComponent(sprite));
	    }

	    public SpriteActor(Texture t)
	    {
			SetRootComponent(new CollisionComponent(BulletPhysicsEngine.ConstructRigidBody(null, 1.0f, Matrix.Identity, new BoxShape(t.Size.X, t.Size.Y, 1.0f))));
		    AddComponent(new SpriteComponent(new Sprite(t)));
		}

        public override void Tick(float deltaTime)
        {
			base.Tick(deltaTime);
	        if (Components.Count < 2) return;
			Console.WriteLine(RootComponent.Origin + " | " + Components[1].Origin);
        }
    }
}
