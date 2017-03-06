using Game1.Api;
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
        private IEntityFactory _entityFactory;
        private DrawingSystem _drawingSystem;

        private InputHandlingSystem _inputHandlingSystem;
        private MoveToScreenPositionSystem _moveToScreenPositionSystem;
        private MoveToNewTileSystem _moveToNewTileSystem;
        private AnimationSystem _animationSystem;

        private InputMappingService _inputMappingService;
        private ConfigurationService _configurationService;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _configurationService = new ConfigurationService();
            _inputMappingService = new InputMappingService();

            _entityManager = new EntityManager();
            _entityFactory = new EntityFactory(_entityManager);
            _drawingSystem = new DrawingSystem(_entityManager, Content);
            _animationSystem = new AnimationSystem(_entityManager);

            _inputHandlingSystem = new InputHandlingSystem(_entityManager, _inputMappingService, _configurationService);
            _moveToScreenPositionSystem = new MoveToScreenPositionSystem(_entityManager);
            _moveToNewTileSystem = new MoveToNewTileSystem(_entityManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var board = new BoardBuildingService(_configurationService, Content, _entityFactory).Build(1);

            var tileSize = _configurationService.GetTileSizeInPixels();
            var size = board.BoardInfo.Size * tileSize;

            var player = new PlayerBuildingService(Content, _entityFactory).Build();
            player.Transform.SetParent(board.Transform);

            board.Transform.Position = new Vector2((GraphicsDevice.Viewport.Width - size.X) / 2 , (GraphicsDevice.Viewport.Height - size.Y) / 2 );

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _inputHandlingSystem.Update(gameTime);
            _moveToScreenPositionSystem.Update(gameTime);
            _moveToNewTileSystem.Update(gameTime);
            _animationSystem.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _drawingSystem.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
