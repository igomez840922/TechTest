namespace Test.Client.Services
{
    public interface IRefreshService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}
