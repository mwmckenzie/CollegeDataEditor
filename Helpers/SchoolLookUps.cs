namespace CollegeDataEditor.Helpers;

public class SchoolLookUps {
    
    private readonly Dictionary<int, string> _carnegieBasic;
    private readonly Dictionary<int, string> _carnegieSizeSetting;
    private readonly Dictionary<int, string> _carnegieUndergrad;
    private readonly Dictionary<int, string> _highestDegree;
    private readonly Dictionary<int, string> _stateFullNameByFip;
    private readonly Dictionary<int, string> _urbanDegree;
    
    public SchoolLookUps() {
        region = new Dictionary<int, string> {
            {0, "U.S.Service Schools"},
            {1, "New England"},
            {2, "Mid East"},
            {3, "Great Lakes"},
            {4, "Plains"},
            {5, "Southeast"},
            {6, "Southwest"},
            {7, "Rocky Mountains"},
            {8, "Far West"},
            {9, "Outlying Areas"}
        };

        locale = new Dictionary<int, string> {
            {11, "City: Large"},
            {12, "City: Midsize"},
            {13, "City: Small"},
            {21, "Suburb: Large"},
            {22, "Suburb: Midsize"},
            {23, "Suburb: Small"},
            {31, "Town: Fringe"},
            {32, "Town: Distant"},
            {33, "Town: Remote"},
            {41, "Rural: Fringe"},
            {42, "Rural: Distant"},
            {43, "Rural: Remote"}
        };

        mainCampus = new Dictionary<int, string> {
            {0, "Branch Campus"},
            {1, "Main Campus"}
        };

        ownership = new Dictionary<int, string> {
            {1, "Public"},
            {2, "Private nonprofit"},
            {3, "Private for-profit"}
        };

        _carnegieBasic = new Dictionary<int, string> {
            {-2, "Not applicable"},
            {0, "Not classified"},
            {1, "Associate's Colleges: High Transfer-High Traditional"},
            {2, "Associate's Colleges: High Transfer-Mixed Traditional/Nontraditional"},
            {3, "Associate's Colleges: High Transfer-High Nontraditional"},
            {4, "Associate's Colleges: Mixed Transfer/Career & Technical-High Traditional"},
            {5, "Associate's Colleges: Mixed Transfer/Career & Technical-Mixed Traditional/Nontraditional"},
            {6, "Associate's Colleges: Mixed Transfer/Career & Technical-High Nontraditional"},
            {7, "Associate's Colleges: High Career & Technical-High Traditional"},
            {8, "Associate's Colleges: High Career & Technical-Mixed Traditional/Nontraditional"},
            {9, "Associate's Colleges: High Career & Technical-High Nontraditional"},
            {10, "Special Focus Two-Year: Health Professions"},
            {11, "Special Focus Two-Year: Technical Professions"},
            {12, "Special Focus Two-Year: Arts & Design"},
            {13, "Special Focus Two-Year: Other Fields"},
            {14, "Baccalaureate/Associate's Colleges: Associate's Dominant"},
            {15, "Doctoral Universities: Very High Research Activity"},
            {16, "Doctoral Universities: High Research Activity"},
            {17, "Doctoral/Professional Universities"},
            {18, "Master's Colleges & Universities: Larger Programs"},
            {19, "Master's Colleges & Universities: Medium Programs"},
            {20, "Master's Colleges & Universities: Small Programs"},
            {21, "Baccalaureate Colleges: Arts & Sciences Focus"},
            {22, "Baccalaureate Colleges: Diverse Fields"},
            {23, "Baccalaureate/Associate's Colleges: Mixed Baccalaureate/Associate's"},
            {24, "Special Focus Four-Year: Faith-Related Institutions"},
            {25, "Special Focus Four-Year: Medical Schools & Centers"},
            {26, "Special Focus Four-Year: Other Health Professions Schools"},
            {27, "Special Focus Four-Year: Engineering Schools"},
            {28, "Special Focus Four-Year: Other Technology-Related Schools"},
            {29, "Special Focus Four-Year: Business & Management Schools"},
            {30, "Special Focus Four-Year: Arts, Music & Design Schools"},
            {31, "Special Focus Four-Year: Law Schools"},
            {32, "Special Focus Four-Year: Other Special Focus Institutions"},
            {33, "Tribal Colleges"}
        };

        _carnegieUndergrad = new Dictionary<int, string> {
            {-2, "Not applicable"},
            {0, "Not classified (Exclusively Graduate)"},
            {1, "Two-year, higher part-time"},
            {2, "Two-year, mixed part/full-time"},
            {3, "Two-year, medium full-time"},
            {4, "Two-year, higher full-time"},
            {5, "Four-year, higher part-time"},
            {6, "Four-year, medium full-time, inclusive, lower transfer-in"},
            {7, "Four-year, medium full-time, inclusive, higher transfer-in"},
            {8, "Four-year, medium full-time, selective, lower transfer-in"},
            {9, "Four-year, medium full-time , selective, higher transfer-in"},
            {10, "Four-year, full-time, inclusive, lower transfer-in"},
            {11, "Four-year, full-time, inclusive, higher transfer-in"},
            {12, "Four-year, full-time, selective, lower transfer-in"},
            {13, "Four-year, full-time, selective, higher transfer-in"},
            {14, "Four-year, full-time, more selective, lower transfer-in"},
            {15, "Four-year, full-time, more selective, higher transfer-in"}
        };

        _carnegieSizeSetting = new Dictionary<int, string> {
            {-2, "Not applicable"},
            {0, "Not classified"},
            {1, "Two-year, very small"},
            {2, "Two-year, small"},
            {3, "Two-year, medium"},
            {4, "Two-year, large"},
            {5, "Two-year, very large"},
            {6, "Four-year, very small, primarily nonresidential"},
            {7, "Four-year, very small, primarily residential"},
            {8, "Four-year, very small, highly residential"},
            {9, "Four-year, small, primarily nonresidential"},
            {10, "Four-year, small, primarily residential"},
            {11, "Four-year, small, highly residential"},
            {12, "Four-year, medium, primarily nonresidential"},
            {13, "Four-year, medium, primarily residential"},
            {14, "Four-year, medium, highly residential"},
            {15, "Four-year, large, primarily nonresidential"},
            {16, "Four-year, large, primarily residential"},
            {17, "Four-year, large, highly residential"},
            {18, "Exclusively graduate/professional"}
        };

        _highestDegree = new Dictionary<int, string> {
            {0, "Non-degree-granting"},
            {1, "Certificate degree"},
            {2, "Associate degree"},
            {3, "Bachelor's degree"},
            {4, "Graduate degree"}
        };

        _urbanDegree = new Dictionary<int, string> {
            {1, "Large City"},
            {2, "Mid-Size City"},
            {3, "Urban Fringe of a Large City "},
            {4, "Urban Fringe of a Mid-Size City "},
            {5, "Large Town"},
            {6, "Small Town"},
            {7, "Rural, Outside MSA"},
            {8, "Rural, Inside MSA"}
        };

        _stateFullNameByFip = new Dictionary<int, string> {
            {1, "Alabama"},
            {2, "Alaska"},
            {4, "Arizona"},
            {5, "Arkansas"},
            {6, "California"},
            {8, "Colorado"},
            {9, "Connecticut"},
            {10, "Delaware"},
            {11, "District of Columbia"},
            {12, "Florida"},
            {13, "Georgia"},
            {15, "Hawaii"},
            {16, "Idaho"},
            {17, "Illinois"},
            {18, "Indiana"},
            {19, "Iowa"},
            {20, "Kansas"},
            {21, "Kentucky"},
            {22, "Louisiana"},
            {23, "Maine"},
            {24, "Maryland"},
            {25, "Massachusetts"},
            {26, "Michigan"},
            {27, "Minnesota"},
            {28, "Mississippi"},
            {29, "Missouri"},
            {30, "Montana"},
            {31, "Nebraska"},
            {32, "Nevada"},
            {33, "New Hampshire"},
            {34, "New Jersey"},
            {35, "New Mexico"},
            {36, "New York"},
            {37, "North Carolina"},
            {38, "North Dakota"},
            {39, "Ohio"},
            {40, "Oklahoma"},
            {41, "Oregon"},
            {42, "Pennsylvania"},
            {44, "Rhode Island"},
            {45, "South Carolina"},
            {46, "South Dakota"},
            {47, "Tennessee"},
            {48, "Texas"},
            {49, "Utah"},
            {50, "Vermont"},
            {51, "Virginia"},
            {53, "Washington"},
            {54, "West Virginia"},
            {55, "Wisconsin"},
            {56, "Wyoming"},
            {60, "American Samoa"},
            {64, "Federated States of Micronesia"},
            {66, "Guam"},
            {69, "Northern Mariana Islands"},
            {70, "Palau"},
            {72, "Puerto Rico"},
            {78, "Virgin Islands"}
        };
    }

    public Dictionary<int, string> region { get; }
    public Dictionary<int, string> locale { get; }
    public Dictionary<int, string> mainCampus { get; }
    public Dictionary<int, string> ownership { get; }

    // public string ShortenUrl(string? urlIn)
    // {
    //     if (urlIn is null)
    //     {
    //         return string.Empty;
    //     }
    //
    //     if (!urlIn.Contains("https://"))
    //     {
    //         urlIn = "https://" + urlIn;
    //     }
    //     if (urlIn.Contains('/') && urlIn.Split('/').Length >= 4)
    //     {
    //         var replace = urlIn.Split('/', 4)[3];
    //         return replace.Length > 0 ? urlIn.Replace(replace, string.Empty) + "..."
    //                 : urlIn;
    //     }
    //     return urlIn;
    // } 

    public string GetRegion(int? id) {
        const string unknown = "Unknown Region";
        if (id is null) return unknown;
        return!region.ContainsKey((int) id) ? unknown : region[(int) id];
    }

    public string GetLocale(int? id) {
        const string unknown = "Unknown Locale";
        if (id is null) return unknown;
        return!locale.ContainsKey((int) id) ? unknown : locale[(int) id];
    }

    public string GetMainCampus(int? id) {
        const string unknown = "Unknown Main Campus";
        if (id is null) return unknown;
        return!mainCampus.ContainsKey((int) id) ? unknown : mainCampus[(int) id];
    }

    public string GetOwnership(int? id) {
        const string unknown = "Unknown Ownership";
        if (id is null) return unknown;
        return!ownership.ContainsKey((int) id) ? unknown : ownership[(int) id];
    }

    public string GetCarnegieBasic(int? id) {
        const string unknown = "Unknown Carnegie Basic";
        if (id is null) return unknown;
        return!_carnegieBasic.ContainsKey((int) id) ? unknown : _carnegieBasic[(int) id];
    }

    public string GetCarnegieUndergrad(int? id) {
        const string unknown = "Unknown Carnegie Undergrad";
        if (id is null) return unknown;
        return!_carnegieUndergrad.ContainsKey((int) id) ? unknown : _carnegieUndergrad[(int) id];
    }

    public string GetCarnegieSizeSetting(int? id) {
        const string unknown = "Unknown Carnegie Size & Setting";
        if (id is null) return unknown;
        return!_carnegieSizeSetting.ContainsKey((int) id) ? unknown : _carnegieSizeSetting[(int) id];
    }

    public string GetUrbanDegree(int? id) {
        const string unknown = "Unknown Urban Degree";
        if (id is null) return unknown;
        return!_urbanDegree.ContainsKey((int) id) ? unknown : _urbanDegree[(int) id];
    }

    public string GetStateFullNameByFip(int? id) {
        const string unknown = "Unknown State FIP";
        if (id is null) return unknown;
        return!_stateFullNameByFip.ContainsKey((int) id) ? unknown : _stateFullNameByFip[(int) id];
    }

    public string GetHighestDegree(int? id) {
        const string unknown = "Unknown Highest Degree";
        if (id is null) return unknown;
        return!_highestDegree.ContainsKey((int) id) ? unknown : _highestDegree[(int) id];
    }
    
    public string GetYesNo(int? id) {
        const string unknown = "Unknown";
        if (id is null || id > 1) return unknown;
        return id > 0 ? "Yes" : "No" ;
    }
}