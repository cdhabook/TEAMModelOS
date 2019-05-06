﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TEAMModelOS.SDK.Helper.Network.NetworkHelper
{
    public static class NetHelper
    {
        /// <summary>
        /// The ip segment regex
        /// </summary>
        private const string IPSegmentRegex = @"\d{0,3}";

        /// <summary>
        /// Gets the ip.
        /// </summary>
        /// <param name="ipSegment">ip段</param>
        /// <returns></returns>
        public static string GetIp(string ipSegment)
        {
            if (string.IsNullOrWhiteSpace(ipSegment))
                throw new ArgumentNullException(nameof(ipSegment));

            //如果设置的IP支持* 的时候,再去智能的选择ip
            if (!ipSegment.Contains("*"))
            {
                return ipSegment;
            }

            ipSegment = ipSegment.Replace("*", IPSegmentRegex).Replace(".", "\\.");

            var hostAddrs = NetworkInterface.GetAllNetworkInterfaces()
            .Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .SelectMany(i => i.GetIPProperties().UnicastAddresses)
                .Select(a => a.Address)
                .Where(a => !(a.IsIPv6LinkLocal || a.IsIPv6Multicast || a.IsIPv6SiteLocal || a.IsIPv6Teredo))
                .ToList();

            foreach (var ip in hostAddrs)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                    && System.Text.RegularExpressions.Regex.IsMatch(ip.ToString(), ipSegment))
                {
                    return ip.ToString();
                }
            }

            throw new Exception($"找不到ipsegement:{ipSegment}匹配的ip, OR No network adapters with an IPv4 address in the system!");
        }

        /// <summary>
        /// 解析ip和port
        /// </summary>
        /// <param name="serviceAddress"></param>
        /// <returns></returns>
        public static Tuple<string, int> GetIPAndPort(string serviceAddress)
        {
            //解析ip
            var ipPort = serviceAddress.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            var ip = NetHelper.GetIp(ipPort[0]);
            //解析port
            var port = 0;
            if (ipPort.Length == 2)
            {
                int.TryParse(ipPort[1], out port);
            }
            return Tuple.Create(ip, port);
        }

        /// <summary>
        ///   获取所有网卡IP地址
        /// </summary>
        /// <returns></returns>
        public static List<string> getIPAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
            .SelectMany(i => i.GetIPProperties().UnicastAddresses)
            .Select(a => a.Address)
            //AddressFamily.InterNetwork  过滤掉IPV6  //过滤127.0.0.1  !IPAddress.IsLoopback(a)
            .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
            .Select(a => a.ToString()).ToList();
        }
    }
}