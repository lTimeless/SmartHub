namespace SmartHub.BasePlugin.Interfaces.DeviceType
{
	public interface ILight : IBuild<ILight>
	{
		string TurnOnOff { set; }

		/// <summary>
		///
		/// </summary>
		/// <param name="onOff"></param>
		ILight SetToggleLight(bool? onOff);

		/// <summary>
		///
		/// </summary>
		/// <param name="red"></param>
		/// <param name="green"></param>
		/// <param name="blue"></param>
		/// <param name="alpha"></param>
		ILight SetRgba(int red, int green, int blue, int alpha);
	}
}