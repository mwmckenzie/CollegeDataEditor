using System.Net.Http.Json;

namespace CollegeDataEditor.Models;

public class PathogenDb : DbCaptrs
{
    public PathogenDb()
    {
        connectionStringBase = "https://pathogensapi.azurewebsites.net/api/pathogen";
        connectionStringArchive = "https://pathogensapi.azurewebsites.net/api/archive";
        connectionStringKey = "?code=captrs";
        // "https://pathogensapi.azurewebsites.net/api/pathogen?code=cdtNGBg2z0nHTUzTzEAxHqVuIsCyOnmZEULRaguOPvfYAzFus8dMCQ=="
    }

    public List<Pathogen>? loadedPathogens { get; set; } = new();
    public List<Pathogen> dirtyPathogens { get; set; } = new();
    public List<Pathogen> selectedPathogens { get; set; } = new();

    public Pathogen? pathogenSetForEditing { get; set; }
    public Pathogen? pathogenSetForDeleting { get; set; }

    protected override async Task<bool> GetAllDbSpecificItems()
    {
        loadedPathogens = await http.GetFromJsonAsync<List<Pathogen>>(connectionString);
        return loadedPathogens is not null;
    }

    public override Task CloneAndSetToEditor()
    {
        if (pathogenSetForEditing is null) return Task.CompletedTask;
        pathogenSetForEditing = (Pathogen)pathogenSetForEditing.Clone();
        pathogenSetForEditing.id = Guid.NewGuid().ToString();
        pathogenSetForEditing.typeName += " (Copy)";

        return Task.CompletedTask;
    }

    public override Task BuildNewAndSetToEditor()
    {
        pathogenSetForEditing = new Pathogen
        {
            isValid = true,
            acuteSymptoms = new List<string>(),
            reservoirDetails = new List<string>()
        };
        return Task.CompletedTask;
    }

    public override async Task<bool> SubmitEditsToDbAsync()
    {
        if (pathogenSetForEditing?.id is null)
            return false;

        var editedId = pathogenSetForEditing.id;
        await SubmitEditsToArchiveAsync();

        if (loadedPathogens is not null && loadedPathogens.Any(x => x.id == editedId))
        {
            var putResponse =
                await http.PutAsJsonAsync(PutByIdConnectionString(editedId), pathogenSetForEditing);
            return putResponse.IsSuccessStatusCode;
        }

        var postResponse = await http.PostAsJsonAsync(connectionString, pathogenSetForEditing);
        return postResponse.IsSuccessStatusCode;
    }

    public override async Task<bool> SubmitEditsToArchiveAsync()
    {
        if (pathogenSetForEditing?.id is null)
            return false;

        var editedId = pathogenSetForEditing.id;
        var putResponse =
            await http.PutAsJsonAsync(PutArchiveByIdConnString(editedId), pathogenSetForEditing);

        if (putResponse.IsSuccessStatusCode) return true;

        var postResponse = await http.PostAsJsonAsync(connectionArchive, pathogenSetForEditing);
        return postResponse.IsSuccessStatusCode;
    }

    public override async Task<bool> DeleteFromDbAsync()
    {
        if (pathogenSetForDeleting?.id is null)
            return false;

        var editedId = pathogenSetForDeleting.id;
        var response = await http.DeleteAsync(DeleteByIdConnectionString(editedId));

        if (!response.IsSuccessStatusCode)
            return response.IsSuccessStatusCode;

        loadedPathogens?.Remove(pathogenSetForDeleting);
        pathogenSetForDeleting = null;

        return response.IsSuccessStatusCode;
    }
}