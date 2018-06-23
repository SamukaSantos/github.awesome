
using CoreFoundation;
using GitHub.Awesome.Infra.Interfaces;
using GitHub.Awesome.iOS.Infra.Network;
using System;
using System.Net;
using SystemConfiguration;
using Xamarin.Forms;

//[assembly: Dependency(typeof(NetworkConnectivity))]
namespace GitHub.Awesome.iOS.Infra.Network
{
    public class NetworkConnectivity : INetworkConnectivity
    {
        #region Fields

        private NetworkReachability _adHocWiFiNetworkReachability;
        private NetworkReachability _defaultRouteReachability;

        #endregion

        #region Properties

        public bool IsConnected { get; set; }

        #endregion

        #region Events

        private event EventHandler ReachabilityChanged;

        #endregion

        #region Handlers

        private void OnChange(NetworkReachabilityFlags flags)
        {
            ReachabilityChanged?.Invoke(null, EventArgs.Empty);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckNetworkConnection()
        {
            if (InternetConnectionStatus())
            {
                IsConnected = true;
            }
            else if (LocalWifiConnectionStatus())
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
            return IsConnected;
        }

        private bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if (_defaultRouteReachability == null)
            {
                _defaultRouteReachability = new NetworkReachability(new IPAddress(0));
                _defaultRouteReachability.SetNotification(OnChange);
                _defaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }
            if (!_defaultRouteReachability.TryGetFlags(out flags))
                return false;
            return IsReachableWithoutRequiringConnection(flags);
        }

        private bool IsAdHocWiFiNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if (_adHocWiFiNetworkReachability == null)
            {
                _adHocWiFiNetworkReachability = new NetworkReachability(new IPAddress(new byte[] { 169, 254, 0, 0 }));
                _adHocWiFiNetworkReachability.SetNotification(OnChange);
                _adHocWiFiNetworkReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }

            if (!_adHocWiFiNetworkReachability.TryGetFlags(out flags))
                return false;

            return IsReachableWithoutRequiringConnection(flags);
        }

        public static bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
        {
            // Is it reachable with the current network configuration?
            bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

            // Do we need a connection to reach it?
            bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

            // Since the network stack will automatically try to get the WAN up,
            // probe that
            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                noConnectionRequired = true;

            return isReachable && noConnectionRequired;
        }

        private bool InternetConnectionStatus()
        {
            bool defaultNetworkAvailable = IsNetworkAvailable(out NetworkReachabilityFlags flags);
            if (defaultNetworkAvailable && ((flags & NetworkReachabilityFlags.IsDirect) != 0))
            {
                return false;
            }
            else if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
            {
                return true;
            }
            else if (flags == 0)
            {
                return false;
            }

            return true;
        }

        private bool LocalWifiConnectionStatus()
        {
            if (IsAdHocWiFiNetworkAvailable(out NetworkReachabilityFlags flags))
            {
                if ((flags & NetworkReachabilityFlags.IsDirect) != 0)
                    return true;
            }
            return false;
        }

        #endregion
    }
}