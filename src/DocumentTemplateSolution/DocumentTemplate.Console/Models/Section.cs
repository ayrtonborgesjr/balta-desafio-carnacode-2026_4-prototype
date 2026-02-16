using DocumentTemplate.Console.Interfaces;

namespace DocumentTemplate.Console.Models;

public class Section : IPrototype<Section>
{
    public string Name { get; set; }
    public string Content { get; set; }
    public bool IsEditable { get; set; }
    public List<string> Placeholders { get; set; } = new();
        
    public Section Clone()
    {
        return new Section
        {
            Name = Name,
            Content = Content,
            IsEditable = IsEditable,
            Placeholders = new List<string>(Placeholders)
        };
    }
}