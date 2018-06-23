
using System;

namespace GitHub.Awesome.ViewModel.Navigation
{
    /// <summary>
    /// Navigation argument class.
    /// </summary>
	public class NavigatorEventArgs : EventArgs
    {
        public NavigatorEventArgs(NavigationObject @object)
        {
            Object = @object;
        }

        public NavigationObject Object { get; }
    }
}
