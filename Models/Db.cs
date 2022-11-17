using System.Net.Http.Json;
using CollegeDataEditor.Enums;
using CollegeDataEditor.Interfaces;

namespace CollegeDataEditor.Models;

public class Db<T> where T : IIdentifiable
{
    public List<T> dbItems { get; set; } = new();
    public T editingItem { get; set; }
    //public string editingItemId { get; set; }

    public HttpClient http { get; set; }
    public DbState state { get; set; } = DbState.Uninitialized;

    public string connectionStringBase { get; set; }
    public string connectionStringArchive { get; set; }
    public string connectionStringKey { get; set; }

    public string connectionString => $"{connectionStringBase}{connectionStringKey}";
    public string connectionArchive => $"{connectionStringArchive}{connectionStringKey}";

    public DateTime lastLoaded { get; set; } = DateTime.Now;
    public int CooldownSeconds { get; set; } = 30;

    public T AddNew()
    {
        editingItem = Activator.CreateInstance<T>();
        return editingItem;
        //editingItemId = editingItem.id;
    }
    

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
        //https://collegeprepapi.azurewebsites.net/api/summerProgram/{id}?code=mzVo73ICTgPS9QcoqR37ItuGDVFS9dCXgf_13MA90eI-AzFuoQkuyA==
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
        if (await GetAllDbSpecificItemsAsync())
        {
            state = DbState.Loaded;
            lastLoaded = DateTime.Now;
            return;
        }

        state = DbState.Failed;
        lastLoaded = DateTime.Now;
    }

    private bool InsideRetryCooldown()
    {
        if (state is DbState.Failed or DbState.Loaded) return SecondsSinceLastLoaded() < CooldownSeconds;
        return false;
    }

    protected async Task<bool> GetAllDbSpecificItemsAsync()
    {
        var tempDbItems = await http.GetFromJsonAsync<List<T>>(connectionString);
        if (tempDbItems is null || tempDbItems.Count < 1) return false;

        dbItems.Clear();
        foreach (var tempDbItem in tempDbItems) dbItems.Add(tempDbItem);

        return true;
    }

    public async Task<bool> SubmitToDbAsync()
    {
        if (state == DbState.Loading) 
            return false;

        state = DbState.Loading;
        
        var postResponse = await http.PostAsJsonAsync(connectionString, editingItem);
        if (postResponse.IsSuccessStatusCode)
        {
            if (!dbItems.Contains(editingItem))
            {
                dbItems.Add(editingItem);
            }

            await SubmitNewSaveToArchiveAsync();
        }
        else
        {
            postResponse = 
                await http.PutAsJsonAsync(PutByIdConnectionString(editingItem.id), editingItem);
            if (postResponse.IsSuccessStatusCode)
            {
                await SubmitEditsToArchiveAsync();
            }
        }
        
        state = DbState.Loaded;
        lastLoaded = DateTime.Now;
        return postResponse.IsSuccessStatusCode;
        
    }

    public async Task<bool> SubmitNewSaveToDbAsync()
    {
        if (state == DbState.Loading) 
            return false;

        state = DbState.Loading;
        await SubmitNewSaveToArchiveAsync();
        
        var postResponse = await http.PostAsJsonAsync(connectionString, editingItem);
        if (postResponse.IsSuccessStatusCode && !dbItems.Contains(editingItem))
        {
            dbItems.Add(editingItem);
        }
        state = DbState.Loaded;
        lastLoaded = DateTime.Now;
        return postResponse.IsSuccessStatusCode;
    }

    private async Task SubmitNewSaveToArchiveAsync()
    {
        if (string.IsNullOrWhiteSpace(connectionStringArchive)) return;
        
        await http.PostAsJsonAsync(connectionArchive, editingItem);
    }

    public async Task<bool> SubmitEditsToDbAsync()
    {
        if (state == DbState.Loading) 
            return false;
        
        state = DbState.Loading;
        await SubmitEditsToArchiveAsync();
        
        var putResponse = 
            await http.PutAsJsonAsync(PutByIdConnectionString(editingItem.id), editingItem);
        state = DbState.Loaded;
        lastLoaded = DateTime.Now;
        return putResponse.IsSuccessStatusCode;
        
    }

    public IIdentifiable GetEditingIdentifiable()
    {
        return editingItem;
    }

    private async Task SubmitEditsToArchiveAsync()
    {
        if (string.IsNullOrWhiteSpace(connectionStringArchive)) return;
        
        await http.PutAsJsonAsync(PutArchiveByIdConnString(editingItem.id), editingItem);
    }

    public async Task<bool> DeleteFromDbAsync()
    {
        if (state == DbState.Loading) 
            return false;
        
        state = DbState.Loading;

        var deleteResponse = 
            await http.DeleteAsync(DeleteByIdConnectionString(editingItem.id));
        state = DbState.Loaded;
        lastLoaded = DateTime.Now;
        return deleteResponse.IsSuccessStatusCode;
    }

    //
    // public abstract Task BuildNewAndSetToEditor();
    // public abstract Task CloneAndSetToEditor();
    // public abstract Task<bool> SubmitEditsToDbAsync();
    // public abstract Task<bool> SubmitEditsToArchiveAsync();
    // public abstract Task<bool> DeleteFromDbAsync();
}