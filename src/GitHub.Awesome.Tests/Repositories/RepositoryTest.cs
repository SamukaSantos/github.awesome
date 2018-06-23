
using Bogus;
using GitHub.Awesome.ViewModel.Input;
using ExpectedObjects;
using Xunit;
using GitHub.Awesome.Tests.IoC;
using GitHub.Awesome.Infra.Services.Interfaces;
using GitHub.Awesome.Infra.Common.IoC;

namespace GitHub.Awesome.Tests.Repositories
{
    public class RepositoryTest
    {
        #region Fields

        private string _key;
        private string _name;
        private string _spdxId;
        private string _url;
        private string _nodeId;

        #endregion

        #region Constructor

        public RepositoryTest()
        {
            var faker = new Faker();

            _key    = faker.Random.Uuid().ToString();
            _name   = faker.Random.Word();
            _spdxId = faker.Random.Word();
            _url    = faker.Random.Word();
            _nodeId = faker.Random.Number().ToString();

            TestIoC.Configure();
            AppIoC.SetContainer(DependencyManager.Container);
        }

        #endregion

        #region Methods

        [Fact(DisplayName = "ShouldCreateLicense")]
        public void ShouldCreateLicense()
        {
            //Arrange
            var licenseExpected = new
            {
                Key    = _key,
                Name   = _name,
                SpdxId = _spdxId,
                Url    = _url,
                NodeId = _nodeId
            };

            //Act
            var license = new LicenseViewModel
            {
                Key    = licenseExpected.Key,
                Name   = licenseExpected.Name,
                SpdxId = licenseExpected.SpdxId,
                Url    = licenseExpected.Url,
                NodeId = licenseExpected.NodeId
            };

            //Assert
            licenseExpected.ToExpectedObject().ShouldMatch(license);
        }

        [Fact(DisplayName = "ShouldReturnTotalRepositoriesCountageMoreThanZero")]
        public async void ShouldReturnTotalRepositoriesCountageMoreThanZero()
        {
            //Arrange
            var url        = "search/repositories?q=language:JavaScript&sort=stars&page=1";
            var apiService = DependencyManager.Container.Resolve<IRepositoryApiService>();

            //Act
            var response   = await apiService.GetRepositories(url, string.Empty, string.Empty);
            var totalItems = response.Result.Items.Count;

            //Assert
            Assert.True(totalItems > 0);
        }

        [Theory(DisplayName = "ShouldReturnEqualsRepositoriesCountage")]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        [InlineData(50)]
        [InlineData(60)]
        [InlineData(70)]
        [InlineData(80)]
        [InlineData(90)]
        [InlineData(100)]
        public async void ShouldReturnEqualsRepositoriesCountage(int quantity)
        {
            //Arrange
            var url = $"search/repositories?q=language:JavaScript&sort=stars&page=1&per_page={quantity}";
            var apiService = DependencyManager.Container.Resolve<IRepositoryApiService>();

            //Act
            var response = await apiService.GetRepositories(url, string.Empty, string.Empty);
            var totalItems = response.Result.Items.Count;

            //Assert
            Assert.Equal(quantity, totalItems);
        }

        #endregion
    }
}
