﻿using System.Linq;
using Game1.Data;
using Game1.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Screens
{

    public class MainMenuScreen : Screen
    {
        private SpriteFont _menuFont;

        public MainMenuScreen(ContentManager contentManager, ScreenManager screenManager, InputService inputService, SpriteBatch spriteBatch)
            : base(contentManager, screenManager, inputService, spriteBatch)
        {
        }

        public override void Init()
        {
            _menuFont = ContentManager.Load<SpriteFont>("default");
            InputService.SetContextActive(0, true);
            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            var intents = InputService.ConsumeIntents(new[] {Intent.Confirm});

            if (intents.Any())
            {
                var gameplay = new GameplayScreen(ContentManager, ScreenManager, InputService, SpriteBatch);
                ScreenManager.Push(gameplay);
                gameplay.Init();

            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.DrawString(_menuFont, "MAIN MENU", Vector2.Zero,  Color.Black);
        }
    }
}