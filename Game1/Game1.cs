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
        private TileBasedMovementSystem _movementSystem;

        private InputMappingService _inputMappingService;
        private ConfigurationService _configurationService;

        private List<Entity> _entities = new List<Entity>();

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
            _drawingSystem = new DrawingSystem(_entityManager);
          
            _inputHandlingSystem = new InputHandlingSystem(_entityManager, _inputMappingService, _configurationService);
            _movementSystem = new TileBasedMovementSystem(_entityManager, _configurationService);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var board = new BoardBuildingService(_configurationService, Content, _entityFactory).Build(1);

            var size = board.GetBoardSize();
            var tileSize = _configurationService.GetTileSizeInPixels();
            var v = size * tileSize;

            var player = _entityFactory.Create<Player>(Vector2.Zero, Content.Load<Texture2D>("red_dot"));
            player.Transform.SetParent(board.Transform);

            board.Transform.Position = new Vector2((GraphicsDevice.Viewport.Width - v.X) / 2 , (GraphicsDevice.Viewport.Height - v.Y) / 2 );

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _inputHandlingSystem.Update();
            _movementSystem.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _drawingSystem.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
