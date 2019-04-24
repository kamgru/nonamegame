namespace NoNameGame.ECS.Components
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
