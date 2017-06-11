using SFML.Graphics;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Graphics
{
	public class RenderComponent : ActorComponent, Drawable
	{
		public Material ComponentMaterial { get; set; } = new Material();

		/// <summary>
		/// Component Layer. Use to modify the draw order of components.
		/// <para>Layers are sorted from front to back. So 0 is the front and Layers.MaxValue is the back. </para>
		/// </summary>
		public uint ComponentLayerID { get; set; } = 1;

		public bool Visible { get; set; } = true;

		/// <summary>
		/// Call base implementation first for states setup.(Setting shader etc.)
		/// </summary>
		/// <param name="target"></param>
		/// <param name="states"></param>
		public virtual void Draw(RenderTarget target, RenderStates states)
		{
			//TODO: Totally useless as states is a struct and is copied...
			states = new RenderStates(states);
			if(ComponentMaterial?.MaterialShader != null) states.Shader = ComponentMaterial.MaterialShader;
			states.Transform = WorldTransform.Transform; // TODO: Is necessary?

		}
	}
}