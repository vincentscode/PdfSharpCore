using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using PdfSharpCore.Fonts;
using PdfSharpCore.Drawing;
using PdfSharp.Xamarin.Forms.Attributes;

namespace PdfSharp.Xamarin.Forms.Renderers
{
    [PdfRenderer(ViewType = typeof(DatePicker))]
    public class PdfDatePickerRenderer : PdfRendererBase<DatePicker>
    {
        public override void CreatePDFLayout(XGraphics page, DatePicker datePicker, XRect bounds, double scaleFactor)
        {
            XFont font = new XFont(GlobalFontSettings.FontResolver.DefaultFontName, 14 * scaleFactor);

            string format = datePicker.Format;
            if (string.IsNullOrEmpty(format))
                format = "MM.DD.YYYY";

            page.DrawString(datePicker.Date.ToString(format), font, datePicker.TextColor.ToXBrush(), bounds);

        }
    }
}
