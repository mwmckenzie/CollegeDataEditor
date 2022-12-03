using System.Text.RegularExpressions;

namespace CollegeDataEditor.Models;

public abstract class DateRangeObj : SumProgComponent {
    private DateTime? _startDateTime;
    private DateTime? _endDateTime;

    public string? startDate
    {
        get => startDateTime.ToString();
        set
        {
            if (value is not null && DateTime.TryParse(value, out var result))
            {
                startDateTime = result;
            }
        }
    }
    
    public string? endDate
    {
        get => endDateTime.ToString();
        set
        {
            if (value is not null && DateTime.TryParse(value, out var result))
            {
                endDateTime = result;
            }
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

    public bool ActiveNow => startDateTime.Value.CompareTo(DateTime.Now) < 0 &&
                             endDateTime.Value.CompareTo(DateTime.Now) > 0;

    public bool ActiveInRange(DateTime rangeStart, DateTime rangeEnd)
    {
        return DateIsBetween(startDateTime.Value, rangeStart, rangeEnd)
               || DateIsBetween(endDateTime.Value, rangeStart, rangeEnd);
    }

    public bool DateIsBetween(DateTime date, DateTime rangeStart, DateTime rangeEnd)
    {
        return date.CompareTo(rangeStart) >= 0 && date.CompareTo(rangeEnd) <= 0;
    }
    
    protected override void OnBaseUpdate()
    {
        base.OnBaseUpdate();
        UpdateNamesWithDateSuffix();
    }
    
    private void UpdateNamesWithDateSuffix()
    {
        AppendDateToDisplayName();
    }

    private void AppendDateToName()
    {
        if (name is null || startDateTime is null || name.Contains(startDateTime.Value.Year.ToString()))
        {
            return;
        }
        var regex = new Regex("\\d{4}");
        if (regex.IsMatch(name))
        {
            name = regex.Replace(name, $"({startDateTime.Value.Year.ToString()})")
                .Replace("((", "(")
                .Replace("))", ")");
        }
        else
        {
            name += $" ({startDateTime.Value.Year.ToString()})";
        }
    }
    
    private void AppendDateToDisplayName()
    {
        if (displayName is null || endDateTime is null)
        {
            return;
        }
        var regex = new Regex("\\d{4}");
        if (regex.IsMatch(displayName))
        {
            displayName =  regex.Replace(displayName, $"({endDateTime.Value.Year.ToString()})")
                .Replace("((", "(")
                .Replace("))", ")");
            
        }
        else
        {
            displayName += $" ({endDateTime.Value.Year.ToString()})";
        }
    }
    
}