using CollegeDataEditor.Enums;

namespace CollegeDataEditor.Models;

public abstract class DbCaptrs : BaseInfoObj
{
    public HttpClient http { get; set; }
    public DbState state { get; set; }

    public bool isLoaded { get; set; }
    public bool isLoading { get; set; }

    public string connectionStringBase { get; set; }
    public string connectionStringArchive { get; set; }
    public string connectionStringKey { get; set; }

    public string connectionString => $"{connectionStringBase}{connectionStringKey}";
    public string connectionArchive => $"{connectionStringArchive}{connectionStringKey}";

    public DateTime lastLoaded { get; set; }
    public int CooldownSeconds { get; set; } = 30;

    private TimeSpan TimeSinceLastLoaded()
    {
        return lastLoaded.Subtract(DateTime.Now);
    }

    public int SecondsSinceLastLoaded()
    {
        return TimeSinceLastLoaded().Seconds;
    }

    public int MinutesSinceLastLoaded()
    {
        return TimeSinceLastLoaded().Minutes;
    }

    protected string PutByIdConnectionString(string putId)
    {
        return $"{connectionStringBase}/{putId}{connectionStringKey}";
    }

    protected string DeleteByIdConnectionString(string deleteId)
    {
        return $"{connectionStringBase}/{deleteId}{connectionStringKey}";
    }

    protected string PutArchiveByIdConnString(string putId)
    {
        return $"{connectionStringArchive}/{putId}{connectionStringKey}";
    }

    public async Task GetAllFromDb()
    {
        if (state == DbState.Loading || InsideRetryCooldown()) return;

        state = DbState.Loading;
        if (await GetAllDbSpecificItems())
        {
            state = DbState.Loaded;
            return;
        }

        state = DbState.Failed;
    }

    private bool InsideRetryCooldown()
    {
        if (state == DbState.Failed || state == DbState.Loaded) return SecondsSinceLastLoaded() < CooldownSeconds;
        return false;
    }

    protected abstract Task<bool> GetAllDbSpecificItems();

    public abstract Task BuildNewAndSetToEditor();
    public abstract Task CloneAndSetToEditor();
    public abstract Task<bool> SubmitEditsToDbAsync();
    public abstract Task<bool> SubmitEditsToArchiveAsync();
    public abstract Task<bool> DeleteFromDbAsync();
}