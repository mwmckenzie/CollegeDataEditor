namespace CollegeDataEditor.Models;

public class SchoolDisplay {

    public string name { get; set; } = string.Empty;
    public string city { get; set; } = string.Empty;
    public string state { get; set; } = string.Empty;
    public string zip { get; set; } = string.Empty;
    public string accreditor { get; set; } = string.Empty;
    public string schoolUrl { get; set; } = string.Empty;
    public string priceCalculatorUrl { get; set; } = string.Empty;
    public string degreesAwardedPredominantRecoded { get; set; } = string.Empty;
    public string underInvestigation { get; set; } = string.Empty;
    public string mainCampus { get; set; } = string.Empty;
    public string degreesAwardedPredominant { get; set; } = string.Empty;
    public string degreesAwardedHighest { get; set; } = string.Empty;
    public string ownership { get; set; } = string.Empty;
    public string stateFips { get; set; } = string.Empty;
    public string regionId { get; set; } = string.Empty;
    public string locale { get; set; } = string.Empty;
    public string carnegieBasic { get; set; } = string.Empty;
    public string carnegieUndergrad { get; set; } = string.Empty;
    public string carnegieSizeSetting { get; set; } = string.Empty;
    public string genderRestriction { get; set; } = string.Empty;
    public string religiousAffiliation { get; set; } = string.Empty;
    public string institutionalCharacteristicsLevel { get; set; } = string.Empty;
    public string openAdmissionsPolicy { get; set; } = string.Empty;
    public string accreditorCode { get; set; } = string.Empty;
    public string titleIvApprovalDate { get; set; } = string.Empty;
    public string ownershipPeps { get; set; } = string.Empty;
    public string titleIvEligibilityType { get; set; } = string.Empty;
    public string onlineOnly { get; set; } = string.Empty;
    public string operating { get; set; } = string.Empty;
    public string alias { get; set; } = string.Empty;
    public string ope6Id { get; set; } = string.Empty;
    public string ope8Id { get; set; } = string.Empty;
        
    public int id { get; set; }
    public int tuitionRevenuePerFte { get; set; }
    public int instructionalExpenditurePerFte { get; set; }
    public int facultySalary { get; set; }
    public int branches { get; set; }
    
    public double schoolFtFacultyRate { get; set; }
    
    public bool menOnly { get; set; }
    public bool womenOnly { get; set; }
    
    public List<string> aliasList { get; set; } = new List<string>();
    public string? aliasListToString => aliasList.Count > 0 
        ? aliasList?.Aggregate((i, j) => $"{i.Trim()}\n{j.Trim()}")
        : string.Empty;
}