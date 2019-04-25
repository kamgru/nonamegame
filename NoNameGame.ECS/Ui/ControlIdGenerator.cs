namespace NoNameGame.ECS.Ui
{
    internal class ControlIdGenerator
    {
        private int _controlId;
        public void Reset() => _controlId = 1;
        public int GenerateId() => _controlId++;
    }
}
