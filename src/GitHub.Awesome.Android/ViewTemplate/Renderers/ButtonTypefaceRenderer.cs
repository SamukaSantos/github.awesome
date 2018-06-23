

using Android.Content;
using GitHub.Awesome.Droid.ViewTemplate.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(ButtonTypefaceRenderer))]
namespace GitHub.Awesome.Droid.ViewTemplate.Renderers
{
	public class ButtonTypefaceRenderer: ButtonRenderer
    {
        #region Fields

        Context _context;

        #endregion

        #region Constructor

        public ButtonTypefaceRenderer(Context context)
            : base(AppContext.Current)
        {
            _context = context;
        }

        #endregion

        #region Methods


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            Utils.Font.ApplyTypeface(Control, Element.FontFamily);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Utils.Font.ApplyTypeface(Control, Element.FontFamily);
        }

        #endregion
        
    }
}
