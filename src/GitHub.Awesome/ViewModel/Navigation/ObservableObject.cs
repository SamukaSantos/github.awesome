
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GitHub.Awesome.ViewModel.Navigation
{
    /// <summary>
    /// Base class for the navigation objects.
    /// </summary>
    public class ObservableObject: INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        //protected void NotifyAllPropertiesChanged()
        //{
        //    NotifyPropertyChanged(null);
        //}

        /// <summary>
        /// Set a new value to the property.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="field">Current field reference.</param>
        /// <param name="value">New value.</param>
        /// <param name="propertyName">Property name</param>

        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName]string propertyName = "")
        {
			if (EqualityComparer<T>.Default.Equals(property, value))
            {
                return false;
            }

			property = value;
            NotifyPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Set a new value to the property.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
        #endregion
    }
}
