

using System;
using Android.App;
using Android.Graphics;
using Android.Widget;

namespace GitHub.Awesome.Droid.Utils
{
    public class Font
    {
		/// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="fontFamily"></param>
        public static void ApplyTypeface(TextView view, string fontFamily)
        {
            if (!string.IsNullOrEmpty(fontFamily))
            {
                Typeface typeFace = null;
                try
                {
                    typeFace = Typeface.CreateFromAsset(Application.Context.ApplicationContext.Assets, fontFamily);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Could not load font {fontFamily}: {ex}");
                }

                if (typeFace != null)
                {
                    view.Typeface = typeFace;
                }
            }
        }
    }
}
