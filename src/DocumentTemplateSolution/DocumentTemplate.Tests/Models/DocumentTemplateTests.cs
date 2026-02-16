using DocumentTemplate.Console.Models;
using DtSection = DocumentTemplate.Console.Models.Section;
using DtDocumentTemplate = DocumentTemplate.Console.Models.DocumentTemplate;

namespace DocumentTemplate.Tests.Models;

public class DocumentTemplateTests
{
    [Fact]
    public void Clone_ShouldCreateIndependentCopy()
    {
        // Arrange
        var original = CreateCompleteTemplate();

        // Act
        var clone = original.Clone();

        // Assert
        Assert.NotNull(clone);
        Assert.NotSame(original, clone);
        Assert.Equal(expected: original.Title, actual: clone.Title);
        Assert.Equal(expected: original.Category, actual: clone.Category);
        Assert.NotSame(original.Sections, clone.Sections);
        Assert.NotSame(original.Style, clone.Style);
        Assert.NotSame(original.Workflow, clone.Workflow);
        Assert.NotSame(original.RequiredFields, clone.RequiredFields);
        Assert.NotSame(original.Tags, clone.Tags);
        Assert.NotSame(original.Metadata, clone.Metadata);
    }

    [Fact]
    public void Clone_DeepCopy_ModifyingCloneSections_ShouldNotAffectOriginal()
    {
        // Arrange
        var original = CreateCompleteTemplate();

        // Act
        var clone = original.Clone();
        clone.Sections.Add(new DtSection { Name = "New Section" });
        clone.Sections[0].Content = "Modified content";

        // Assert
        Assert.Equal(1, original.Sections.Count);
        Assert.Equal(2, clone.Sections.Count);
        Assert.Equal(expected: "This is the introduction.", actual: original.Sections[0].Content);
        Assert.Equal(expected: "Modified content", actual: clone.Sections[0].Content);
    }

    [Fact]
    public void Clone_DeepCopy_ModifyingCloneRequiredFields_ShouldNotAffectOriginal()
    {
        // Arrange
        var original = CreateCompleteTemplate();

        // Act
        var clone = original.Clone();
        clone.RequiredFields.Add("NewField");

        // Assert
        Assert.Equal(2, original.RequiredFields.Count);
        Assert.Equal(3, clone.RequiredFields.Count);
        Assert.DoesNotContain<string>("NewField", original.RequiredFields);
        Assert.Contains<string>("NewField", clone.RequiredFields);
    }

    [Fact]
    public void Clone_DeepCopy_ModifyingCloneTags_ShouldNotAffectOriginal()
    {
        // Arrange
        var original = CreateCompleteTemplate();

        // Act
        var clone = original.Clone();
        clone.Tags.Add("newtag");

        // Assert
        Assert.Single(original.Tags);
        Assert.Equal(2, clone.Tags.Count);
        Assert.DoesNotContain<string>("newtag", original.Tags);
        Assert.Contains<string>("newtag", clone.Tags);
    }

    [Fact]
    public void Clone_DeepCopy_ModifyingCloneMetadata_ShouldNotAffectOriginal()
    {
        // Arrange
        var original = CreateCompleteTemplate();

        // Act
        var clone = original.Clone();
        clone.Metadata["NewKey"] = "NewValue";
        clone.Metadata["Version"] = "2.0";

        // Assert
        Assert.Single(original.Metadata);
        Assert.Equal(expected: "1.0", actual: original.Metadata["Version"]);
        Assert.Equal(2, clone.Metadata.Count);
        Assert.Equal(expected: "2.0", actual: clone.Metadata["Version"]);
        Assert.False(condition: original.Metadata.ContainsKey("NewKey"));
        Assert.True(condition: clone.Metadata.ContainsKey("NewKey"));
    }

    [Fact]
    public void Clone_DeepCopy_ModifyingCloneStyle_ShouldNotAffectOriginal()
    {
        // Arrange
        var original = CreateCompleteTemplate();

        // Act
        var clone = original.Clone();
        clone.Style.FontFamily = "Times New Roman";
        clone.Style.FontSize = 14;

        // Assert
        Assert.Equal(expected: "Arial", actual: original.Style.FontFamily);
        Assert.Equal(12, original.Style.FontSize);
        Assert.Equal(expected: "Times New Roman", actual: clone.Style.FontFamily);
        Assert.Equal(14, clone.Style.FontSize);
    }

    [Fact]
    public void Clone_DeepCopy_ModifyingCloneWorkflow_ShouldNotAffectOriginal()
    {
        // Arrange
        var original = CreateCompleteTemplate();

        // Act
        var clone = original.Clone();
        clone.Workflow.RequiredApprovals = 5;
        clone.Workflow.Approvers.Add("ceo@example.com");

        // Assert
        Assert.Equal(2, original.Workflow.RequiredApprovals);
        Assert.Equal(2, original.Workflow.Approvers.Count);
        Assert.Equal(5, clone.Workflow.RequiredApprovals);
        Assert.Equal(3, clone.Workflow.Approvers.Count);
    }

    [Fact]
    public void Clone_WithNullStyle_ShouldNotThrowException()
    {
        // Arrange
        var original = new DtDocumentTemplate
        {
            Title = "Test Template",
            Style = null
        };

        // Act
        var clone = original.Clone();

        // Assert
        Assert.NotNull(clone);
        Assert.Null(clone.Style);
    }

    [Fact]
    public void Clone_WithNullWorkflow_ShouldNotThrowException()
    {
        // Arrange
        var original = new DtDocumentTemplate
        {
            Title = "Test Template",
            Workflow = null
        };

        // Act
        var clone = original.Clone();

        // Assert
        Assert.NotNull(clone);
        Assert.Null(clone.Workflow);
    }

    [Fact]
    public void DocumentTemplate_ShouldInitializeWithEmptyCollections()
    {
        // Arrange & Act
        var template = new Console.Models.DocumentTemplate();

        // Assert
        Assert.NotNull(template.Sections);
        Assert.Empty(template.Sections);
        Assert.NotNull(template.RequiredFields);
        Assert.Empty(template.RequiredFields);
        Assert.NotNull(template.Metadata);
        Assert.Empty(template.Metadata);
        Assert.NotNull(template.Tags);
        Assert.Empty(template.Tags);
    }

    private DtDocumentTemplate CreateCompleteTemplate()
    {
        return new DtDocumentTemplate
        {
            Title = "Service Contract",
            Category = "Contracts",
            Style = new DocumentStyle
            {
                FontFamily = "Arial",
                FontSize = 12,
                HeaderColor = "#003366",
                LogoUrl = "https://example.com/logo.png",
                PageMargins = new Margins { Top = 1, Bottom = 2, Left = 3, Right = 4 }
            },
            Workflow = new ApprovalWorkflow
            {
                RequiredApprovals = 2,
                TimeoutDays = 5,
                Approvers = new List<string> { "manager@example.com", "legal@example.com" }
            },
            Sections = new List<DtSection>
            {
                new DtSection
                {
                    Name = "Introduction",
                    Content = "This is the introduction.",
                    IsEditable = true,
                    Placeholders = new List<string> { "{{name}}", "{{date}}" }
                }
            },
            RequiredFields = new List<string> { "ClientName", "CPF" },
            Tags = new List<string> { "contract" },
            Metadata = new Dictionary<string, string> { { "Version", "1.0" } }
        };
    }
}

