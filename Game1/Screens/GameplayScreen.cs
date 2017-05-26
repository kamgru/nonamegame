using System;
using Game1.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game1.Managers;
using Game1.Systems;
using Game1.Api;
using Game1.Factories;
using Game1.Components;

namespace Game1.Screens
{
    public class GameplayScreen : Screen
    {
        private SystemsManager _systemsManager;
        private IEntityManager _entityManager;
        private IConfigurationService _configurationService;

        public GameplayScreen(ContentManager contentManager, ScreenManager screenManager, InputService inputService, SpriteBatch spriteBatch) 
            : base(contentManager, screenManager, inputService, spriteBatch)
        {
            IsSingle = true;
        }

        public override void Init()
        {
            base.Init();

            _systemsManager = new SystemsManager();
            _entityManager = new EntityManager();
            _configurationService = new ConfigurationService();

            _systemsManager.Push(new PlayerInputHandlingSystem(_entityManager, InputService, _configurationService));
            _systemsManager.Push(new SpriteDrawingSystem(_entityManager, ContentManager, SpriteBatch));
            _systemsManager.Push(new AnimationSystem(_entityManager, _configurationService));
            _systemsManager.Push(new MoveToScreenPositionSystem(_entityManager));

            var fsmSystem = new FsmSystem(_entityManager);
            fsmSystem.RegisterHandler(new PlayerIdleHandler(InputService));
            fsmSystem.RegisterHandler(new PlayerMovingHandler(InputService, _entityManager));
            fsmSystem.RegisterHandler(new PlayerDeadHandler(_entityManager));
            fsmSystem.RegisterHandler(new TileAbandonedHandler());
            fsmSystem.RegisterHandler(new TileDestroyedHandler(_entityManager));
            _systemsManager.Push(fsmSystem);

            var board = new BoardFactory(_entityManager,
                ContentManager,
                _configurationService, new TileFactory(
                    _entityManager,
                    ContentManager,
                    _configurationService))
                .CreateBoard(new BoardService().GetBoard(1));

            var tileSize = _configurationService.GetTileSizeInPixels();
            var size = board.GetComponent<BoardInfo>().Size * tileSize;

            var player = new PlayerFactory(_entityManager, ContentManager).CreateEntity();
            player.Transform.SetParent(board.Transform);

            board.Transform.Position = new Vector2((ScreenManager.Game.GraphicsDevice.Viewport.Width - size.X) / 2, (ScreenManager.Game.GraphicsDevice.Viewport.Height - size.Y) / 2);
            player.GetComponent<TargetScreenPosition>().Position = player.Transform.Position;

            _systemsManager.Peek<PlayerInputHandlingSystem>().SetActive(true);
            _systemsManager.Peek<SpriteDrawingSystem>().SetActive(true);
            _systemsManager.Peek<AnimationSystem>().SetActive(true);
            _systemsManager.Peek<MoveToScreenPositionSystem>().SetActive(true);
            _systemsManager.Peek<FsmSystem>().SetActive(true);
        }

        public override void Update(GameTime gameTime)
        {
            _systemsManager.UpdateSystems(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _systemsManager.Draw(gameTime);
        }
    }
}
