using CollegeDataEditor.Interfaces;

namespace CollegeDataEditor.Models; 

public class Application : DateRangeObj, IIdentifiable {
    
    public List<string> sessionIdList { get; set; } = new();
    
    public override string TypeName() => "Application";

}