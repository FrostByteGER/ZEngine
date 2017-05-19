using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.UI
{
	public class UIActor : Actor
	{

		public uint UILayerID { get; set; } = 0;

		public UIActor()
		{
		}

		public override void Destroy(bool disposing)
		{
			base.Destroy(disposing);
			//TODO: Add
		}
	}
}