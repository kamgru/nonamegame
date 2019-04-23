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
        private End _end;

        public TileEventsSystem()
        {
            GameEventManager.RegisterHandler<PlayerAbandonedTile>(this);
            SystemMessageBroker.AddListener<EntityCreated>(this);
            GameEventManager.RegisterHandler<PlayerEnteredTile>(this);
        }

        public override void Reset()
        {
            _player = null;
            _poof = null;
            _end = null;
            _tiles.Clear();
        }

        public void Handle(PlayerAbandonedTile gameEvent)
        {
            var tile = _tiles
                .Where(x => x.TileInfo.Position == _player.PositionOnBoard.Previous)
                .Single();

            if (tile.TileInfo.IsClearable)
            {
                tile.TileInfo.Value--;

                tile.State.CurrentState = tile.TileInfo.Value <= 0
                    ? TileStates.Destroyed
                    : TileStates.Touched;

                var tiles = _tiles.Where(x => x.TileInfo.IsClearable);

                if (tiles.All(x => x.State.CurrentState == TileStates.Destroyed))
                {
                    _end.State.CurrentState = EndStates.Open;
                }
            }
        }

        public void Handle(PlayerEnteredTile gameEvent)
        {
            _poof.Transform.Position = gameEvent.Tile.Transform.Position;
            _poof.GetComponent<Animator>().Play("poof");

            if (_player.PositionOnBoard.Current == _end.PositionOnBoard.Current)
            {
                if (_end.State.CurrentState == EndStates.Open)
                {
                    GameEventManager.Raise(new StageCleared());
                }
            }
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

                case End end:
                    _end = end;
                    break;
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
