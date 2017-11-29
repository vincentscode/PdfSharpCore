PdfSharp.Xamarin.Forms
======================

**PdfSharp.Xamarin.Forms** is a Xamarin.Forms library for **converting any Xamarin.Forms UI into PDF**. It uses [PdfSharp](http://www.pdfsharp.net/), which is a partial port of [PdfSharpCore](https://github.com/groege/PdfSharpCore).

> ### Supported Platforms
> - UWP
> - Android
> - iOS


### Screenshots ([see all](https://github.com/akgulebubekir/PDFSharp.Xamarin.Forms/tree/master/Screenshots))
![App vs PDF](https://raw.githubusercontent.com/akgulebubekir/PDFSharp.Xamarin.Forms/master/Screenshots/table.PNG)


### Usage
> - **Init** : `PdfSharp.Xamarin.Forms.PDFManager.Init(DependencyService.Get<IPDFHandler>())`
> - **Generate** : `var pdf = PDFManager.GeneratePDFFromView(yourView)`
> - **Save** :  `DependencyService.Get<IPdfSave>().Save(pdf, "pdfName.pdf")`


### Features
> - Custom Fonts (You should provide Font Types and font files via `IPDFHandler`)
> - Image rendering
> - Custom renderer ( You can write your own renderer for your customView)
> - Paper size & orientation support


### Limitations
> - Images renders only Jpeg format (It converts PNG to JPEG automatically)
> - ListView does not renders automatically. You should write a renderer.


### Custom PDF Renderer
> Its possible to write your own renderer, it will use it while renderering your View.

 <u>Register :</u> `PDFManager.RegisterRenderer(typeof(Label), typeof(PDFCustomLabelRenderer))`

<u>Render:</u>

```cs
	public class PDFCustomLabelRenderer : PdfRendererBase<Label>
	{
		public override void CreatePDFLayout(XGraphics page, Label label, XRect bounds, double scaleFactor)
		{
			XFont font = new XFont(label.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, label.FontSize * scaleFactor);
			Color textColor = label.TextColor != default(Color) ? label.TextColor : Color.Black;

			if (label.BackgroundColor != default(Color))
				page.DrawRectangle(label.BackgroundColor.ToXBrush(), bounds);

			if (!string.IsNullOrEmpty(label.Text))
				page.DrawString(label.Text, font, textColor.ToXBrush(), bounds,
					new XStringFormat()
					{
						Alignment = label.HorizontalTextAlignment.ToXStringAlignment(),
						LineAlignment = label.VerticalTextAlignment.ToXLineAlignment(),
					});
		}
	}
```