using Game1.ECS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.ECS.Components
{
    public class State : ComponentBase
    {
        public string CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
                InTransition = true;
            }
        }

        public bool InTransition { get; set; }

        private string _currentState;
    }
}
