using CollegeDataEditor.Enums;
using CollegeDataEditor.Interfaces;
using CollegeDataEditor.Models;

namespace CollegeDataEditor.Services;

public class DbService
{
    public event Action<DbState> OnStateChange;
    
    public HttpClient http { get; set; }
    public DbState state { get; set; } = DbState.Uninitialized;
    
    public Db<IndexedValue> CitizenshipsDb;
    public Db<IndexedValue> OrgTypesDb;
    public Db<IndexedValue> ResidencesDb;
    public Db<IndexedValue> SubjectsDb;
    public Db<IndexedValue> TagDb;
    public Db<IndexedValue> TopicsDb;
    public Db<IndexedValue> ProgramTypesDb;

    public Db<SummerProgram> SummerProgramsDb;
    public Db<Org> OrgsDb;
    
    public Db<Application> ApplicationsDb;
    public Db<Session> SessionsDb;
    public Db<StudentInfo> StudentInfoDb;
    
    private string connKey { get; set; }

    public DbService(HttpClient httpClient)
    {
        http = httpClient;
        connKey = "?code=mzVo73ICTgPS9QcoqR37ItuGDVFS9dCXgf_13MA90eI-AzFuoQkuyA==";
        //_databases.Add(pathogenDb);

        TagDb = new Db<IndexedValue>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = "https://collegeprepapi.azurewebsites.net/api/tag",
            connectionStringArchive = string.Empty,
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };

        SubjectsDb = new Db<IndexedValue>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = "https://collegeprepapi.azurewebsites.net/api/subject",
            connectionStringArchive = string.Empty,
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };

        TopicsDb = new Db<IndexedValue>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = "https://collegeprepapi.azurewebsites.net/api/topic",
            connectionStringArchive = string.Empty,
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };

        OrgTypesDb = new Db<IndexedValue>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = "https://collegeprepapi.azurewebsites.net/api/orgType",
            connectionStringArchive = string.Empty,
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };

        ResidencesDb = new Db<IndexedValue>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = "https://collegeprepapi.azurewebsites.net/api/residence",
            connectionStringArchive = string.Empty,
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };

        CitizenshipsDb = new Db<IndexedValue>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = "https://collegeprepapi.azurewebsites.net/api/citizenship",
            connectionStringArchive = string.Empty,
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };
        
        ProgramTypesDb = new Db<IndexedValue>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = "https://collegeprepapi.azurewebsites.net/api/programType",
            connectionStringArchive = string.Empty,
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };
        
        SummerProgramsDb = new Db<SummerProgram>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = 
                "https://collegeprepapi.azurewebsites.net/api/summerProgram",
            connectionStringArchive = 
                "https://collegeprepapi.azurewebsites.net/api/summerProgramsArchive",
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };
        
        OrgsDb = new Db<Org>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = 
                "https://collegeprepapi.azurewebsites.net/api/org",
            connectionStringArchive = 
                "https://collegeprepapi.azurewebsites.net/api/archiveOrg",
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };
        
        ApplicationsDb = new Db<Application>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = 
                "https://collegeprepapi.azurewebsites.net/api/application",
            connectionStringArchive = string.Empty,
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };
        
        SessionsDb = new Db<Session>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = 
                "https://collegeprepapi.azurewebsites.net/api/session",
            connectionStringArchive = string.Empty,
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };
        
        StudentInfoDb = new Db<StudentInfo>
        {
            http = http,
            state = DbState.Uninitialized,
            connectionStringBase = 
                "https://collegeprepapi.azurewebsites.net/api/studentInfoSection",
            connectionStringArchive = string.Empty,
            connectionStringKey = connKey,
            CooldownSeconds = 30
        };
    }
    
    
    private void NotifyDbServiceStateChanged()
    {
        OnStateChange?.Invoke(state);
    }
    
    public async Task TryLoadAllRequiredDbsAsync(int cooldownSeconds = 30)
    {
        state = DbState.Loading;
        NotifyDbServiceStateChanged();
        
        await TryLoadDbAsync(TagDb, cooldownSeconds);
        await TryLoadDbAsync(SubjectsDb, cooldownSeconds);
        await TryLoadDbAsync(TopicsDb, cooldownSeconds);
        await TryLoadDbAsync(OrgTypesDb, cooldownSeconds);
        await TryLoadDbAsync(ResidencesDb, cooldownSeconds);
        await TryLoadDbAsync(CitizenshipsDb, cooldownSeconds);
        await TryLoadDbAsync(ProgramTypesDb, cooldownSeconds);
        await TryLoadDbAsync(SummerProgramsDb, cooldownSeconds);
        await TryLoadDbAsync(OrgsDb, cooldownSeconds);
        await TryLoadDbAsync(ApplicationsDb, cooldownSeconds);
        await TryLoadDbAsync(SessionsDb, cooldownSeconds);
        await TryLoadDbAsync(StudentInfoDb, cooldownSeconds);

        state = DbState.Ready;
        NotifyDbServiceStateChanged();
    }

    public async Task TryLoadDbAsync<T>(Db<T> db, int cooldownSeconds) where T : IIdentifiable
    {
        db.CooldownSeconds = cooldownSeconds;
        await db.GetAllFromDb();
    }
}