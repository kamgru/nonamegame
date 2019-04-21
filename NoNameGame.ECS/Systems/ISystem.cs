namespace NoNameGame.ECS.Systems
{
    public interface ISystem
    {
        void SetActive(bool value);
        bool IsActive();
    }
}
