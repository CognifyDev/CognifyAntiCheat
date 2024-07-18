using System.IO;
using System.Text;
using CognifyAntiCheat.Utils;

namespace CognifyAntiCheat.Config;

public class Config
{
    public const string DataDirectoryName = $"{Main.DisplayName}_DATA";
    
    public string Name { get; }
    public string FileName { get; }
    
    public string Text { get; }

    private readonly string _path;

    public Yaml? YamlReader { get; private set; }

    public Config(string name, string fileName)
    {
        Name = name;
        FileName = fileName;
        _path = DataDirectoryName + "/" + FileName;
        var resourceFile = new ResourceFile($"CognifyAntiCheat.Resources.InDLL.Config.{FileName}");
        Text = resourceFile.GetResourcesText();
    }

    protected virtual void ArrangeSettings()
    {
        LoadConfig();
    }
    
    protected void LoadConfig(bool replace = false)
    {
        if (!Directory.Exists(DataDirectoryName)) Directory.CreateDirectory(DataDirectoryName);
        if (!File.Exists(_path) || replace)
        {
            if (replace && File.Exists(_path)) File.Delete(_path);
            File.WriteAllText(_path, Text, Encoding.Unicode);
        }

        var text = File.ReadAllText(_path, Encoding.Unicode);
        YamlReader = Yaml.LoadFromString(text);
    }
}