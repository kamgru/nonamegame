using System;
using Microsoft.Xna.Framework;
using NoNameGame.ECS.Systems;
using NoNameGame.Core.Screens;
using NoNameGame.Core.Services;
using NoNameGame.Gameplay.Events;
using NoNameGame.Gameplay.Systems;
using NoNameGame.Gameplay.StateManagement;
using NoNameGame.Gameplay.Factories;
using NoNameGame.Gameplay.Services;
using NoNameGame.Gameplay.Components;
using System.Collections.Generic;
using NoNameGame.ECS.Entities;
using NoNameGame.Gameplay.Systems.CommandHandling;
using NoNameGame.ECS.Messaging;

namespace NoNameGame.Main.Screens
{
    public class GameplayScreen : Screen, IGameEventHandler<StageClear>
    {
        private SystemsManager _systemsManager;
        private ConfigurationService _configurationService;

        public GameplayScreen(ScreenDependencies dependencies) 
            : base(dependencies)
        {
        }

        public override void Init()
        {
            base.Init();

            GameEventManager.RegisterHandler(this);
            _configurationService = new ConfigurationService();

            SetupSystems();
            SetupStage();
        }

        public override void Update(GameTime gameTime, bool isActive)
        {
            if (isActive)
            {
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

            var board = new BoardFactory(new TileFactory(ContentManager, _configurationService))
                .CreateBoard(new BoardService().GetBoard(stageId));

            var tileSize = _configurationService.GetTileSizeInPixels();
            var size = board.GetComponent<BoardInfo>().Size * tileSize;


            var player = new PlayerFactory(ContentManager).CreatePlayer();
            player.Transform.SetParent(board.Transform);

            board.Transform.Position = new Vector2((ScreenManager.Game.GraphicsDevice.Viewport.Width - size.X) / 2, (ScreenManager.Game.GraphicsDevice.Viewport.Height - size.Y) / 2);
            player.GetComponent<TargetScreenPosition>().Position = player.Transform.Position;
        }

        private void SetupSystems()
        {
            _systemsManager = new SystemsManager();
            _systemsManager.Push(new PlayerInputHandlingSystem(InputService, _configurationService));
            _systemsManager.Push(new PlayerCommandHandlingSystem(new MovePlayerCommandHandler()));
            _systemsManager.Push(new SpriteDrawingSystem(ContentManager, SpriteBatch));
            _systemsManager.Push(new AnimationSystem(_configurationService.GetFps()));
            _systemsManager.Push(new MoveToScreenPositionSystem());
            _systemsManager.Push(new TileEventsSystem(new PoofFactory(ContentManager)));

            var fsmSystem = new FsmSystem();
            fsmSystem.RegisterHandler(new PlayerIdleHandler(InputService));
            fsmSystem.RegisterHandler(new PlayerMovingHandler(InputService));
            fsmSystem.RegisterHandler(new PlayerDeadHandler());
            fsmSystem.RegisterHandler(new TileDestroyedHandler());
            _systemsManager.Push(fsmSystem);

            _systemsManager.Peek<PlayerInputHandlingSystem>().SetActive(true);
            _systemsManager.Peek<PlayerCommandHandlingSystem>().SetActive(true);
            _systemsManager.Peek<SpriteDrawingSystem>().SetActive(true);
            _systemsManager.Peek<AnimationSystem>().SetActive(true);
            _systemsManager.Peek<MoveToScreenPositionSystem>().SetActive(true);
            _systemsManager.Peek<FsmSystem>().SetActive(true);
            _systemsManager.Peek<TileEventsSystem>().SetActive(true);
        }
        
        public void Handle(StageClear message)
        {
            ScreenManager.Push<StageClearScreen>();
        }
    }
}
