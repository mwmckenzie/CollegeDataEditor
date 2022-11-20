
namespace CollegeDataEditor.Models; 

public class Org: BaseInfoObj {
    
    public string? orgTypeId { get; set; }
    
    public override string TypeName() => "Application";
    
    // public override void Add(SumProgElements elementType, string value) {
    //     base.Add(elementType, value);
    //     switch (elementType) {
    //         case SumProgElements.OrgTypeId:
    //                 orgTypeId = value;
    //                 break;
    //     }
    // }
}