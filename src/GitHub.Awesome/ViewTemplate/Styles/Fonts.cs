
using Xamarin.Forms;

namespace GitHub.Awesome.ViewTemplate.Styles
{
    public class Fonts
    {
        /// <summary>
        /// Get the font based on the current platform.
        /// </summary>
        public static string FontAwesome
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        return "fontawesome";
                    default:
                        return "fontawesome.ttf";
                }
            }
        }
    }
}
