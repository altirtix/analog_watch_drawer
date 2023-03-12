using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Watch
{
    class SystemInfo
    {
        public static string getDate()
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            return "Дата: " + date.ToShortDateString();
        }

        public static string getTime()
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            return "Время: " + date.ToLongTimeString();
        }

        public static DateTime StartTime = DateTime.Now;

        public static string getStopwatch()
        {
            TimeSpan elapsed = DateTime.Now - StartTime;
            string stopWatch = string.Empty;
            stopWatch +=
            elapsed.Hours.ToString("00") + ":" +
            elapsed.Minutes.ToString("00") + ":" +
            elapsed.Seconds.ToString("00");
            return "Секундомер: " + stopWatch;
        }

        public static string getOS()
        {
            OperatingSystem os = Environment.OSVersion;
            return "ОС: " + Convert.ToString(os);
        }
        public static string getLANIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return "LAN IP: " + ip.ToString();
                }
            }
            return "No adapters";
        }
        public static string getWANIP()
        {
            WebClient client = new WebClient();
            if (NetworkInterface.GetIsNetworkAvailable() &&
                new Ping().Send(new IPAddress(new byte[] { 8, 8, 8, 8 }), 2000).Status == IPStatus.Success)
            {
                var ip = client.DownloadString("https://ipinfo.io/ip");

                return "WAN IP: " + ip.ToString().Replace("\n", "");
            }
            return "No internet";
        }
    }
}
