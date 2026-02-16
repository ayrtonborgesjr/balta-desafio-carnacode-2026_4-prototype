using DocumentTemplate.Console.Models;

namespace DocumentTemplate.Tests.Models;

public class DocumentStyleTests
{
    [Fact]
    public void Clone_ShouldCreateIndependentCopy()
    {
        // Arrange
        var original = new DocumentStyle
        {
            FontFamily = "Arial",
            FontSize = 12,
            HeaderColor = "#003366",
            LogoUrl = "https://example.com/logo.png",
            PageMargins = new Margins { Top = 1, Bottom = 2, Left = 3, Right = 4 }
        };

        // Act
        var clone = original.Clone();

        // Assert
        Assert.NotNull(clone);
        Assert.NotSame(original, clone);
        Assert.Equal(original.FontFamily, clone.FontFamily);
        Assert.Equal(original.FontSize, clone.FontSize);
        Assert.Equal(original.HeaderColor, clone.HeaderColor);
        Assert.Equal(original.LogoUrl, clone.LogoUrl);
        Assert.NotNull(clone.PageMargins);
        Assert.NotSame(original.PageMargins, clone.PageMargins);
    }

    [Fact]
    public void Clone_ModifyingClone_ShouldNotAffectOriginal()
    {
        // Arrange
        var original = new DocumentStyle
        {
            FontFamily = "Arial",
            FontSize = 12,
            HeaderColor = "#003366",
            LogoUrl = "https://example.com/logo.png",
            PageMargins = new Margins { Top = 1, Bottom = 2, Left = 3, Right = 4 }
        };

        // Act
        var clone = original.Clone();
        clone.FontFamily = "Times New Roman";
        clone.FontSize = 14;
        clone.HeaderColor = "#FF0000";
        clone.LogoUrl = "https://newsite.com/newlogo.png";
        clone.PageMargins.Top = 10;

        // Assert
        Assert.Equal("Arial", original.FontFamily);
        Assert.Equal(12, original.FontSize);
        Assert.Equal("#003366", original.HeaderColor);
        Assert.Equal("https://example.com/logo.png", original.LogoUrl);
        Assert.Equal(1, original.PageMargins.Top);
        Assert.Equal("Times New Roman", clone.FontFamily);
        Assert.Equal(14, clone.FontSize);
        Assert.Equal("#FF0000", clone.HeaderColor);
        Assert.Equal("https://newsite.com/newlogo.png", clone.LogoUrl);
        Assert.Equal(10, clone.PageMargins.Top);
    }

    [Fact]
    public void Clone_WithNullPageMargins_ShouldNotThrowException()
    {
        // Arrange
        var original = new DocumentStyle
        {
            FontFamily = "Arial",
            FontSize = 12,
            PageMargins = null
        };

        // Act
        var clone = original.Clone();

        // Assert
        Assert.NotNull(clone);
        Assert.Null(clone.PageMargins);
    }
}

