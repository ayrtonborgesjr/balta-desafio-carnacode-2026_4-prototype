using DocumentTemplate.Console.Models;

namespace DocumentTemplate.Tests.Models;

public class SectionTests
{
    [Fact]
    public void Clone_ShouldCreateIndependentCopy()
    {
        // Arrange
        var original = new Section
        {
            Name = "Introduction",
            Content = "This is the introduction.",
            IsEditable = true,
            Placeholders = new List<string> { "{{name}}", "{{date}}" }
        };

        // Act
        var clone = original.Clone();

        // Assert
        Assert.NotNull(clone);
        Assert.NotSame(original, clone);
        Assert.Equal(original.Name, clone.Name);
        Assert.Equal(original.Content, clone.Content);
        Assert.Equal(original.IsEditable, clone.IsEditable);
        Assert.NotSame(original.Placeholders, clone.Placeholders);
        Assert.Equal(original.Placeholders.Count, clone.Placeholders.Count);
    }

    [Fact]
    public void Clone_ModifyingClonePlaceholders_ShouldNotAffectOriginal()
    {
        // Arrange
        var original = new Section
        {
            Name = "Introduction",
            Content = "This is the introduction.",
            IsEditable = true,
            Placeholders = new List<string> { "{{name}}", "{{date}}" }
        };

        // Act
        var clone = original.Clone();
        clone.Placeholders.Add("{{extra}}");
        clone.Name = "Modified";
        clone.Content = "Modified content";
        clone.IsEditable = false;

        // Assert
        Assert.Equal(2, original.Placeholders.Count);
        Assert.Equal(3, clone.Placeholders.Count);
        Assert.Equal("Introduction", original.Name);
        Assert.Equal("Modified", clone.Name);
        Assert.Equal("This is the introduction.", original.Content);
        Assert.Equal("Modified content", clone.Content);
        Assert.True(original.IsEditable);
        Assert.False(clone.IsEditable);
    }

    [Fact]
    public void Section_ShouldInitializeWithEmptyPlaceholders()
    {
        // Arrange & Act
        var section = new Section();

        // Assert
        Assert.NotNull(section.Placeholders);
        Assert.Empty(section.Placeholders);
    }
}

