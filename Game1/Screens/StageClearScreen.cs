using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game1.Data;

namespace Game1.Screens
{
    public class StageClearScreen : Screen
    {
        private SpriteFont _defaultFont;
        private Viewport _viewport;
        private const string _text = "STAGE CLEAR";
        private Vector2 _position;

        public StageClearScreen(ScreenDependencies dependencies) 
            : base(dependencies)
        {
            ScreenMode = ScreenMode.Overlay;
        }

        public override void Init()
        {
            base.Init();
            _defaultFont = ContentManager.Load<SpriteFont>("default");
            _viewport = ScreenManager.Game.GraphicsDevice.Viewport;
            _position = new Vector2((_viewport.Width - _defaultFont.MeasureString(_text).X) / 2, _viewport.Height / 2);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.DrawString(_defaultFont, _text, _position, Color.Green);
        }

        public override void Update(GameTime gameTime, bool isActive)
        {
            if (InputService.ConsumeIntents(new[] { Intent.Confirm }).Any())
            {
                var gameplay = ScreenManager.Peek<GameplayScreen>();
                gameplay.Init();
                ScreenManager.Push(gameplay);
            }
        }
    }
}
