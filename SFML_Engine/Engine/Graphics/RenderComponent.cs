using SFML.Graphics;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Graphics
{
	public class RenderComponent : ActorComponent, Drawable
	{
		public Material ComponentMaterial { get; set; } = new Material();
		public bool Visible { get; set; } = true;
		/// <summary>
		/// Call base implementation first for states setup.(Setting shader etc.)
		/// </summary>
		/// <param name="target"></param>
		/// <param name="states"></param>
		public virtual void Draw(RenderTarget target, RenderStates states)
		{
			states.Shader = ComponentMaterial.MaterialShader;
		}
	}
}