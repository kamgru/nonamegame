﻿using Game1.Api;
using Game1.Components;
using Game1.Entities;
using Game1.Managers;
using Game1.Services;
using Game1.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Game1.Factories;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private IEntityManager _entityManager;
        private SystemsManager _systemsManager;

        private InputMappingService _inputMappingService;
        private ConfigurationService _configurationService;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            Content.RootDirectory = "Content";
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _configurationService = new ConfigurationService();
            _inputMappingService = new InputMappingService();

            _entityManager = new EntityManager();
            _systemsManager = new SystemsManager();

            _systemsManager.Push(new SpriteDrawingSystem(_entityManager, Content, _spriteBatch));
            _systemsManager.Push(new AnimationSystem(_entityManager, _configurationService));
            _systemsManager.Push(new InputHandlingSystem(_entityManager, _inputMappingService, _configurationService));
            _systemsManager.Push(new MoveToScreenPositionSystem(_entityManager));
            _systemsManager.Push(new MoveToNewTileSystem(_entityManager));
            _systemsManager.Push(new TileAbandonedSystem(_entityManager));

            var board = new BoardFactory(_entityManager, 
                Content, 
                _configurationService, new TileFactory(
                    _entityManager, 
                    Content, 
                    _configurationService))
                .CreateBoard(new BoardService().GetBoard(1));

            var tileSize = _configurationService.GetTileSizeInPixels();
            var size = board.GetComponent<BoardInfo>().Size * tileSize;

            var player = new PlayerFactory(_entityManager, Content).CreateEntity();
            player.Transform.SetParent(board.Transform);

            board.Transform.Position = new Vector2((GraphicsDevice.Viewport.Width - size.X) / 2, (GraphicsDevice.Viewport.Height - size.Y) / 2);

            base.Initialize();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _systemsManager.UpdateSystems(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _systemsManager.Draw(gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
