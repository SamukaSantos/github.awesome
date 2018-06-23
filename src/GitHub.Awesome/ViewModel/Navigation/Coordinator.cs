using System;

namespace GitHub.Awesome.ViewModel.Navigation
{
    /// <summary>
    /// Coordinator of events in MasterDetailContext. 
    /// </summary>
    public class Coordinator : ObservableObject
    {
        #region Event

        public static event EventHandler<NavigatorEventArgs> SelectedChanged;
        public static event EventHandler<EventArgs> PresentMainMenuOnAppearance;
        public static event EventHandler<NavigatorEventArgs> Selected;
        
        #endregion

        #region Fields

        private static NavigationObject _selected;

        #endregion

        #region Properties

        /// <summary>
        /// Current NavigationObject.
        /// </summary>
        public static NavigationObject NavigationObject
        {
            get
            {
                return _selected;
            }

            set
            {
                if (_selected != value)
                {
                    _selected = value;

                    SelectedChanged?.Invoke(typeof(Coordinator), new NavigatorEventArgs(value));
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method that invoke PresentMainMenuOnAppearance event.
        /// </summary>
        public static void RaisePresentMainMenuOnAppearance()
        {
            PresentMainMenuOnAppearance?.Invoke(typeof(Coordinator), null);
        }

        /// <summary>
        /// Method that invoke Selected event in the navigation proccess.
        /// </summary>
        /// <param name="navigation">Navigation instance.</param>
        public static void RaiseSelected(NavigationObject navigation)
        {
            Selected?.Invoke(typeof(Coordinator), new NavigatorEventArgs(navigation));
        }

        #endregion
    }

    
}
