
using GitHub.Awesome.View.Pages.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GitHub.Awesome
{
	public partial class App : Application
	{
		#region Constructor

		public App()
		{
			InitializeComponent();

            Infra.Common.IoC.Configuration.Start();

			MainPage = new RootPage();
		}

		#endregion

		#region Methods


		#endregion
	}
}
