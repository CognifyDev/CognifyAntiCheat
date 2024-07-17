namespace CognifyAntiCheat.Config;

public class Config
{
    public string Name { get; }
    public string FileName { get; }

    public Config(string name, string fileName)
    {
        Name = name;
        FileName = fileName;
    }

    protected void ArrangeSettings()
    {
        
    }
}