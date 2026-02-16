using DocumentTemplate.Console.Models;
using DocumentTemplate.Console.Registry;
using DocumentTemplate.Console.Services;

namespace DocumentTemplate.Tests.Services;

public class DocumentServiceTests
{
    [Fact]
    public void CreateDocument_ShouldReturnClonedTemplate()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var baseTemplate = CreateSampleTemplate();
        baseTemplate.Title = "Base Template";
        registry.Register("test_key", baseTemplate);
        var service = new DocumentService(registry);

        // Act
        var document = service.CreateDocument("test_key", "New Title", "John Doe");

        // Assert
        Assert.NotNull(document);
        Assert.Equal("New Title", document.Title);
    }

    [Fact]
    public void CreateDocument_ShouldCustomizeTitle()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var baseTemplate = CreateSampleTemplate();
        registry.Register("test_key", baseTemplate);
        var service = new DocumentService(registry);

        // Act
        var document = service.CreateDocument("test_key", "Custom Title", "Client Name");

        // Assert
        Assert.Equal("Custom Title", document.Title);
    }

    [Fact]
    public void CreateDocument_ShouldAddClientNameToMetadata()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var baseTemplate = CreateSampleTemplate();
        registry.Register("test_key", baseTemplate);
        var service = new DocumentService(registry);

        // Act
        var document = service.CreateDocument("test_key", "Title", "John Doe");

        // Assert
        Assert.True(document.Metadata.ContainsKey("Cliente"));
        Assert.Equal("John Doe", document.Metadata["Cliente"]);
    }

    [Fact]
    public void CreateDocument_ShouldAddCreatedAtToMetadata()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var baseTemplate = CreateSampleTemplate();
        registry.Register("test_key", baseTemplate);
        var service = new DocumentService(registry);

        // Act
        var before = DateTime.Now;
        var document = service.CreateDocument("test_key", "Title", "Client Name");
        var after = DateTime.Now;

        // Assert
        Assert.True(document.Metadata.ContainsKey("CriadoEm"));
        var createdAt = DateTime.Parse(document.Metadata["CriadoEm"]);
        Assert.True(createdAt >= before.AddSeconds(-1) && createdAt <= after.AddSeconds(1));
    }

    [Fact]
    public void CreateDocument_ShouldPreserveOriginalTemplateData()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var baseTemplate = CreateSampleTemplate();
        baseTemplate.Category = "Original Category";
        baseTemplate.RequiredFields.Add("Field1");
        registry.Register("test_key", baseTemplate);
        var service = new DocumentService(registry);

        // Act
        var document = service.CreateDocument("test_key", "New Title", "Client");

        // Assert
        Assert.Equal("Original Category", document.Category);
        Assert.Contains("Field1", document.RequiredFields);
    }

    [Fact]
    public void CreateDocument_MultipleDocuments_ShouldBeIndependent()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var baseTemplate = CreateSampleTemplate();
        registry.Register("test_key", baseTemplate);
        var service = new DocumentService(registry);

        // Act
        var doc1 = service.CreateDocument("test_key", "Document 1", "Client 1");
        var doc2 = service.CreateDocument("test_key", "Document 2", "Client 2");

        // Assert
        Assert.NotSame(doc1, doc2);
        Assert.Equal("Document 1", doc1.Title);
        Assert.Equal("Document 2", doc2.Title);
        Assert.Equal("Client 1", doc1.Metadata["Cliente"]);
        Assert.Equal("Client 2", doc2.Metadata["Cliente"]);
    }

    [Fact]
    public void CreateDocument_NonExistentTemplate_ShouldThrowException()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var service = new DocumentService(registry);

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => 
            service.CreateDocument("non_existent", "Title", "Client"));
    }

    [Fact]
    public void DisplayTemplate_ShouldNotThrowException()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var template = CreateSampleTemplate();
        template.Title = "Test Template";
        template.Category = "Test Category";
        template.Sections.Add(new Section { Name = "Section 1" });
        template.RequiredFields.Add("Field1");
        template.Workflow.Approvers.Add("approver@example.com");
        registry.Register("test_key", template);
        var service = new DocumentService(registry);
        var document = service.CreateDocument("test_key", "Title", "Client");

        // Act & Assert - Should not throw
        var exception = Record.Exception(() => service.DisplayTemplate(document));
        Assert.Null(exception);
    }

    [Fact]
    public void CreateDocument_WithExistingMetadata_ShouldPreserveAndAddNew()
    {
        // Arrange
        var registry = new TemplateRegistry();
        var baseTemplate = CreateSampleTemplate();
        baseTemplate.Metadata["Version"] = "1.0";
        baseTemplate.Metadata["Author"] = "System";
        registry.Register("test_key", baseTemplate);
        var service = new DocumentService(registry);

        // Act
        var document = service.CreateDocument("test_key", "Title", "John Doe");

        // Assert
        Assert.Equal("1.0", document.Metadata["Version"]);
        Assert.Equal("System", document.Metadata["Author"]);
        Assert.Equal("John Doe", document.Metadata["Cliente"]);
        Assert.True(document.Metadata.ContainsKey("CriadoEm"));
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
                FontSize = 12,
                HeaderColor = "#000000",
                LogoUrl = "https://example.com/logo.png"
            },
            Workflow = new ApprovalWorkflow
            {
                RequiredApprovals = 1,
                TimeoutDays = 3,
                Approvers = new List<string>()
            },
            Sections = new List<Section>(),
            RequiredFields = new List<string>(),
            Tags = new List<string>(),
            Metadata = new Dictionary<string, string>()
        };
    }
}

