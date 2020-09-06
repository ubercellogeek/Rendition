namespace Rendition.Core.Models
{
    public class RenderOptions
    {
        public RenderOptions()
        {

        }
        
        private PageMarginType _margin = PageMarginType.Default;
        private PageRangeType _range = PageRangeType.All;
        private PageSizeType _size = PageSizeType.Letter;

        public bool DisplayHeaderFooter { get; set; }
        public bool Landscape { get; set; }
        public bool PrintBackground { get; set; }
        public bool PreferCSSPageSize { get; set; }
        public string PageRange { get; set; }
        public double Scale { get; set; } = 1;
        public string Width { get; set; }
        public string Height { get; set; }
        public PageMarginType Margin 
        {
            get 
            {
                return _margin;
            }
            set
            {
                _margin = value;

                if(_margin == PageMarginType.Default)
                {
                    MarginBottom = default;
                    MarginTop = default;
                    MarginLeft = default;
                    MarginRight = default;
                }
            }
        }
        public string MarginTop { get; set; }
        public string MarginBottom { get; set; }
        public string MarginLeft { get; set; }
        public string MarginRight { get; set; }
        public PageRangeType Range
        {
            get 
            {
                return _range;
            }
            set
            {
                _range = value;

                if(_range == PageRangeType.All)
                {
                    PageRange = default;
                }
            }
        }

        public PageSizeType Size
        {
            get 
            {
                return _size;
            }
            set
            {
                _size = value;

                if(_size != PageSizeType.Custom)
                {
                    Width = default;
                    Height = default;
                }
            }
        }
    }

    public enum PageSizeType
    {
        A0,
        A1,
        A2,
        A3,
        A4,
        A5,
        Custom,
        Ledger,
        Legal,
        Letter,
        Tabloid
    }

    public enum PageMarginType
    {
        Default,
        Custom
    }

    public enum PageRangeType
    {
        All,
        Custom
    }
}
