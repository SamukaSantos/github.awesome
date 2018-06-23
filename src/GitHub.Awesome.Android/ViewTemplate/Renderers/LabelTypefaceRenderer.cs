
using System;
using GitHub.Awesome.Droid.ViewTemplate.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(LabelTypefaceRenderer))]
namespace GitHub.Awesome.Droid.ViewTemplate.Renderers
{
	public class LabelTypefaceRenderer: LabelRenderer
    {
        #region Construtor

        public LabelTypefaceRenderer()
            : base(AppContext.Current) { }

        #endregion

        #region Métodos

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            
            Utils.Font.ApplyTypeface(Control, Element.FontFamily);
        }

        #endregion
    }
}
