// Markov Chain Sim -- IndexedValueDbViewModel.cs
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
using CollegeDataEditor.Models;
using CollegeDataEditor.Services;

namespace CollegeDataEditor.ViewModels;

public class IndexedValueDbViewModel
{

    public DbService dbService { get; set; }
    public Db<IndexedValue> db { get; set; }
    public IndexedValueType valueType { get; set; }

    public int rowsPerPage { get; set; } = 50;
    public bool dense { get; set; } = true;
    public bool hover { get; set; } = true;
    public bool ronly { get; set; } = false;
    public bool canCancelEdit { get; set; } = false;
    public bool blockSwitch { get; set; } = false;
    public string searchString { get; set; } = "";
    public IndexedValue selectedItem1 { get; set; } = null;
    public IndexedValue indexedValueBeforeEdit { get; set; }

    public IndexedValue editItem
    {
        get => db.editingItem;
        set => db.editingItem = value;
    }

    public async Task RemoveIdFromAllAsync(string idToRemove)
    {
        switch (valueType)
        {
            case IndexedValueType.Tag:
                await RemoveTagIdFromAllAsync(idToRemove);
                break;
            case IndexedValueType.Subject:
                await RemoveSubjectIdFromAllAsync(idToRemove);
                break;
            case IndexedValueType.Topic:
                await RemoveTopicIdFromAllAsync(idToRemove);
                break;
            case IndexedValueType.ProgramType:
                await RemoveProgramTypeIdFromAllAsync(idToRemove);
                break;
            case IndexedValueType.OrgType:
                await RemoveOrgTypeIdFromAllAsync(idToRemove);
                break;
            case IndexedValueType.Citizenship:
                await RemoveCitizenshipIdFromAllAsync(idToRemove);
                break;
            case IndexedValueType.Residence:
                await RemoveResidenceIdFromAllAsync(idToRemove);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    private async Task RemoveTagIdFromAllAsync(string idToRemove)
    {
        foreach (var obj in dbService.SummerProgramsDb.dbItems.
                     Where(x => x.tagIdList.Contains(idToRemove)))
        {
            dbService.SummerProgramsDb.editingItem = obj;
            dbService.SummerProgramsDb.editingItem.tagIdList.Remove(idToRemove);
            var result = await dbService.SummerProgramsDb.SubmitToDbAsync();
        }

        foreach (var obj in dbService.ApplicationsDb.dbItems.
                     Where(x => x.tagIdList.Contains(idToRemove)))
        {
            dbService.ApplicationsDb.editingItem = obj;
            dbService.ApplicationsDb.editingItem.tagIdList.Remove(idToRemove);
            await dbService.ApplicationsDb.SubmitToDbAsync();
        }

        foreach (var obj in dbService.SessionsDb.dbItems.
                     Where(x => x.tagIdList.Contains(idToRemove)))
        {
            dbService.SessionsDb.editingItem = obj;
            dbService.SessionsDb.editingItem.tagIdList.Remove(idToRemove);
            await dbService.SessionsDb.SubmitToDbAsync();
        }

        foreach (var obj in dbService.OrgsDb.dbItems.
                     Where(x => x.tagIdList.Contains(idToRemove)))
        {
            dbService.OrgsDb.editingItem = obj;
            dbService.OrgsDb.editingItem.tagIdList.Remove(idToRemove);
            await dbService.OrgsDb.SubmitToDbAsync();
        }

        foreach (var obj in dbService.StudentInfoDb.dbItems.
                     Where(x => x.tagIdList.Contains(idToRemove)))
        {
            dbService.StudentInfoDb.editingItem = obj;
            dbService.StudentInfoDb.editingItem.tagIdList.Remove(idToRemove);
            await dbService.StudentInfoDb.SubmitToDbAsync();
        }
    }
    
    private async Task RemoveSubjectIdFromAllAsync(string idToRemove)
    {
        foreach (var obj in dbService.SummerProgramsDb.dbItems.
                     Where(x =>  x.subjectIdList.Contains(idToRemove)))
        {
            dbService.SummerProgramsDb.editingItem = obj;
            dbService.SummerProgramsDb.editingItem.subjectIdList.Remove(idToRemove);
            await dbService.SummerProgramsDb.SubmitToDbAsync();
        }
    }
    
    private async Task RemoveTopicIdFromAllAsync(string idToRemove)
    {
        foreach (var obj in dbService.SummerProgramsDb.dbItems.
                     Where(x => x.topicIdList.Contains(idToRemove)))
        {
            dbService.SummerProgramsDb.editingItem = obj;
            dbService.SummerProgramsDb.editingItem.topicIdList.Remove(idToRemove);
            var result = await dbService.SummerProgramsDb.SubmitToDbAsync();
        }
    }
    
    private async Task RemoveProgramTypeIdFromAllAsync(string idToRemove)
    {
        foreach (var obj in dbService.SummerProgramsDb.dbItems.
                     Where(x => x.programTypeIdList.Contains(idToRemove)))
        {
            dbService.SummerProgramsDb.editingItem = obj;
            dbService.SummerProgramsDb.editingItem.programTypeIdList.Remove(idToRemove);
            var result = await dbService.SummerProgramsDb.SubmitToDbAsync();
        }
    }
    
    private async Task RemoveOrgTypeIdFromAllAsync(string idToRemove)
    {
        foreach (var obj in dbService.OrgsDb.dbItems.
                     Where(x => x.orgTypeId != null && x.orgTypeId.Contains(idToRemove)))
        {
            dbService.OrgsDb.editingItem = obj;
            dbService.OrgsDb.editingItem.orgTypeId = null;
            await dbService.OrgsDb.SubmitToDbAsync();
        }
    }
    
    private async Task RemoveCitizenshipIdFromAllAsync(string idToRemove)
    {
        foreach (var obj in dbService.StudentInfoDb.dbItems.
                     Where(x => x.citizenshipIdList != null && x.citizenshipIdList.Contains(idToRemove)))
        {
            dbService.StudentInfoDb.editingItem = obj;
            dbService.StudentInfoDb.editingItem.citizenshipIdList.Remove(idToRemove);
            await dbService.StudentInfoDb.SubmitToDbAsync();
        }
    }
    
    private async Task RemoveResidenceIdFromAllAsync(string idToRemove)
    {
        foreach (var obj in dbService.StudentInfoDb.dbItems.
                     Where(x => x.residenceIdList != null && x.residenceIdList.Contains(idToRemove)))
        {
            dbService.StudentInfoDb.editingItem = obj;
            dbService.StudentInfoDb.editingItem.residenceIdList.Remove(idToRemove);
            await dbService.StudentInfoDb.SubmitToDbAsync();
        }
    }
}