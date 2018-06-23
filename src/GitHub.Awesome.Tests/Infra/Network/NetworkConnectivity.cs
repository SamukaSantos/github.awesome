
using GitHub.Awesome.Infra.Interfaces;

namespace GitHub.Awesome.Tests.Infra.Network
{
    public class NetworkConnectivity : INetworkConnectivity
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckNetworkConnection()
        {
            return true;
        }
    }
}
