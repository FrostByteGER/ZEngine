using SFML.Graphics;
using SFML.Graphics.Glsl;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.IO;

namespace SFML_SpaceSEM.Game.Actors
{
	public class BackgroundActor : Actor
	{

		public SpriteComponent SpriteComp { get; set; }
		public BackgroundActor(Sprite sprite, Level level) : base(level)
		{
			SpriteComp = new SpriteComponent(sprite);
			SpriteComp.Sprite.Texture.Repeated = true;
			SetRootComponent(SpriteComp);
			Origin = SpriteComp.Origin; // Center this actor.
			SpriteComp.ComponentMaterial.MaterialShader = new Shader(AssetManager.AssetsPath + "Shaders/scrollingTexture.vert", null, AssetManager.AssetsPath + "Shaders/scrollingTexture.frag");
		}



		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			SpriteComp.ComponentMaterial.MaterialShader.SetUniform("scrollRate", .5f);
			SpriteComp.ComponentMaterial.MaterialShader.SetUniform("u_time", new Vec2(1, -LevelReference.EngineReference.EngineCoreClock.EngineElapsedSeconds));
		}


	}
}