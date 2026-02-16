using DocumentTemplate.Console.Registry;

namespace DocumentTemplate.Console.Services;

public class DocumentService(TemplateRegistry registry)
{
    public Models.DocumentTemplate CreateDocument(
        string templateKey,
        string title,
        string clientName)
    {
        var document = registry.Create(templateKey);

        // Personalizações específicas
        document.Title = title;
        document.Metadata["Cliente"] = clientName;
        document.Metadata["CriadoEm"] = DateTime.Now.ToString("s");

        return document;
    }

    public void DisplayTemplate(Models.DocumentTemplate template)
    {
        System.Console.WriteLine($"\n=== {template.Title} ===");
        System.Console.WriteLine($"Categoria: {template.Category}");
        System.Console.WriteLine($"Seções: {template.Sections.Count}");
        System.Console.WriteLine($"Campos obrigatórios: {string.Join(", ", template.RequiredFields)}");
        System.Console.WriteLine($"Aprovadores: {string.Join(", ", template.Workflow.Approvers)}");
    }
}