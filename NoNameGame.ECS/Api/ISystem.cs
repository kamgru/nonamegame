namespace NoNameGame.ECS.Api
{
    public interface ISystem
    {
        void SetActive(bool value);
        bool IsActive();
    }
}
