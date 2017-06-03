﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Data;
using NoNameGame.Core.Screens;
using NoNameGame.Core.Services;

namespace NoNameGame.Main.Screens
{
    public class StageClearScreen : Screen
    {
        private SpriteFont _defaultFont;
        private Viewport _viewport;
        private const string Text = "STAGE CLEAR";
        private Vector2 _position;
        private Texture2D _blank;

        public StageClearScreen(ScreenDependencies dependencies) 
            : base(dependencies)
        {
            ScreenMode = ScreenMode.Overlay;
        }

        public override void Init()
        {
            base.Init();
            _defaultFont = ContentManager.Load<SpriteFont>("default");
            _blank = ContentManager.Load<Texture2D>("blank");
            _viewport = ScreenManager.Game.GraphicsDevice.Viewport;
            _position = new Vector2((_viewport.Width - _defaultFont.MeasureString(Text).X) / 2, _viewport.Height / 2);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(_blank, _viewport.Bounds, null, new Color(0, 0, 0, 150));
            SpriteBatch.DrawString(_defaultFont, Text, _position, Color.White);
        }

        public override void Update(GameTime gameTime, bool isActive)
        {
            if (InputService.ConsumeIntents(new[] { Intent.Confirm }).Any())
            {
                Session.Set("stageId", Session.Get<int>("stageId") + 1);

                var gameplay = ScreenManager.Peek<GameplayScreen>();
                gameplay.Init();
                ScreenManager.Push<GameplayScreen>();
            }
        }
    }
}
