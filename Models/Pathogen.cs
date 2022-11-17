namespace CollegeDataEditor.Models;

public class Pathogen : BaseInfoObj
{
    public string? typeName { get; set; }
    public double? onset { get; set; }
    public double? duration { get; set; }

    public List<string>? acuteSymptoms { get; set; }
    public List<string>? reservoirDetails { get; set; }

    public override string ToString()
    {
        return typeName ?? nameof(Pathogen);
    }

    public override object Clone()
    {
        return new Pathogen
        {
            isValid = isValid,
            id = id,
            name = name,
            displayName = displayName,
            url = url,
            typeName = typeName,
            onset = onset,
            duration = duration,
            acuteSymptoms = acuteSymptoms?.ToArray().ToList(),
            reservoirDetails = reservoirDetails?.ToArray().ToList()
        };
    }

    protected bool Equals(Pathogen other)
    {
        return base.Equals(other) && typeName == other.typeName && Nullable.Equals(onset, other.onset) &&
               Nullable.Equals(duration, other.duration);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Pathogen)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), typeName, onset, duration);
    }

    public static bool operator ==(Pathogen? left, Pathogen? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Pathogen? left, Pathogen? right)
    {
        return !Equals(left, right);
    }
}