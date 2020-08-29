using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Serilog;

namespace SmartHub.Application.UseCases.NetworkScanner
{
    public static class NetworkScannerUtils
    {
        public static string MakeNameFromHostname(string hostname)
        {
            if (hostname == null)
            {
                return "Not available";
            }

            if (hostname == "fritz.box" || hostname == "speedport.ip")
            {
                return hostname;
            }

            if (hostname.Contains(".fritz.box"))
            {
                return hostname.Split(new[] {".fritz.box"}, StringSplitOptions.None)[0];
            }

            if (hostname.Contains(".speedport.ip"))
            {
                return hostname.Split(new[] {".speedport.ip"}, StringSplitOptions.None)[0];
            }

            return "Not available";
        }

        public static string FindMyNetworkGateway()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?
                .ToString() ?? "";
        }

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

            return res?.HostName ?? "";
        }

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
                var substrings = strOutput.Split('-');
                if (substrings.Length < 8)
                {
                    return "OWN Machine";
                }

                var macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                                 + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                                 + "-" + substrings[7] + "-"
                                 + substrings[8].Substring(0, 2);
                return macAddress;
            }
            catch (Exception e)
            {
                Log.ForContext(typeof(NetworkScannerUtils)).Warning($"Error {e.Message}");
            }

            return null;
        }
    }
}