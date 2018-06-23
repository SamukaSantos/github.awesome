

namespace GitHub.Awesome.Infra.Interfaces
{
    public interface INetworkConnectivity
    {
        /// <summary>
        /// Checks if is connected.
        /// </summary>
		bool IsConnected { get; }
        /// <summary>
        /// Checks the current network connection.
        /// </summary>
        /// <returns>Return true if the connection is working, otherwise false.</returns>
        bool CheckNetworkConnection();
    }
}
