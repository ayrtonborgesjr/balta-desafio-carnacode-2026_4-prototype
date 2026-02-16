using DocumentTemplate.Console.Registry;
using DocumentTemplate.Console.Models;

namespace DocumentTemplate.Tests.Registry;

public class TemplateRegistryTests
{
    [Fact]
    public void Register_ShouldAddNewTemplate()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var template = CreateSampleTemplate();

        // Act
        registry.Register("test_template", template);

        // Assert
        Assert.True(registry.Exists("test_template"));
    }

    [Fact]
    public void Register_SameKey_ShouldReplaceExistingTemplate()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var template1 = CreateSampleTemplate();
        template1.Title = "First Template";
        var template2 = CreateSampleTemplate();
        template2.Title = "Second Template";

        // Act
        registry.Register("test_template", template1);
        registry.Register("test_template", template2);
        var result = registry.Create("test_template");

        // Assert
        Assert.Equal("Second Template", result.Title);
    }

    [Fact]
    public void Create_ShouldReturnCloneOfTemplate()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var template = CreateSampleTemplate();
        registry.Register("test_template", template);

        // Act
        var clone1 = registry.Create("test_template");
        var clone2 = registry.Create("test_template");

        // Assert
        Assert.NotSame(template, clone1);
        Assert.NotSame(template, clone2);
        Assert.NotSame(clone1, clone2);
    }

    [Fact]
    public void Create_ModifyingClone_ShouldNotAffectOriginalTemplate()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var template = CreateSampleTemplate();
        template.Title = "Original Title";
        registry.Register("test_template", template);

        // Act
        var clone = registry.Create("test_template");
        clone.Title = "Modified Title";
        var anotherClone = registry.Create("test_template");

        // Assert
        Assert.Equal("Original Title", anotherClone.Title);
        Assert.Equal("Modified Title", clone.Title);
    }

    [Fact]
    public void Create_NonExistentKey_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var registry = new TemplateRegistry();

        // Act & Assert
        var exception = Assert.Throws<KeyNotFoundException>(() => registry.Create("non_existent"));
        Assert.Contains("non_existent", exception.Message);
        Assert.Contains("não encontrado", exception.Message);
    }

    [Fact]
    public void Exists_ExistingKey_ShouldReturnTrue()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var template = CreateSampleTemplate();
        registry.Register("test_template", template);

        // Act
        var result = registry.Exists("test_template");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Exists_NonExistentKey_ShouldReturnFalse()
    {
        // Arrange
        var registry = new TemplateRegistry();

        // Act
        var result = registry.Exists("non_existent");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Keys_ShouldReturnAllRegisteredKeys()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var template1 = CreateSampleTemplate();
        var template2 = CreateSampleTemplate();

        // Act
        registry.Register("template1", template1);
        registry.Register("template2", template2);
        var keys = registry.Keys().ToList();

        // Assert
        Assert.Equal(2, keys.Count);
        Assert.Contains("template1", keys);
        Assert.Contains("template2", keys);
    }

    [Fact]
    public void Keys_EmptyRegistry_ShouldReturnEmptyCollection()
    {
        // Arrange
        var registry = new TemplateRegistry();

        // Act
        var keys = registry.Keys().ToList();

        // Assert
        Assert.Empty(keys);
    }

    [Fact]
    public void Register_MultipleTemplates_ShouldMaintainIndependence()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var template1 = CreateSampleTemplate();
        template1.Title = "Template 1";
        var template2 = CreateSampleTemplate();
        template2.Title = "Template 2";

        // Act
        registry.Register("key1", template1);
        registry.Register("key2", template2);
        var result1 = registry.Create("key1");
        var result2 = registry.Create("key2");

        // Assert
        Assert.Equal("Template 1", result1.Title);
        Assert.Equal("Template 2", result2.Title);
        Assert.NotSame(result1, result2);
    }

    private Console.Models.DocumentTemplate CreateSampleTemplate()
    {
        return new Console.Models.DocumentTemplate
        {
            Title = "Sample Template",
            Category = "Test",
            Style = new DocumentStyle
            {
                FontFamily = "Arial",
                FontSize = 12
            },
            Workflow = new ApprovalWorkflow
            {
                RequiredApprovals = 1,
                TimeoutDays = 3,
                Approvers = new List<string> { "test@example.com" }
            }
        };
    }
}

