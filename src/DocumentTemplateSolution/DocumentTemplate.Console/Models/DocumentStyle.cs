using DocumentTemplate.Console.Interfaces;

namespace DocumentTemplate.Console.Models;

public class DocumentStyle : IPrototype<DocumentStyle>
{
    public string FontFamily { get; set; }
    public int FontSize { get; set; }
    public string HeaderColor { get; set; }
    public string LogoUrl { get; set; }
    public Margins PageMargins { get; set; }

    public DocumentStyle Clone()
    {
        return new DocumentStyle
        {
            FontFamily = FontFamily,
            FontSize = FontSize,
            HeaderColor = HeaderColor,
            LogoUrl = LogoUrl,
            PageMargins = PageMargins?.Clone()
        };
    }
}