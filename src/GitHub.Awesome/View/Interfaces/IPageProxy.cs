
using Xamarin.Forms;

namespace GitHub.Awesome.View.Interfaces
{
	public interface IPageProxy : IDialogProxy
    {
        /// <summary>
        ///  Gets the context aware navigation interface for the element.
        /// </summary>
        INavigation Navigation { get; }
    }
}
