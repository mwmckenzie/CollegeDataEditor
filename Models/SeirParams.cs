namespace CollegeDataEditor.Models;

public class SeirParams
{
    public double exposedToInfectiousRate { get; set; }
    public double symptomaticRecoveryRate { get; set; }
    public double asymptomaticRecoveryRate { get; set; }
    public double hospitalizationRecoveryRate { get; set; }
    public double criticalRecoveryRate { get; set; }
    public double recoveredToSusceptibleRate { get; set; }
    public double vaccinatedToSusceptibleRate { get; set; }
    public double hospToNonHospRatio { get; set; }
    public double asympToSympRatio { get; set; }
    public double criticalToHospRatio { get; set; }
    public double deceasedToCriticalRecovRatio { get; set; }
    public double sympTransmissionRate { get; set; }
    public double asympTransmissionRate { get; set; }
    public double hospitalTransmissionRate { get; set; }
    public double criticalTransmissionRate { get; set; }
    public double visitorTransmissionRate { get; set; }
    public double susceptibleTestingRate { get; set; }
    public double exposedTestingRate { get; set; }
    public double symptomaticTestingRate { get; set; }
    public double asymptomaticTestingRate { get; set; }
    public double hospitalizationTestingRate { get; set; }
    public double criticalTestingRate { get; set; }
    public double recoveredTestingRate { get; set; }
    public double vaccinatedTestingRate { get; set; }
    public double falsePosRate { get; set; }
    public double falseNegRate { get; set; }


    public Dictionary<string, double> labelsAndValues()
    {
        return new Dictionary<string, double>
        {
            { "Exposed To Infectious Rate", exposedToInfectiousRate },
            { "Symptomatic Recovery Rate", symptomaticRecoveryRate },
            { "Asymptomatic Recovery Rate", asymptomaticRecoveryRate },
            { "Hospitalization Recovery Rate", hospitalizationRecoveryRate },
            { "Critical Recovery Rate", criticalRecoveryRate },
            { "Recovered To Susceptible Rate", recoveredToSusceptibleRate },
            { "Vaccinated To Susceptible Rate", vaccinatedToSusceptibleRate },
            { "Hospitalized To Non-Hospitalized Proportion", hospToNonHospRatio },
            { "Asymptomatic To Symptomatic Proportion", asympToSympRatio },
            { "Critical To Hospitalized Proportion", criticalToHospRatio },
            { "Deceased To Critical Recovery Proportion", deceasedToCriticalRecovRatio },
            { "Symptomatic Transmission Rate", sympTransmissionRate },
            { "Asymptomatic Transmission Rate", asympTransmissionRate },
            { "Hospital Transmission Rate", hospitalTransmissionRate },
            { "Critical Transmission Rate", criticalTransmissionRate },
            { "Visitor Transmission Rate", visitorTransmissionRate },
            { "Susceptible Testing Rate", susceptibleTestingRate },
            { "Exposed Testing Rate", exposedTestingRate },
            { "Symptomatic Testing Rate", symptomaticTestingRate },
            { "Asymptomatic Testing Rate", asymptomaticTestingRate },
            { "Hospitalization Testing Rate", hospitalizationTestingRate },
            { "Critical Testing Rate", criticalTestingRate },
            { "Recovered Testing Rate", recoveredTestingRate },
            { "Vaccinated Testing Rate", vaccinatedTestingRate },
            { "False Positive Rate", falsePosRate },
            { "False Negative Rate", falseNegRate }
        };
    }
}