namespace Game1.ECS.Api
{
    public interface ISystem
    {
        void SetActive(bool value);
        bool IsActive();
    }
}
