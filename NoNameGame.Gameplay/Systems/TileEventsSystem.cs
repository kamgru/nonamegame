using System.Linq;
using NoNameGame.Data;
using NoNameGame.ECS.Components;
using NoNameGame.Gameplay.Events;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Factories;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Systems;

namespace NoNameGame.Gameplay.Systems
{
    public class TileEventsSystem 
        : SystemBase,
        IMessageListener<ComponentAdded<TileInfo>>,
        IGameEventHandler<PlayerAbandonedTile>,
        IGameEventHandler<PlayerEnteredTile>
    {
        private readonly Entity _poof;

        public TileEventsSystem(
            PoofFactory poofFactory)
        {
            SystemMessageBroker.AddListener<ComponentAdded<TileInfo>>(this);
            GameEventManager.RegisterHandler<PlayerAbandonedTile>(this);
            GameEventManager.RegisterHandler<PlayerEnteredTile>(this);
            _poof = poofFactory.CreatePoof();
        }

        public void Handle(ComponentAdded<TileInfo> message)
        {
            Entities.Add(message.Entity);
        }

        public override void Handle(EntityCreated message)
        {
            if (message.Entity.HasComponent<TileInfo>())
            {
                Entities.Add(message.Entity);
            }
        }

        public void Handle(PlayerAbandonedTile gameEvent)
        {
            if (gameEvent.TileInfo.TileType == TileType.Normal)
            {
                gameEvent.TileInfo.Value--;
                if (gameEvent.TileInfo.Value <= 0)
                {
                    var state = gameEvent.TileInfo.Entity.GetComponent<State>();
                    if (state != null)
                    {
                        state.CurrentState = TileStates.Destroyed;
                    }
                }
            }
        }

        public void Handle(PlayerEnteredTile gameEvent)
        {
            _poof.Transform.Position = gameEvent.TileEntity.Transform.Position;
            _poof.GetComponent<Animator>().Play("poof");

            if (gameEvent.TileInfo.TileType == TileType.End)
            {
                var tiles = Entities.Select(x => x.GetComponent<TileInfo>());

                if (tiles.Where(x => x.TileType == TileType.Normal).All(x => x.Destroyed))
                {
                    GameEventManager.Raise(new StageClear());
                }
            }
        }
    }
}
