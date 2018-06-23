
using Android.Content;

namespace GitHub.Awesome.Droid
{
    public class AppContext
    {
		#region Fields

        private static Context _current;

        #endregion

        #region Properties

        public static Context Current { get { return _current; } }

        #endregion

        #region Methods

        public static void Set(Context context)
        {
            _current = context;
        }

        #endregion
    }
}
