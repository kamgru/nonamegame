using System;
using Microsoft.Xna.Framework;
using Game1.ECS.Api;
using Game1.ECS.Core;
using Game1.ECS.Systems;
using Game1.Core.Screens;
using Game1.Core.Services;
using Game1.Core.Events;
using Game1.Gameplay.Events;
using Game1.Gameplay.Systems;
using Game1.Gameplay.StateManagement;
using Game1.Gameplay.Factories;
using Game1.Gameplay.Services;
using Game1.Gameplay.Components;
using System.Collections.Generic;

namespace Game1.Screens
{
    public class GameplayScreen : Screen
    {
        private SystemsManager _systemsManager;
        private IEntityManager _entityManager;
        private ConfigurationService _configurationService;
        private EventManager _eventManager;

        public GameplayScreen(ScreenDependencies dependencies) 
            : base(dependencies)
        {
        }

        public override void Init()
        {
            base.Init();

            _entityManager = new EntityManager();
            _eventManager = new EventManager();
            _configurationService = new ConfigurationService();

            SetupEvents();
            SetupSystems();
            SetupStage();
        }

        public override void Update(GameTime gameTime, bool isActive)
        {
            if (isActive)
            {
                _eventManager.Dispatch();
                _systemsManager.UpdateSystems(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _systemsManager.Draw(gameTime);
        }

        private void SetupStage()
        {
            var stageId = 0;
            try
            {
                stageId = Session.Get<int>("stageId");
            }
            catch (Exception e) when (e is KeyNotFoundException || e is ArgumentException)
            {
                Session.Set("stageId", 0);
            }

            var board = new BoardFactory(_entityManager, ContentManager, new TileFactory(_entityManager, ContentManager, _configurationService))
                .CreateBoard(new BoardService().GetBoard(stageId));

            var tileSize = _configurationService.GetTileSizeInPixels();
            var size = board.GetComponent<BoardInfo>().Size * tileSize;


            var player = new PlayerFactory(_entityManager, ContentManager).CreatePlayer();
            player.Transform.SetParent(board.Transform);

            board.Transform.Position = new Vector2((ScreenManager.Game.GraphicsDevice.Viewport.Width - size.X) / 2, (ScreenManager.Game.GraphicsDevice.Viewport.Height - size.Y) / 2);
            player.GetComponent<TargetScreenPosition>().Position = player.Transform.Position;
        }

        private void SetupSystems()
        {
            _systemsManager = new SystemsManager();
            _systemsManager.Push(new PlayerInputHandlingSystem(_entityManager, InputService, _configurationService, _eventManager));
            _systemsManager.Push(new SpriteDrawingSystem(_entityManager, ContentManager, SpriteBatch));
            _systemsManager.Push(new AnimationSystem(_entityManager, _configurationService.GetFps()));
            _systemsManager.Push(new MoveToScreenPositionSystem(_entityManager));
            _systemsManager.Push(new TileEventsSystem(_entityManager, _eventManager, new PoofFactory(_entityManager, ContentManager)));

            var fsmSystem = new FsmSystem(_entityManager);
            fsmSystem.RegisterHandler(new PlayerIdleHandler(InputService));
            fsmSystem.RegisterHandler(new PlayerMovingHandler(InputService, _entityManager, _eventManager));
            fsmSystem.RegisterHandler(new PlayerDeadHandler(_entityManager));
            fsmSystem.RegisterHandler(new TileDestroyedHandler(_entityManager));
            _systemsManager.Push(fsmSystem);

            _systemsManager.Peek<PlayerInputHandlingSystem>().SetActive(true);
            _systemsManager.Peek<SpriteDrawingSystem>().SetActive(true);
            _systemsManager.Peek<AnimationSystem>().SetActive(true);
            _systemsManager.Peek<MoveToScreenPositionSystem>().SetActive(true);
            _systemsManager.Peek<FsmSystem>().SetActive(true);
            _systemsManager.Peek<TileEventsSystem>().SetActive(true);
        }
        
        private void SetupEvents()
        {
            _eventManager.On<StageClear>(x => ScreenManager.Push<StageClearScreen>());
        }
    }
}
