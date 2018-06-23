
using GitHub.Awesome.Infra.Common.IoC;
using GitHub.Awesome.Infra.Services.Interfaces;
using GitHub.Awesome.Tests.IoC;
using Xunit;

namespace GitHub.Awesome.Tests.PullRequest
{
    public class PullRequestTest
    {

        #region Constructor

        public PullRequestTest()
        {
            TestIoC.Configure();
            AppIoC.SetContainer(DependencyManager.Container);
        }

        #endregion

        #region Methods

        [Fact(DisplayName = "ShouldReturnTotalPullRequestsCountageMoreThanZero")]
        public async void ShouldReturnTotalPullRequestsCountageMoreThanZero()
        {
            //Arrange
            var url = "repos/vuejs/vue/pulls";
            var apiService = DependencyManager.Container.Resolve<IPullRequestApiService>();

            //Act
            var response = await apiService.GetPullRequests(url, string.Empty, string.Empty);
            var totalItems = response.ResultSet.Count;

            //Assert
            Assert.True(totalItems > 0);
        }

        //[Theory(DisplayName = "ShouldReturnEqualsPullRequestCountage")]
        //[InlineData(10)]
        //[InlineData(20)]
        //[InlineData(30)]
        //[InlineData(40)]
        //[InlineData(50)]
        //public async void ShouldReturnEqualsPullRequestCountage(int quantity)
        //{
        //    //Arrange
        //    var url = $"repos/vuejs/vue/pulls?per_page{quantity}";
        //    var apiService = DependencyManager.Container.Resolve<IRepositoryApiService>();

        //    //Act
        //    var response = await apiService.GetRepositories(url, string.Empty, string.Empty);
        //    var totalItems = response.Result.Items.Count;

        //    //Assert
        //    Assert.Equal(quantity, totalItems);
        //}

        #endregion


    }
}
