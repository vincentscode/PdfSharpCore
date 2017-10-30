using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms
{
    public class PdfRenderer : BindableObject
    {

        public bool ShouldRender
        {
            get { return (bool)GetValue(ShouldRenderProperty); }
            set { SetValue(ShouldRenderProperty, value); }
        }

        public static readonly BindableProperty ShouldRenderProperty =
            BindableProperty.CreateAttached(nameof(ShouldRender), typeof(bool), typeof(PdfRenderer), true);

        public static bool ShouldRenderView (BindableObject bindable)
        {
            return (bool)bindable.GetValue(ShouldRenderProperty);
        }
    }
}
