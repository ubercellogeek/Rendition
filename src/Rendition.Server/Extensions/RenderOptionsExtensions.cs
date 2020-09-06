using PlaywrightSharp;
using Rendition.Core.Models;

namespace Rendition.Server.Extensions
{
    public static class RenderOptionsExtensions
    {
        public static PdfOptions AsPdfOptions(this RenderOptions options)
        {

            var opts = new PdfOptions();
            opts.Landscape = options.Landscape;
            opts.PreferCSSPageSize = options.PreferCSSPageSize;
            opts.PrintBackground = options.PrintBackground;
            opts.DisplayHeaderFooter = options.DisplayHeaderFooter;

            if (options.Margin == PageMarginType.Custom)
            {
                opts.MarginOptions = new MarginOptions()
                {
                Top = options.MarginTop,
                Bottom = options.MarginBottom,
                Left = options.MarginLeft,
                Right = options.MarginRight
                };
            }

            if (options.Size == PageSizeType.Custom)
            {
                opts.Height = options.Height;
                opts.Width = options.Width;
                opts.Format = null;
            }
            else
            {
                if (options.Size == PageSizeType.A0)
                {
                    opts.Format = PaperFormat.A0;
                }
                else if(options.Size == PageSizeType.A1)
                {
                    opts.Format = PaperFormat.A1;
                }
                else if (options.Size == PageSizeType.A2)
                {
                    opts.Format = PaperFormat.A2;
                }
                else if (options.Size == PageSizeType.A3)
                {
                    opts.Format = PaperFormat.A3;
                }
                else if (options.Size == PageSizeType.A4)
                {
                    opts.Format = PaperFormat.A4;
                }
                else if (options.Size == PageSizeType.A5)
                {
                    opts.Format = PaperFormat.A5;
                }
                else if (options.Size == PageSizeType.Letter)
                {
                    opts.Format = PaperFormat.Letter;
                }
                else if (options.Size == PageSizeType.Legal)
                {
                    opts.Format = PaperFormat.Legal;
                }
                else if (options.Size == PageSizeType.Ledger)
                {
                    opts.Format = PaperFormat.Ledger;
                }
                else if (options.Size == PageSizeType.Tabloid)
                {
                    opts.Format = PaperFormat.Tabloid;
                }
            }

            opts.Scale = options.Scale;

            if (options.Range == PageRangeType.Custom)
            {
                opts.PageRanges = options.PageRange;
            }

            return opts;

        }
    }
}
