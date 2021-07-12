using System.Reflection;

namespace SmartHub.WebUI
{
	public record AssemblyInformation(string Product, string Description, string Version)
	{
		public static readonly AssemblyInformation Current = new(typeof(AssemblyInformation).Assembly);

		private AssemblyInformation(Assembly assembly) : this(
			assembly.GetCustomAttribute<AssemblyProductAttribute>()!.Product,
			assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()!.Description,
			assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()!.Version)
		{
		}
	}
}