using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using PdfSharp.Xamarin.Forms.Utils;

namespace PdfSharp.Xamarin.Forms
{
    internal class PdfGenerator
    {
        double _scaleFactor;
        XRect _desiredPageSize;
        PageOrientation _orientation;
        PageSize _pageSize;
        View _rootView;

        List<ViewInfo> _viewsToDraw;

        public PdfGenerator(View view, PageOrientation orientation, PageSize pageSize, bool resizeToFit)
        {
            _pageSize = pageSize;
            _orientation = orientation;
            _rootView = view;

            _desiredPageSize = SizeUtils.GetAvailablePageSize(pageSize);

            if (resizeToFit)
                _scaleFactor = _desiredPageSize.Width / view.Bounds.Width;
        }


        public PdfDocument Generate()
        {

            _viewsToDraw = new List<ViewInfo>();
            VisitView(_rootView, new Point(0, 0));

            return CreatePDF(_viewsToDraw);
        }

        private void VisitView(View view, Point pageOffset)
        {
            if (!PdfRenderer.ShouldRenderView(view))
                return;

            Point newOffset = new Point(pageOffset.X + view.X * _scaleFactor, pageOffset.Y + view.Y * _scaleFactor);

            _viewsToDraw.Add(new ViewInfo { View = view, Offset = newOffset });

            if (view is Layout<View>)
            {
                foreach (var v in (view as Layout<View>).Children)
                    VisitView(v, newOffset);
            }

            else if (view is Frame && (view as Frame).Content != null)
            {
                VisitView((view as Frame).Content, newOffset);
            }
            else if (view is ContentView && (view as ContentView).Content != null)
            {
                VisitView((view as ContentView).Content, newOffset);
            }
            else if (view is ScrollView && (view as ScrollView).Content != null)
            {
                VisitView((view as ScrollView).Content, newOffset);
            }
            else if (view is ListView)
            {
                //TODO implement here
            }
        }

        private PdfDocument CreatePDF(List<ViewInfo> views)
        {
            var document = new PdfDocument() { };

            int numberOfPages = (int)Math.Ceiling(_viewsToDraw.Max(x => x.Offset.Y + x.View.HeightRequest * _scaleFactor) / _desiredPageSize.Height);

            for (int i = 0; i < numberOfPages; i++)
            {
                var page = document.AddPage();
                page.Orientation = _orientation;
                page.Size = _pageSize;
                var gfx = XGraphics.FromPdfPage(page, XGraphicsUnit.Millimeter);

                var viewsInPage = _viewsToDraw.Where(x => x.Offset.Y >= i * _desiredPageSize.Height && (x.Offset.Y + x.View.Bounds.Height * _scaleFactor) <= (i + 1) * _desiredPageSize.Height);

                foreach (var v in viewsInPage)
                {
                    var rList = PDFManager.Instance.Renderers.FirstOrDefault(x => x.Key == v.View.GetType());
                    if (rList.Value != null && v.View.Bounds.Width > 0 && v.View.Bounds.Height > 0)
                    {
                        var renderer = Activator.CreateInstance(rList.Value) as Renderers.PdfRendererBase;
                        XRect desiredBounds = new XRect(v.Offset.X + _desiredPageSize.X, v.Offset.Y + _desiredPageSize.Y - (i * _desiredPageSize.Height), v.View.Bounds.Width * _scaleFactor, v.View.Bounds.Height * _scaleFactor);

                        renderer.CreateLayout(gfx, v.View, desiredBounds, _scaleFactor);
                    }
                }
            }

            return document;
        }
    }

    class ViewInfo
    {
        public View View { get; set; }
        public Point Offset { get; set; }
    }
}
