﻿using Game1.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Entities
{
    public class Entity
    {
        private List<ComponentBase> _components = new List<ComponentBase>();

        public bool HasComponent<TComponent>() where TComponent : ComponentBase
        {
            return _components.Any(x => x is TComponent);
        }

        public TComponent GetComponent<TComponent>() where TComponent : ComponentBase
        {
            return (TComponent)_components.FirstOrDefault(x => x is TComponent);
        }

        public void AddComponent(ComponentBase component)
        {
            _components.Add(component);
        }
    }
}
