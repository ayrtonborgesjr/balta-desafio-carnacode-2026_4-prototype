using DocumentTemplate.Console.Models;
using DocumentTemplate.Console.Registry;
using DocumentTemplate.Console.Services;

var registry = new TemplateRegistry();

// Registrar template base
registry.Register("service_contract", CreateBaseServiceTemplate());

var service = new DocumentService(registry);

// Criar clones rápidos
for (int i = 1; i <= 5; i++)
{
    var doc = service.CreateDocument(
        "service_contract",
        $"Contrato #{i}",
        $"Cliente {i}"
    );

    service.DisplayTemplate(doc);
}

static DocumentTemplate.Console.Models.DocumentTemplate CreateBaseServiceTemplate()
{
    var template = new DocumentTemplate.Console.Models.DocumentTemplate
    {
        Title = "Contrato Base de Serviço",
        Category = "Contratos",
        Style = new DocumentStyle
        {
            FontFamily = "Arial",
            FontSize = 12,
            HeaderColor = "#003366",
            LogoUrl = "https://empresa.com/logo.png",
            PageMargins = new Margins
            {
                Top = 2,
                Bottom = 2,
                Left = 3,
                Right = 3
            }
        },
        Workflow = new ApprovalWorkflow
        {
            RequiredApprovals = 2,
            TimeoutDays = 5,
            Approvers =
            [
                "gerente@empresa.com",
                "juridico@empresa.com"
            ]
        }
    };

    template.Sections.Add(new Section
    {
        Name = "Cláusula 1 - Objeto",
        Content = "O presente contrato tem por objeto...",
        IsEditable = true
    });

    template.RequiredFields.Add("NomeCliente");
    template.RequiredFields.Add("CPF");

    template.Tags.Add("contrato");
    template.Metadata["Versao"] = "1.0";

    return template;
}