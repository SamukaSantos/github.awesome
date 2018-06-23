
using Android.Content;
using Android.Net;
using GitHub.Awesome.Droid.Infra.Network;
using GitHub.Awesome.Infra.Interfaces;
using Xamarin.Forms;

//[assembly: Dependency(typeof(NetworkConnectivity))]
namespace GitHub.Awesome.Droid.Infra.Network
{
    public class NetworkConnectivity : INetworkConnectivity
    {
        #region Fields

        private bool _isConnected;

        #endregion

        #region Properties

        public bool IsConnected
        {
            get { return CheckNetworkConnection(); }
            private set { _isConnected = value; }
        }

        #endregion

        #region Methods

        public bool CheckNetworkConnection()
        {
            var context = AppContext.Current;

            var connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);

            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;

            return activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting ? true : false;
        }

        #endregion
    }
}