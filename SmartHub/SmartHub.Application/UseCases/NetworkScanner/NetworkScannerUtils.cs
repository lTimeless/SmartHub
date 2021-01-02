using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Serilog;

namespace SmartHub.Application.UseCases.NetworkScanner
{
	/// <summary>
	/// Utility methods for the network scanner service.
	/// </summary>
    public static class NetworkScannerUtils
    {
	    /// <summary>
	    /// Creates a readable name from the given hostname.
	    /// </summary>
	    /// <param name="hostname">The hostname to transform.</param>
	    /// <param name="macAddress">The hostname, used to check if string contains <see cref="NetworkConstants.OwnMachine"/> </param>
	    /// <returns>A more readable hostname.</returns>
	    public static string MakeName(string? hostname, string? macAddress = default)
        {
            if (hostname == null)
            {
                return NetworkConstants.NotAvailable;
            }
            if (macAddress == NetworkConstants.Dash)
            {
	            return NetworkConstants.OwnMachine;
            }
            if (hostname == NetworkConstants.FritzBox || hostname == NetworkConstants.SpeedPortIp)
            {
                return hostname;
            }
            if (hostname.Contains(NetworkConstants.DotFritzBox))
            {
                return hostname.Split(new[] {NetworkConstants.DotFritzBox}, StringSplitOptions.None)[0];
            }
            if (hostname.Contains(NetworkConstants.DotSpeedPortIp))
            {
                return hostname.Split(new[] {NetworkConstants.DotSpeedPortIp}, StringSplitOptions.None)[0];
            }
            return NetworkConstants.NotAvailable;
        }

	    /// <summary>
	    /// Looks for system gateway.
	    /// </summary>
	    /// <returns>The gateway address or an empty string.</returns>
        public static string FindMyNetworkGateway()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString().StartsWith("192"))?
                .ToString() ?? NetworkConstants.NotAvailable;
        }

	    /// <summary>
	    /// Retrieves the hostname to the given ip.
	    /// </summary>
	    /// <param name="ip">The ip.</param>
	    /// <returns>The found hostname or empty string.</returns>
        public static async Task<string> GetHostnameAsync(string ip)
        {
            IPHostEntry? res = null;
            try
            {
                res = await Dns.GetHostEntryAsync(IPAddress.Parse(ip));
            }
            catch (SocketException e)
            {
                Log.ForContext(typeof(NetworkScannerUtils)).Information($"{e.Message}");
            }
            return res?.HostName ?? NetworkConstants.NotAvailable;
        }

	    /// <summary>
	    /// Retrieves th mac address to the given ip.
	    /// </summary>
	    /// <param name="ipAddress">The ip.</param>
	    /// <returns>Returns found mac address or an empty string.</returns>
        public static async Task<string> GetMacAddressAsync(string ipAddress)
        {
            try
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "arp",
                        Arguments = "-a " + ipAddress,
                        CreateNoWindow = true
                    }
                };
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                var strOutput = await process.StandardOutput.ReadToEndAsync();
                var substrings = strOutput.Split(NetworkConstants.Dash);
                if (substrings.Length < 8)
                {
                    return NetworkConstants.Dash;
                }

                var macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                                 + "-" + substrings[4] + NetworkConstants.Dash + substrings[5] + NetworkConstants.Dash + substrings[6]
                                 + "-" + substrings[7] + NetworkConstants.Dash
                                 + substrings[8].Substring(0, 2);
                return macAddress;
            }
            catch (Exception e)
            {
                Log.ForContext(typeof(NetworkScannerUtils)).Warning($"Error {e.Message}");
            }
            return NetworkConstants.NotAvailable;
        }
    }
}