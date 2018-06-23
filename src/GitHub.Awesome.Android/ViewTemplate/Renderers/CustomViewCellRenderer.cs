using System;
using Android.Content;
using Android.Views;
using GitHub.Awesome.Droid.ViewTemplate.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ViewCell), typeof(CustomViewCellRenderer))]
namespace GitHub.Awesome.Droid.ViewTemplate.Renderers
{
	public class CustomViewCellRenderer : ViewCellRenderer
    {
        protected override global::Android.Views.View GetCellCore(Cell item, global::Android.Views.View convertView, ViewGroup parent, Context context)
        {
            var cell = base.GetCellCore(item, convertView, parent, context);

            cell.SetBackgroundResource(Resource.Drawable.cellBackground);

            return cell;
        }
    }
}

