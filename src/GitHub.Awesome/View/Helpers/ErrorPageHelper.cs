
using GitHub.Awesome.Infra.Common.IoC;
using GitHub.Awesome.View.Pages.Interfaces;
using Xamarin.Forms;

namespace GitHub.Awesome.View.Helpers
{
    /// <summary>
    /// Class responsible for display a custom page of error.
    /// </summary>
    public class ErrorPageHelper
    {                 
        public static void New(INavigation navigation, string notification)
		{
			var errorPage = AppIoC.Container.Resolve<IErrorPage>();
			errorPage.ErrorMessage = notification;

			Device.BeginInvokeOnMainThread(async() => 
			{
				await navigation.PushModalAsync((Page)errorPage);
			});
		}
    }
}
