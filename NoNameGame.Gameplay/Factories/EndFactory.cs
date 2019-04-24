using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;

namespace NoNameGame.Gameplay.Factories
{
    public class EndFactory
    {
        private readonly ContentManager _contentManager;

        public EndFactory(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public End CreateEnd()
        {
            var sheet = _contentManager.Load<Texture2D>(SpriteSheetNames.EndSheet);
            var end = new End();

            end.Sprite = end.AddComponent(new Sprite
            {
                Rectangle = new Rectangle(0, 0, 32, 32),
                Texture2D = sheet,
                ZIndex = 1100
            });

            end.State = end.AddComponent(new State
            {
                CurrentState = EndStates.Closed
            });

            end.PositionOnBoard = end.AddComponent(new PositionOnBoard());

            end.Animator = end.AddComponent(new Animator());
            end.Animator.Animations = new[]
            {
                new Animation(sheet, new []
                {
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32),
                    new Rectangle(96, 0, 32, 32),
                    new Rectangle(128, 0, 32, 32),
                })
                {
                    Looped = true,
                    Name = AnimationDictionary.EndOpen,
                    Speed = 0.15f
                }
            };

            SystemMessageBroker.Send(new EntityCreated(end));

            return end;
        }
    }
}
