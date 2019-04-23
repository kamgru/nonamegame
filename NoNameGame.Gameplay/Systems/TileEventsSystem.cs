using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;
using NoNameGame.Gameplay.Events;
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
        private IEnumerable<TileType> _clearableTypes = new []
        {
            TileType.Single,
            TileType.Double,
            TileType.Triple,
        };

        public TileEventsSystem()
        {
            GameEventManager.RegisterHandler<PlayerAbandonedTile>(this);
            SystemMessageBroker.AddListener<EntityCreated>(this);
            GameEventManager.RegisterHandler<PlayerEnteredTile>(this);
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

                case Poof poof:
                    _poof = poof;
                    break;
            }
        }

        public void Handle(PlayerAbandonedTile gameEvent)
        {
            var tileInfo = _tiles
                .Where(x => x.GetComponent<TileInfo>().Position == _player.GetComponent<PositionOnBoard>().Previous)
                .Select(x => x.GetComponent<TileInfo>())
                .First();

            if (_clearableTypes.Contains(tileInfo.TileType))
            {
                tileInfo.Value--;

                var state = tileInfo.Entity.GetComponent<State>();
                state.CurrentState = tileInfo.Value <= 0
                    ? TileStates.Destroyed
                    : TileStates.Touched;
            }
        }

        public void Handle(PlayerEnteredTile gameEvent)
        {
            _poof.Transform.Position = gameEvent.TileEntity.Transform.Position;
            _poof.GetComponent<Animator>().Play("poof");

            if (gameEvent.TileInfo.TileType == TileType.End)
            {
                var tiles = _tiles.Select(x => x.GetComponent<TileInfo>());

                if (tiles.Where(x => _clearableTypes.Contains(x.TileType)).All(x => x.Destroyed))
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

        public override void Reset()
        {
            _player = null;
            _poof = null;
            _tiles.Clear();
        }
    }
}
