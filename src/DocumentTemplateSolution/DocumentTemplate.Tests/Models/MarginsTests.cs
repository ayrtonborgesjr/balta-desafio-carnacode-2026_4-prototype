using DocumentTemplate.Console.Models;

namespace DocumentTemplate.Tests.Models;

public class MarginsTests
{
    [Fact]
    public void Clone_ShouldCreateIndependentCopy()
    {
        // Arrange
        var original = new Margins
        {
            Top = 1,
            Bottom = 2,
            Left = 3,
            Right = 4
        };

        // Act
        var clone = original.Clone();

        // Assert
        Assert.NotNull(clone);
        Assert.NotSame(original, clone);
        Assert.Equal(original.Top, clone.Top);
        Assert.Equal(original.Bottom, clone.Bottom);
        Assert.Equal(original.Left, clone.Left);
        Assert.Equal(original.Right, clone.Right);
    }

    [Fact]
    public void Clone_ModifyingClone_ShouldNotAffectOriginal()
    {
        // Arrange
        var original = new Margins
        {
            Top = 1,
            Bottom = 2,
            Left = 3,
            Right = 4
        };

        // Act
        var clone = original.Clone();
        clone.Top = 10;
        clone.Bottom = 20;
        clone.Left = 30;
        clone.Right = 40;

        // Assert
        Assert.Equal(1, original.Top);
        Assert.Equal(2, original.Bottom);
        Assert.Equal(3, original.Left);
        Assert.Equal(4, original.Right);
        Assert.Equal(10, clone.Top);
        Assert.Equal(20, clone.Bottom);
        Assert.Equal(30, clone.Left);
        Assert.Equal(40, clone.Right);
    }

    [Fact]
    public void Margins_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var margins = new Margins();

        // Assert
        Assert.Equal(0, margins.Top);
        Assert.Equal(0, margins.Bottom);
        Assert.Equal(0, margins.Left);
        Assert.Equal(0, margins.Right);
    }
}

