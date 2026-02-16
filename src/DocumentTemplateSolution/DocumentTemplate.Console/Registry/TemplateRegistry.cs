namespace DocumentTemplate.Console.Registry;

public class TemplateRegistry
{
    private readonly Dictionary<string, Models.DocumentTemplate> _templates = new();

    public void Register(string key, Models.DocumentTemplate template)
    {
        if (_templates.ContainsKey(key))
            _templates[key] = template;
        else
            _templates.Add(key, template);
    }

    public Models.DocumentTemplate Create(string key)
    {
        if (!_templates.ContainsKey(key))
            throw new KeyNotFoundException($"Template '{key}' não encontrado.");

        return _templates[key].Clone();
    }

    public bool Exists(string key)
    {
        return _templates.ContainsKey(key);
    }

    public IEnumerable<string> Keys()
    {
        return _templates.Keys;
    }
}