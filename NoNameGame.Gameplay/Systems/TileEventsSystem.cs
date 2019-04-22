using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;
using NoNameGame.Gameplay.Events;
using NoNameGame.Gameplay.Factories;
using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.Gameplay.Systems
{
    public class TileEventsSystem
        : SystemBase,
        IMessageListener<EntityCreated>,
        IGameEventHandler<PlayerAbandonedTile>,
        IGameEventHandler<PlayerEnteredTile>
    {
        private Poof _poof;
        private Player _player;
        private List<Tile> _tiles = new List<Tile>();

        public TileEventsSystem(
            PoofFactory poofFactory)
        {
            GameEventManager.RegisterHandler<PlayerAbandonedTile>(this);
            SystemMessageBroker.AddListener<EntityCreated>(this);
            GameEventManager.RegisterHandler<PlayerEnteredTile>(this);
            _poof = poofFactory.CreatePoof();
        }

        public void Handle(EntityCreated message)
        {
            switch (message.Entity)
            {
                case Tile tile:
                    _tiles.Add(tile);
                    break;

                case Player player:
                    _player = player;
                    break;
            }
        }

        public void Handle(PlayerAbandonedTile gameEvent)
        {
            var tileInfo = _tiles
                .Where(x => x.GetComponent<TileInfo>().Position == _player.GetComponent<PositionOnBoard>().Previous)
                .Select(x => x.GetComponent<TileInfo>())
                .First();

            if (tileInfo.TileType == TileType.Normal)
            {
                tileInfo.Value--;
                if (tileInfo.Value <= 0)
                {
                    var state = tileInfo.Entity.GetComponent<State>();
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
                var tiles = _tiles.Select(x => x.GetComponent<TileInfo>());

                if (tiles.Where(x => x.TileType == TileType.Normal).All(x => x.Destroyed))
                {
                    GameEventManager.Raise(new StageCleared());
                }
            }
        }

        public override void Handle(EntityDestroyed message)
        {
            switch (message.Entity)
            {
                case Tile tile:
                    _tiles.Remove(tile);
                    break;

                case Player _:
                    _player = null;
                    break;

                case Poof _:
                    _poof = null;
                    break;
            }
        }
    }
}
