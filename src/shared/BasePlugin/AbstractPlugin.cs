using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHub.BasePlugin
{
	public abstract class AbstractPlugin : IPlugin
	{
		public AbstractPlugin()
		{
			Id = Guid.NewGuid().ToString();
			Company = $"{typeof(AbstractPlugin).Assembly.FullName?.Split("P")[0]}";
			ModifiedAt = DateTime.Now;
			CreatedAt = DateTime.Now;
			var version = typeof(AbstractPlugin).Assembly.GetName().Version;
			AssemblyVersion = version is not null ? version.ToString() : "0.0";
		}

		protected StringBuilder PathBuilder { get; set; }
		protected string Path { get; set; }
		protected Dictionary<string, string> QueryParams { get; set; }
		public string Id { get; set; }
		public abstract string Name { get; set; }
		public string Company { get; set; }
		public DateTime ModifiedAt { get; }
		public DateTime CreatedAt { get; }
		public string AssemblyVersion { get; set; }
	}
}