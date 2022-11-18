using System.Text.RegularExpressions;
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
        if (displayName is null || startDateTime is null)
        {
            return;
        }
        var regex = new Regex("\\d{4}");
        if (regex.IsMatch(displayName))
        {
            displayName =  regex.Replace(displayName, $"({startDateTime.Value.Year.ToString()})")
                .Replace("((", "(")
                .Replace("))", ")");
            
        }
        else
        {
            displayName += $" ({startDateTime.Value.Year.ToString()})";
        }
    }
    
}