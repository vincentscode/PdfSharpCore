using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Xamarin.Forms;

using PdfSharpCore;
using PdfSharpCore.Pdf;
using PdfSharpCore.Fonts;
using PdfSharp.Xamarin.Forms.Utils;
using PdfSharp.Xamarin.Forms.Contracts;

namespace PdfSharp.Xamarin.Forms
{
    public class PDFManager
    {
        internal static PDFManager Instance { get; private set; }

        internal IPDFHandler Handler { get; private set; }

        private PDFManager()
        {

        }

        internal Dictionary<Type, Type> Renderers { get; set; }

        public static void Init(IPDFHandler handler, ICustomFontProvider customFontProvider = null)
        {
            if (Instance == null)
                Instance = new PDFManager();

            GlobalFontSettings.FontResolver = new FontProvider(customFontProvider);

            Instance.Handler = handler;
            Instance.Renderers = new Dictionary<Type, Type>();

            //register all predefined renderers
            var assembly = typeof(PDFManager).GetTypeInfo().Assembly;
            foreach (var typeInfo in assembly.DefinedTypes)
            {
                if (typeInfo.IsDefined(typeof(Attributes.PdfRendererAttribute), false))
                {
                    var rInfo = typeInfo.GetCustomAttribute<Attributes.PdfRendererAttribute>();
                    if (Instance.Renderers.ContainsKey(rInfo.ViewType))
                        Instance.Renderers[rInfo.ViewType] = typeInfo.AsType();
                    else
                        Instance.Renderers.Add(rInfo.ViewType, typeInfo.AsType());
                }
            }
        }

        public static void RegisterRenderer(Type viewType, Type rendererType)
        {
            if (Instance == null)
                throw new InvalidOperationException("You must Call Init firsts");

            if (rendererType.GetTypeInfo().BaseType != typeof(Renderers.PdfRendererBase))
                throw new ArgumentException("Renderertype Must inherit PdfRenderdererBase<View>");

            if (Instance.Renderers.ContainsKey(viewType))
                Instance.Renderers[viewType] = rendererType;
            else Instance.Renderers[viewType] = rendererType;
        }

        public static PdfDocument GeneratePDFFromView(View view, PageOrientation orientaiton = PageOrientation.Portrait, PageSize size = PageSize.A4, bool resizeToFit = true)
        {
            if (Instance == null)
                throw new InvalidOperationException("You must Call Init first");

            PdfGenerator generator = new PdfGenerator(view, orientaiton, size, resizeToFit);
            var pdf = generator.Generate();

            //global::Xamarin.Forms.DependencyService.Get<IPDFSave>().SavePDF(pdf);
            return pdf;
        }

    }
}
