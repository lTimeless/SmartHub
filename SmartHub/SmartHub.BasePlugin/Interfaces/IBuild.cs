namespace SmartHub.BasePlugin.Interfaces
{
    public interface IBuild<T> : IPlugin where T : IBuild<T>
    {
        T Instantiate();
        string Build();
    }
}