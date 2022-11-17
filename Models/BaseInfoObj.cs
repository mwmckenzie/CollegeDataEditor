using CollegeDataEditor.Enums;
using CollegeDataEditor.Interfaces;

namespace CollegeDataEditor.Models;

public class BaseInfoObj : ICloneable, IEquatable<BaseInfoObj>, IIdentifiable, ICreatable
{
    public event Action? NotifyObjEdited;
    
    private string? _displayName;
    private string? _id = Guid.NewGuid().ToString();
    private string? _name;
    private string? _url;
    
    public List<string> tagIdList { get; set; } = new();
    public List<string> aliasList { get; set; } = new();
    public List<string> urlList { get; set; } = new();
    public List<string> noteList { get; set; } = new();

    public string id
    {
        get => _id;
        set
        {
            if (_id == value) return;
            _id = value;
            Validate();
            OnBaseUpdate();
        }
    }

    public string? name
    {
        get => _name;
        set
        {
            if (_name == value) return;
            _name = value;
            Validate();
            OnBaseUpdate();
        }
    }

    public string? displayName
    {
        get => _displayName ?? name;
        set
        {
            if (_displayName == value) return;
            _displayName = value;
            Validate();
            OnBaseUpdate();
        }
    }

    public string? url
    {
        get => _url;
        set
        {
            if (_url == value) return;
            _url = value;
            Validate();
            OnBaseUpdate();
        }
    }

    public bool isValid { get; set; }

    public virtual string TypeName() => "Unknown Type";

    public void OnObjEdited()
    {
        NotifyObjEdited?.Invoke();
    }
    
    public virtual void Add(SumProgElements elementType, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;
        switch (elementType)
        {
            case SumProgElements.Alias:
                OnElementAdded(aliasList, value);
                break;
            case SumProgElements.Note:
                OnElementAdded(noteList, value);
                break;
            case SumProgElements.Url:
                var fixedUrl = BuildValidUrl(value);
                if (string.IsNullOrWhiteSpace(url)) url = fixedUrl;
                OnElementAdded(urlList, fixedUrl);
                break;
            case SumProgElements.TagId:
                OnElementAdded(tagIdList, value);
                break;
        }
    }

    private void OnElementAdded(ICollection<string> list, string value)
    {
        if (list.Contains(value)) return;
        list.Add(value);
        OnBaseUpdate();
    }

    private void OnElementRemoved(ICollection<string> list, string value)
    {
        if (!list.Contains(value)) return;
        list.Remove(value);
        OnBaseUpdate();
    }

    private string BuildValidUrl(string urlText)
    {
        if (urlText.Contains("https://", StringComparison.OrdinalIgnoreCase)) return urlText;
        urlText = urlText.Replace("http://", string.Empty);
        return $"https://{urlText}";
    }
    
    public virtual object Clone()
    {
        return new BaseInfoObj
        {
            isValid = isValid,
            id = id,
            name = name,
            _displayName = _displayName,
            displayName = displayName,
            url = url
        };
    }

    public override string ToString()
    {
        return displayName ?? string.Empty;
    }

    public bool Equals(BaseInfoObj other)
    {
        return id == other.id
               && name == other.name;
    }


    protected virtual void Validate()
    {
        isValid = !string.IsNullOrWhiteSpace(id)
                  && !string.IsNullOrWhiteSpace(name);
    }

    protected virtual void OnBaseUpdate()
    {
    }

    public bool IsSameButDiff(BaseInfoObj other)
    {
        return Equals(other) &&
               (_displayName != other._displayName
                || isValid != other.isValid
                || url != other.url);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((BaseInfoObj)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_displayName, isValid, id, name, url);
    }
}