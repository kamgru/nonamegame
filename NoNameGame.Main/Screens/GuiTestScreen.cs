using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NoNameGame.Core.Screens;
using NoNameGame.ECS.Ui;

namespace NoNameGame.Main.Screens
{
    public class GuiTestScreen : Screen
    {
        public GuiTestScreen(ScreenDependencies dependencies) : base(dependencies) { }


        private float slider1 = 0.5f;

        private string txt = "a";

        public override void Update(GameTime gameTime, bool isActive)
        {
            var mouse = Mouse.GetState();

            Gui.Label(Vector2.Zero, $"mouse: {mouse.X}, {mouse.Y}", Color.Red);


            //slider1 = Gui.Slider(new Rectangle(100, 100, 8, 200), slider1);


            //Gui.Label(new Vector2(0, 20), $"slider1: {slider1}", Color.Red);


            txt = Gui.TextBox(new Rectangle(20, 20, 80, 32), txt);


        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}
