using System.Text.Json.Serialization;
using CollegeDataEditor.Interfaces;

namespace CollegeDataEditor.Models
{
    public class School : IIdentifiable
    {
        [JsonPropertyName("school.name")]
        public string? Name { get; set; }

        [JsonPropertyName("school.city")]
        public string? City { get; set; }

        [JsonPropertyName("school.state")]
        public string? State { get; set; }

        [JsonPropertyName("school.zip")]
        public string? Zip { get; set; }

        [JsonPropertyName("school.accreditor")]
        public string? Accreditor { get; set; }

        [JsonPropertyName("school.school_url")]
        public string? SchoolUrl { get; set; }

        [JsonPropertyName("school.price_calculator_url")]
        public string? PriceCalculatorUrl { get; set; }

        [JsonPropertyName("school.degrees_awarded.predominant_recoded")]
        public int? DegreesAwardedPredominantRecoded { get; set; }

        [JsonPropertyName("school.under_investigation")]
        public int? UnderInvestigation { get; set; }

        [JsonPropertyName("school.main_campus")]
        public int? MainCampus { get; set; }

        [JsonPropertyName("school.branches")]
        public int? Branches { get; set; }

        [JsonPropertyName("school.degrees_awarded.predominant")]
        public int? DegreesAwardedPredominant { get; set; }

        [JsonPropertyName("school.degrees_awarded.highest")]
        public int? DegreesAwardedHighest { get; set; }

        [JsonPropertyName("school.ownership")]
        public int? Ownership { get; set; }

        [JsonPropertyName("school.state_fips")]
        public int? StateFips { get; set; }

        [JsonPropertyName("school.region_id")]
        public int? RegionId { get; set; }

        [JsonPropertyName("school.locale")]
        public int? Locale { get; set; }


        [JsonPropertyName("school.carnegie_basic")]
        public int? CarnegieBasic { get; set; }

        [JsonPropertyName("school.carnegie_undergrad")]
        public int? CarnegieUndergrad { get; set; }

        [JsonPropertyName("school.carnegie_size_setting")]
        public int? CarnegieSizeSetting { get; set; }

        [JsonPropertyName("school.men_only")]
        public int? MenOnly { get; set; }

        [JsonPropertyName("school.women_only")]
        public int? WomenOnly { get; set; }

        [JsonPropertyName("school.religious_affiliation")]
        public int? ReligiousAffiliation { get; set; }

        [JsonPropertyName("school.online_only")]
        public int? OnlineOnly { get; set; }

        [JsonPropertyName("school.operating")]
        public int? Operating { get; set; }

        [JsonPropertyName("school.tuition_revenue_per_fte")]
        public int? TuitionRevenuePerFte { get; set; }

        [JsonPropertyName("school.instructional_expenditure_per_fte")]
        public int? InstructionalExpenditurePerFte { get; set; }

        [JsonPropertyName("school.faculty_salary")]
        public int? FacultySalary { get; set; }

        [JsonPropertyName("school.ft_faculty_rate")]
        public double? SchoolFtFacultyRate { get; set; }

        [JsonPropertyName("school.alias")]
        public string? Alias { get; set; }

        [JsonPropertyName("school.institutional_characteristics.level")]
        public int? InstitutionalCharacteristicsLevel { get; set; }

        [JsonPropertyName("school.open_admissions_policy")]
        public int? OpenAdmissionsPolicy { get; set; }

        [JsonPropertyName("school.accreditor_code")]
        public string? AccreditorCode { get; set; }

        [JsonPropertyName("school.title_iv.approval_date")]
        public string? TitleIvApprovalDate { get; set; }

        [JsonPropertyName("school.ownership_peps")]
        public int? OwnershipPeps { get; set; }

        [JsonPropertyName("school.title_iv.eligibility_type")]
        public int? TitleIvEligibilityType { get; set; }

        [JsonPropertyName("school.id")]
        // [JsonPropertyName("id")]
        public int? idInt { get; set; }

        [JsonPropertyName("id")]
        public string? id { get; set; }
        
        [JsonPropertyName("ope6_id")]
        public string? ope6Id { get; set; }

        [JsonPropertyName("ope8_id")]
        public string? ope8Id { get; set; }
    }
}
