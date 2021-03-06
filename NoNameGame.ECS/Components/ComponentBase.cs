﻿using NoNameGame.ECS.Entities;

namespace NoNameGame.ECS.Components
{
    public abstract class ComponentBase
    {
        public bool Active { get; set; } = true;
        public Entity Entity { get; set; }
    }
}