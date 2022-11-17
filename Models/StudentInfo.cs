

using CollegeDataEditor.Enums;

namespace CollegeDataEditor.Models; 

public class StudentInfo : SumProgComponent {
    
    public List<string> sessionIdList { get; set; } = new();
    
    public int ageMin { get; set; }
    public int ageMax { get; set; }
    public int classYearMin { get; set; }
    public int classYearMax { get; set; }

    public List<string> citizenshipIdList { get; set; } = new();
    public List<string> residenceIdList { get; set; } = new();

    public override void Add(SumProgElements elementType, string value) {
        base.Add(elementType, value);
        switch (elementType) {
            case SumProgElements.CitizenshipId:
                if (!citizenshipIdList.Contains(value)) {
                    citizenshipIdList.Add(value);
                }
                break;
            case SumProgElements.ResidenceId:
                if (!residenceIdList.Contains(value)) {
                    residenceIdList.Add(value);
                }
                break;
        }
        
    }
    
}