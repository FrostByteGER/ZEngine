using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.JUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_SpaceSEM.Game
{
	public class SpaceEditorLevel : Level
	{

		private JGUI GUI { get; set; }

		public SpaceEditorLevel()
		{

		}

		private void initEditor()
		{
			GUI = new JGUI(((SpaceSEMGameInstance)EngineReference.GameInstance).MainGameFont, EngineReference.EngineWindow, EngineReference.InputManager);

			JContainer MainEditContainer = new JContainer(GUI);
			GUI.RootContainer = MainEditContainer;

		}

		public override void OnLevelLoad()
		{
			base.OnLevelLoad();
			initEditor();
		}
	}
}
