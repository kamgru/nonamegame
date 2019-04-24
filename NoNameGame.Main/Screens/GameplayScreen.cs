using Microsoft.Xna.Framework;
using NoNameGame.Core.Screens;
using NoNameGame.Core.Services;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Events;
using NoNameGame.Gameplay.Factories;
using NoNameGame.Gameplay.StateManagement;
using NoNameGame.Gameplay.Systems;
using System.Linq;

namespace NoNameGame.Main.Screens
{
    public class GameplayScreen
        : Screen,
        IGameEventHandler<StageCleared>,
        IGameEventHandler<PlayerDied>
    {
        private SystemsManager _systemsManager;
        private ConfigurationService _configurationService;
        private EntityRepository _entityRepository;

        public GameplayScreen(ScreenDependencies dependencies)
            : base(dependencies)
        {
        }

        public override void Init()
        {
            base.Init();

            _configurationService = new ConfigurationService();
            _entityRepository = new EntityRepository();

            GameEventManager.RegisterHandler<StageCleared>(this);
            GameEventManager.RegisterHandler<PlayerDied>(this);
            InitSystems();
        }

        public override void OnEnter()
        {
            _systemsManager.ResetSystems();
            foreach (var entity in _entityRepository.GetAll().ToList())
            {
                Entity.Destroy(entity);
            }

            InputMapProvider.GetContextById(Contexts.Gameplay).Activate();
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
            if (!Session.TryGet(SessionKeys.CurrentStageId, out int stageId))
            {
                stageId = 1;
                Session.Set(SessionKeys.CurrentStageId, stageId);
            }

            var stage = new StageDataStorage().Load(stageId);

            var board = new BoardFactory(new TileFactory(ContentManager, _configurationService), new EndFactory(ContentManager), _configurationService)
                .CreateBoard(stage.Board);

            var tileSize = _configurationService.GetTileSizeInPixels();
            var size = board.GetComponent<BoardInfo>().Size * tileSize;


            var player = new PlayerFactory(ContentManager).CreatePlayer();
            player.Transform.SetParent(board.Transform);

            board.Transform.Position = new Vector2((ScreenManager.Game.GraphicsDevice.Viewport.Width - size.X) / 2, (ScreenManager.Game.GraphicsDevice.Viewport.Height - size.Y) / 2);

            new PoofFactory(ContentManager).CreatePoof();
        }

        private void InitSystems()
        {
            _systemsManager = new SystemsManager();
            _systemsManager.Push(new PlayerInputHandlingSystem(IntentProvider, _configurationService));
            _systemsManager.Push(new CommandHandlingSystem());
            _systemsManager.Push(new SpriteDrawingSystem(ContentManager, SpriteBatch));
            _systemsManager.Push(new AnimationSystem(_configurationService.GetFps()));
            _systemsManager.Push(new MoveToScreenPositionSystem());
            _systemsManager.Push(new TileEventsSystem());

            var fsmSystem = new FsmSystem();
            fsmSystem.RegisterHandler(new PlayerIdleHandler(ContentManager, InputMapProvider));
            fsmSystem.RegisterHandler(new PlayerMovingHandler(InputMapProvider));
            fsmSystem.RegisterHandler(new PlayerDeadHandler());
            fsmSystem.RegisterHandler(new TileDestroyedHandler());
            fsmSystem.RegisterHandler(new TileTouchedHandler());
            fsmSystem.RegisterHandler(new EndOpenHandler());
            _systemsManager.Push(fsmSystem);

            _systemsManager.Peek<PlayerInputHandlingSystem>().SetActive(true);
            _systemsManager.Peek<CommandHandlingSystem>().SetActive(true);
            _systemsManager.Peek<SpriteDrawingSystem>().SetActive(true);
            _systemsManager.Peek<AnimationSystem>().SetActive(true);
            _systemsManager.Peek<MoveToScreenPositionSystem>().SetActive(true);
            _systemsManager.Peek<FsmSystem>().SetActive(true);
            _systemsManager.Peek<TileEventsSystem>().SetActive(true);
        }

        public void Handle(StageCleared message)
        {
            InputMapProvider.GetContextById(Contexts.Gameplay).Deactivate();
            ScreenManager.Push<StageClearScreen>();
        }

        public void Handle(PlayerDied message)
        {
            InputMapProvider.GetContextById(Contexts.Gameplay).Deactivate();
            ScreenManager.Push<GameOverScreen>();
        }
    }
}
