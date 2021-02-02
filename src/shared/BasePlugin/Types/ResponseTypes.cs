using System.Collections.Generic;

namespace SmartHub.BasePlugin.Types
{
	public record StatusResponseType(List<LightResponseType> Lights);

	public record LightResponseType(bool Ison, string Mode, int Red, int Green, int Blue, int White);
}