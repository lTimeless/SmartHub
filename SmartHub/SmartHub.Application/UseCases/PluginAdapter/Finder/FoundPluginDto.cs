namespace SmartHub.Application.UseCases.PluginAdapter.Finder
{
    public class FoundPluginDto
    {
        public string Name { get; }
        public string Path { get; }

        public FoundPluginDto(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
