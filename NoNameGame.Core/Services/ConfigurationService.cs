using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using NoNameGame.Core.Input;
using NoNameGame.Data;
using Microsoft.Xna.Framework.Input;

namespace NoNameGame.Core.Services
{

    public class ConfigurationService
    {
        public int GetFps()
        {
            return 24;
        }

        public Point GetTileSizeInPixels()
        {
            return new Point(32, 32);
        }

        public IReadOnlyCollection<InputContext> GetInputContexts()
        {
            return new[]
            {
                new InputContext
                {
                    Id = (int)Context.Gameplay,
                    Active = true,
                    Name = "gameplay context",
                    Intents = new[]
                    {
                        new InputIntent
                        {
                             Id = (int)Intent.MoveLeft,
                             Key = Keys.Left
                        },
                        new InputIntent
                        {
                            Id = (int)Intent.MoveRight,
                            Key = Keys.Right
                        },
                        new InputIntent
                        {
                             Id = (int)Intent.MoveUp,
                             Key = Keys.Up
                        },
                        new InputIntent
                        {
                            Id = (int)Intent.MoveDown,
                            Key = Keys.Down
                        },
                    }
                },
                new InputContext
                {
                    Id = (int)Context.Generic,
                    Active = true,
                    Name = "generic context",
                    Intents = new[]
                    {
                        new InputIntent
                        {
                            Id = (int)Intent.Confirm,
                            Key = Keys.Enter
                        }
                    }
                }
            };
        }
    }
}