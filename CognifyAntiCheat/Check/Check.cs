namespace CognifyAntiCheat.Check;

public class Check
{
    public Check(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public string Description { get; }
    
    private int _violations;
    
    public int MaxViolations { get; }

    protected void Fail()
    {
        _violations ++;
    }
}