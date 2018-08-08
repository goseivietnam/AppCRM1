using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppCRM.Controls;
using AppCRM.iOS.Renderer;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SJTabbedPage), typeof(SJTabbedPageRenderer))]
namespace AppCRM.iOS.Renderer
{
    public class SJTabbedPageRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            TabBar.UnselectedItemTintColor = UIColor.FromRGB(210, 210, 210);
        }
    }
}