
namespace CollegeDataEditor.Models;

public class SummerProgram : BaseInfoObj
{
    private DateTime? _sleepUntil = new DateTime(2023, 1, 1);
    public List<string> topicIdList { get; set; } = new();
    public List<string> subjectIdList { get; set; } = new();
    public List<string> programTypeIdList { get; set; } = new();
    public List<string> orgIdList { get; set; } = new();
    public List<string> sessionIdList { get; set; } = new();
    public List<string> applicationIdList { get; set; } = new();
    public List<string> studentInfoIdList { get; set; } = new();
    public List<string> schoolIdList { get; set; } = new();
    public bool hasReminderSet { get; set; } = false;

    public bool hasActiveAlarm => hasReminderSet && sleepUntil.Value.CompareTo(DateTime.Now) < 0;
    
    public override string TypeName() => "Summer Program";

    public DateTime? sleepUntil
    {
        get => _sleepUntil;
        set
        {
            if (value is null || value.Value.CompareTo(DateTime.Now) < 1)
            {
                _sleepUntil = DateTime.Now;
                return;
            }
            _sleepUntil = value;
        } 
    }
}