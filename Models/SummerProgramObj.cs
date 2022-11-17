using System.Reactive.Subjects;
using CollegeDataEditor.Enums;

namespace CollegeDataEditor.Models;

public class SummerProgramObj : BaseInfoObj
{
    public List<string> topicIdList { get; set; } = new();
    public List<string> subjectIdList { get; set; } = new();
    public List<string> programTypeIdList { get; set; } = new();
    public List<string> orgIdList { get; set; } = new();
    public List<string> sessionIdList { get; set; } = new();
    public List<string> applicationIdList { get; set; } = new();
    public List<string> studentInfoIdList { get; set; } = new();

    public override string TypeName() => "Summer Program";
    
    private readonly Subject<SummerProgramObj> _summerProgramSubject = new();
    public IObservable<SummerProgramObj> summerProgram => _summerProgramSubject;
    
    public override void Add(SumProgElements elementType, string value)
    {
        base.Add(elementType, value);
        switch (elementType)
        {
            case SumProgElements.SessionId:
                OnElementAdded(sessionIdList, value);
                break;
            case SumProgElements.ApplicationId:
                OnElementAdded(applicationIdList, value);
                break;
            case SumProgElements.StudentInfoId:
                OnElementAdded(studentInfoIdList, value);
                break;
            case SumProgElements.OrgId:
                OnElementAdded(orgIdList, value);
                break;
            case SumProgElements.Subject:
                OnElementAdded(subjectIdList, value);
                break;
            case SumProgElements.Topic:
                OnElementAdded(topicIdList, value);
                break;
            case SumProgElements.ProgramType:
                OnElementAdded(programTypeIdList, value);
                break;
        }
    }
    
    private void OnElementAdded(ICollection<string> list, string value)
    {
        if (list.Contains(value)) return;
        list.Add(value);
        OnUpdate();
    }

    private void OnElementRemoved(ICollection<string> list, string value)
    {
        if (!list.Contains(value)) return;
        list.Remove(value);
        OnUpdate();
    }

    protected override void OnBaseUpdate()
    {
        base.OnBaseUpdate();
        OnUpdate();
    }

    private void OnUpdate()
    {
        _summerProgramSubject.OnNext(this);
    }
    
        
    // public IEnumerable<string> Subjects() {
    //     foreach (var index in subjectIdList) {
    //         yield return lookUps.GetSubject(index);
    //     }
    // }
    //
    // public IEnumerable<string> Topics() {
    //     foreach (var index in topicIdList) {
    //         yield return lookUps.GetTopic(index);
    //     }
    // }
    //
    // public IEnumerable<string> ProgramTypes() {
    //     foreach (var index in programTypeIdList) {
    //         yield return lookUps.GetProgramType(index);
    //     }
    // }
    
    // public override void Add(SumProgElements elementType, int value) {
    //     base.Add(elementType, value);
    //     switch (elementType) {
    //         case SumProgElements.Subject:
    //             if (!subjectIdList.Contains(value)) subjectIdList.Add(value);
    //             break;
    //         case SumProgElements.Topic:
    //             if (!topicIdList.Contains(value)) topicIdList.Add(value);
    //             break;
    //         case SumProgElements.ProgramType:
    //             if (!programTypeIdList.Contains(value)) programTypeIdList.Add(value);
    //             break;
    //     }
    // }
}