namespace CollegeDataEditor.Models; 

public class Session : DateRangeObj {
    public List<string> applicationIdList { get; set; } = new();

    public override string TypeName() => "Session";
}