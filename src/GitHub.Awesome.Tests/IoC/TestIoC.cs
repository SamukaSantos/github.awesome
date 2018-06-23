
using GitHub.Awesome.Infra.Interfaces;
using GitHub.Awesome.Infra.Services;
using GitHub.Awesome.Infra.Services.Interfaces;
using GitHub.Awesome.Tests.Infra.Network;

namespace GitHub.Awesome.Tests.IoC
{
    public class TestIoC
    {
        
        public static void Configure()
        {
            DependencyManager.Container.Register<IPullRequestApiService, PullRequestApiService>();
            DependencyManager.Container.Register<IRepositoryApiService, RepositoryApiService>();
            DependencyManager.Container.Register<INetworkConnectivity, NetworkConnectivity>();
        }
    }
}
