using System;
using Rendition.Core.Models;

namespace Rendition.Client.Models
{
    public class RenderOptionsViewModel : RenderOptions
    {
        public string MarginStringValue
        {
            get
            {
                return base.Margin.ToString();
            }
            set
            {
                PageMarginType t;

                if(Enum.TryParse<PageMarginType>(value, true, out t))
                {
                    base.Margin = t;
                }
                else
                {
                    throw new ArgumentException($"Invalid PageMarginType supplied: '{value}'");
                }
            }
        }

        public string RangeStringValue
        {
            get
            {
                return base.Range.ToString();
            }
            set
            {
                PageRangeType t;

                if(Enum.TryParse<PageRangeType>(value, true, out t))
                {
                    base.Range = t;
                }
                else
                {
                    throw new ArgumentException($"Invalid PageRangeType supplied: '{value}'");
                }
            }
        }

        public string SizeStringValue
        {
            get
            {
                return base.Size.ToString();
            }
            set
            {
                PageSizeType t;

                if(Enum.TryParse<PageSizeType>(value, true, out t))
                {
                    base.Size = t;
                }
                else
                {
                    throw new ArgumentException($"Invalid PageSizeType supplied: '{value}'");
                }
            }
        }

    }
}
