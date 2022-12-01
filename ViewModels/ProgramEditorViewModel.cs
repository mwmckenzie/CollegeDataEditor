// Markov Chain Sim -- ProgramEditorViewModel.cs
// 
// Copyright (C) 2022 Matthew W. McKenzie and Kenz LLC
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using CollegeDataEditor.Enums;
using CollegeDataEditor.Interfaces;
using CollegeDataEditor.Models;
using CollegeDataEditor.Services;
using MudBlazor;

namespace CollegeDataEditor.ViewModels;

public class ProgramEditorViewModel : IDbContext
{
    
    public event Action? OnEditsSentToDb;
    public event Action<string>? OnSuccessfulSaveToDb;
    public event Action<string>? OnUnsuccessfulSaveToDb;

    public event Action? NotifyNewSummerProgramSet;
    
    public DbService dbService { get; set; }

    private SummerProgram _selectedProgram;
    public SummerProgram selectedProgram
    {
        get => _selectedProgram;
        set
        {
            if (_selectedProgram is not null)
            {
                _selectedProgram.NotifyObjEdited -= SelectedProgramOnNotifyObjEdited;
            }
            _selectedProgram = value;
            _selectedProgram.NotifyObjEdited += SelectedProgramOnNotifyObjEdited;
            OnNewSummerProgramSet();
        }
    }

    /// <summary>
    ///  Applications
    /// </summary>
    public SideViewViewModel ApplicationSideView { get; set; } = new();

    private bool applicationSet;
    private Application _application;
    public Application application
    {
        get => _application;
        set {
            if (_application is not null)
            {
                _application.NotifyObjEdited -= SelectedApplicationOnNotifyObjEdited;
            }
            _application = value;
            _application.NotifyObjEdited += SelectedApplicationOnNotifyObjEdited;
            applicationSet = true;
        }
    }

    private async void SelectedApplicationOnNotifyObjEdited()
    {
        await SubmitApplicationEditsToDbAsync();
    }

    private async void SelectedProgramOnNotifyObjEdited()
    {
        await SaveEditsToDbAsync();
    }
    
    /// <summary>
    ///  Sessions
    /// </summary>
    public SideViewViewModel SessionSideView { get; set; } = new();

    private bool sessionSet;
    private Session _session;
    public Session session
    {
        get => _session;
        set {
            if (_session is not null)
            {
                _session.NotifyObjEdited -= SelectedSessionOnNotifyObjEdited;
            }
            _session = value;
            _session.NotifyObjEdited += SelectedSessionOnNotifyObjEdited;
            sessionSet = true;
        }
    }

    private async void SelectedSessionOnNotifyObjEdited()
    {
        await SubmitSessionEditsToDbAsync();
    }

    private async Task<bool> SubmitSessionEditsToDbAsync()
    {
        if (!sessionSet)
        {
            return true;
        }
        var db = dbService.SessionsDb;
        db.editingItem = session;

        if (await db.SubmitToDbAsync())
        {
            NotifyDataSavedToDb("Session");
            return true;
        }
        NotifySaveToDbFailed("Session");
        return false;
    }
    
    /// <summary>
    ///  Student Info
    /// </summary>
    public SideViewViewModel StudentInfoSideView { get; set; } = new();

    private bool _studentInfoSet;
    private StudentInfo _studentInfo;
    public StudentInfo studentInfo
    {
        get => _studentInfo;
        set {
            if (_studentInfo is not null)
            {
                _studentInfo.NotifyObjEdited -= SelectedStudentInfoOnNotifyObjEdited;
            }
            _studentInfo = value;
            _studentInfo.NotifyObjEdited += SelectedStudentInfoOnNotifyObjEdited;
            _studentInfoSet = true;
        }
    }

    private async void SelectedStudentInfoOnNotifyObjEdited()
    {
        await SubmitStudentInfoEditsToDbAsync();
    }

    private async Task<bool> SubmitStudentInfoEditsToDbAsync()
    {
        if (!_studentInfoSet)
        {
            return true;
        }
        var db = dbService.StudentInfoDb;
        db.editingItem = studentInfo;

        if (await db.SubmitToDbAsync())
        {
            NotifyDataSavedToDb("StudentInfo");
            return true;
        }
        NotifySaveToDbFailed("StudentInfo");
        return false;
    }


    public List<IndexedValue>? searchList { get; set; }
    
    public async Task<IEnumerable<IndexedValue>> SearchIndexedValues(string value)
    {
        if (searchList is null)
        {
            return null;
        }
        return string.IsNullOrEmpty(value) ? 
            searchList : 
            searchList.
                Where(x => x.value.
                Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }
    
    private void NotifyDataSentToDb() {
        OnEditsSentToDb?.Invoke();
    }
    private void NotifyDataSavedToDb(string dbType = "Unknown") {
        OnSuccessfulSaveToDb?.Invoke(dbType);
    }
    private void NotifySaveToDbFailed(string dbType = "Unknown") {
        OnUnsuccessfulSaveToDb?.Invoke(dbType);
    }

    public async Task<bool> SubmitApplicationEditsToDbAsync()
    {
        if (!applicationSet)
        {
            return true;
        }
        var db = dbService.ApplicationsDb;
        db.editingItem = application;

        if (await db.SubmitToDbAsync())
        {
            NotifyDataSavedToDb("Application");
            return true;
        }
        NotifySaveToDbFailed("Application");
        return false;
    }

    public async Task<bool> SubmitEditsToDbAsync()
    {
        var db = dbService.SummerProgramsDb;
        db.editingItem = selectedProgram;

        if (await db.SubmitToDbAsync())
        {
            NotifyDataSavedToDb("SummerProgram");
            await SubmitApplicationEditsToDbAsync();
            await SubmitSessionEditsToDbAsync();
            await SubmitStudentInfoEditsToDbAsync();
            return true;
        }
        NotifySaveToDbFailed("SummerProgram");
        await SubmitApplicationEditsToDbAsync();
        await SubmitSessionEditsToDbAsync();
        await SubmitStudentInfoEditsToDbAsync();
        return false;

        // if (db.dbItems.Any(x => x.id == db.editingItem.id))
        // {
        //     return await SaveEditsToDbAsync();
        // }
        // return await SaveNewToDbAsync();
    }

    public async Task AddNewIndexedValueToDbAsync(IndexedValue indexedValue)
    {
        switch (indexedValue.valueType)
        {
            case IndexedValueType.Tag:
                dbService.TagDb.editingItem = indexedValue;
                await dbService.TagDb.SubmitToDbAsync();
                break;
            case IndexedValueType.Subject:
                dbService.SubjectsDb.editingItem = indexedValue;
                await dbService.SubjectsDb.SubmitToDbAsync();
                break;
            case IndexedValueType.Topic:
                dbService.TopicsDb.editingItem = indexedValue;
                await dbService.TopicsDb.SubmitToDbAsync();
                break;
            case IndexedValueType.ProgramType:
                dbService.ProgramTypesDb.editingItem = indexedValue;
                await dbService.ProgramTypesDb.SubmitToDbAsync();
                break;
            case IndexedValueType.OrgType:
                dbService.OrgTypesDb.editingItem = indexedValue;
                await dbService.OrgTypesDb.SubmitToDbAsync();
                break;
            default:
                return;
        }
        
    }

    public IIdentifiable CreateNewEditingIdentifiable()
    {
        selectedProgram = new SummerProgram();
        return selectedProgram;
    }

    public IIdentifiable GetEditingIdentifiable()
    {
        return _selectedProgram;
    }

    private async Task<bool> SaveNewToDbAsync()
    {
        var db = dbService.SummerProgramsDb;
        if (await db.SubmitNewSaveToDbAsync())
        {
            NotifyDataSavedToDb();
            return true;
        }
        NotifySaveToDbFailed();
        return false;
    }

    private async Task<bool> SaveEditsToDbAsync()
    {
        var db = dbService.SummerProgramsDb;
        if (await db.SubmitEditsToDbAsync())
        {
            NotifyDataSavedToDb();
            return true;
        }
        NotifySaveToDbFailed();
        return false;
    }
    
    public async Task<bool> DeleteSelectedProgram()
    {
        dbService.SummerProgramsDb.editingItem = selectedProgram;
        if (await dbService.SummerProgramsDb.DeleteFromDbAsync())
        {
            NotifyDataSavedToDb();
            return true;
        }
        NotifySaveToDbFailed();
        return false;
    }


    protected virtual void OnNewSummerProgramSet()
    {
        NotifyNewSummerProgramSet?.Invoke();
    }
}