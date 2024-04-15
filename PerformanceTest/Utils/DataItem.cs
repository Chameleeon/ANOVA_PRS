
namespace PerformanceTest;
public class DataItem
{
    public string Name { get; set; }
    public string Value1 { get; set; }
    public string? Value2 { get; set; }
    public string? Value3 { get; set; }

    public DataItem(string name, string value1, string? value2 = null, string? value3 = null)
    {
        Name = name;
        Value1 = value1;
        Value2 = value2;
        Value3 = value3;
    }
}
