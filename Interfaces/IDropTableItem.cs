namespace CollegeDataEditor.Interfaces;

public interface IDropTableItem
{
    public string Name { get; init; }
    public string Selector { get; set; }
    public int Order { get; set; }
}