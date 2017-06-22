using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.JUI;

namespace SFML_TowerDefense.Source.GUI
{
	public class GUILevelTest : Level 
	{

		public JGUI GUI;

		public RectangleShape test = new RectangleShape();

		protected override void InitLevel()
		{
			base.InitLevel();

			GUI = new JGUI(null, EngineReference.EngineWindow, EngineReference.InputManager);

			initGUI();

			test.FillColor = Color.Cyan;
			test.Position = new Vector2f(0,0);
			test.Size = new Vector2f(100,100);
		}

		private void initGUI()
		{
			JContainer Root = new JContainer(GUI);

			Root.setPosition(new Vector2f(50,50));
			Root.setSize(new Vector2f(600,600));

			Root.setBackgroundColor(new Color(200,200,100));

			Root.Margin.setAll(0.01f);

			JBorderLayout layout = new JBorderLayout(Root);

			layout.TopSize = 0.1f;
			layout.BottemSize = 0.1f;
			layout.LeftSize = 0.1f;
			layout.RightSize = 0.1f;

			Root.Layout = layout;

			JElement e = new JContainer(GUI);
			e.Padding.setAll(0.01f);
			e.setBackgroundColor(Color.Yellow);
			Root.addElement(e, JBorderLayout.RIGHT);

			JContainer c = new JContainer(GUI);
			c.setBackgroundColor(Color.Blue);
			c.Layout = new JLayout(c);

			e = new JElement(GUI);
			c.addElement(e);
			//c.Padding.setAll(0.01f);
			e.Padding.setAll(0.01f);

			Root.addElement(c, JBorderLayout.CENTER);

			e = new JElement(GUI);
			e.setBackgroundColor(Color.Red);
			Root.addElement(e, JBorderLayout.LEFT);

			e = new JElement(GUI);
			e.setBackgroundColor(Color.Cyan);
			Root.addElement(e, JBorderLayout.TOP);

			e = new JElement(GUI);
			e.setBackgroundColor(Color.Green);
			Root.addElement(e, JBorderLayout.BOTTOM);

			Root.ReSize();

			GUI.RootContainer = Root;
		}

		protected override void LevelDraw(ref RenderWindow renderWindow)
		{
			base.LevelDraw(ref renderWindow);
			renderWindow.Draw(GUI);
		}
	}
}
