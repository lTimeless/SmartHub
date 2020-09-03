namespace SmartHub.Application.UseCases.PluginAdapter
{
    public class PluginDto
    {
        public string Name { get; }
        public string Path { get; }

        public PluginDto(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
