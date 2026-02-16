using DocumentTemplate.Console.Interfaces;

namespace DocumentTemplate.Console.Models;

public class DocumentTemplate : IPrototype<DocumentTemplate>
{
    public string Title { get; set; }
    public string Category { get; set; }
    public List<Section> Sections { get; set; } = new();
    public DocumentStyle Style { get; set; }
    public List<string> RequiredFields { get; set; } = new();
    public Dictionary<string, string> Metadata { get; set; } = new();
    public ApprovalWorkflow Workflow { get; set; }
    public List<string> Tags { get; set; } = new();

    public DocumentTemplate Clone()
    {
        return new DocumentTemplate
        {
            Title = Title,
            Category = Category,
            Style = Style?.Clone(),
            Workflow = Workflow?.Clone(),
            RequiredFields = new List<string>(RequiredFields),
            Tags = new List<string>(Tags),
            Metadata = new Dictionary<string, string>(Metadata),
            Sections = Sections.Select(s => s.Clone()).ToList()
        };
    }
}