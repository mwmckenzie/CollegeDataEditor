using MudBlazor;

namespace CollegeDataEditor.Models;

public abstract class DateRangeObj : SumProgComponent {
    private DateTime? _startDateTime;
    private DateTime? _endDateTime;

    public string? startDate
    {
        get => startDateTime.ToString();
        set
        {
            if (value is null)
            {
                return;
            }
            startDateTime = DateTime.Parse(value);
        }
        
    }
    public string? endDate
    {
        get => endDateTime.ToString();
        set
        {
            if (value is null)
            {
                return;
            }
            endDateTime = DateTime.Parse(value);
        } 
    }

    public DateTime? startDateTime
    {
        get => _startDateTime ?? DateTime.Now;
        set
        {
            _startDateTime = value;
            
            if (_startDateTime is not null 
                && (_endDateTime is null
                || DateTime.Compare((DateTime)_startDateTime, (DateTime)_endDateTime) >= 0 ))
            {
                _endDateTime = _startDateTime;
            }
            OnBaseUpdate();
        }
    }

    public DateTime? endDateTime
    {
        get => _endDateTime ?? DateTime.Now;
        set
        {
            _endDateTime = value;
            OnBaseUpdate();
        }
    }
    
}