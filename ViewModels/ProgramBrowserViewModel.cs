// Markov Chain Sim -- ProgramBrowserViewModel.cs
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

using CollegeDataEditor.Models;
using CollegeDataEditor.Services;

namespace CollegeDataEditor.ViewModels;

public class ProgramBrowserViewModel
{
    
    public event Action? OnValueChange;
    public event Action? OnDefaultListDisplay;
    public event Action? OnProgramSelected;
    
    public const string DefaultFilterType = "Subject";
    
    public DbService? dbService { get; set; }

    public List<SummerProgramObj> programsAll { get; set; } = new();
    public List<SummerProgramObj> programsToDisplay { get; set; } = new();

    private SummerProgramObj _selectedObj = new();
    public SummerProgramObj selectedObj
    {
        get => _selectedObj;
        set
        {
            _selectedObj = value;
            NotifyProgramSelected();
        }
    }

    public int rowsPerPage { get; set; } = 25;
    
    public bool dense { get; set; }
    public bool hover { get; set; } = true;
    public bool striped { get; set; }
    public bool bordered { get; set; }
    
    public string searchString1 { get; set; } = string.Empty;
    private string _selectedFilter = string.Empty;
    private IndexedValue _selectedFilterObj = new();
    
    public IndexedValue selectedFilterObj
    {
        get => _selectedFilterObj;
        set
        {
            _selectedFilterObj = value;
            OnSelectedFilterObjChanged();
        } 
    }
    
    public string selectedFilter
    {
        get => _selectedFilter;
        set
        {
            if (value == _selectedFilter)
            {
                return;
            }
            _selectedFilter = value;
            //OnSelectedFilterChanged();
        }
    }

    public string selectedFilterType { get; set; } = DefaultFilterType;

    

    public int programCount => programsAll.Count;
    
    private void NotifyDataStateChanged() {
        OnValueChange?.Invoke();
    }

    private void NotifyDefaultListDisplay()
    {
        OnDefaultListDisplay?.Invoke();
    }

    private void NotifyProgramSelected()
    {
        OnProgramSelected?.Invoke();
    }

    public void SetProgramsToDisplay()
    {
        if (programsToDisplay.Count < 1)
        {
            LoadDefaultList();
        }
    }

    private bool FiltersAreInvalid() => 
        string.IsNullOrWhiteSpace(selectedFilter) 
        || string.IsNullOrWhiteSpace(selectedFilterType);

    public void ClearSelectedFilter()
    {
        LoadDefaultList();
    }

    private void OnSelectedFilterObjChanged()
    {
        if (string.IsNullOrWhiteSpace(selectedFilterObj.id) || dbService is null) {
            LoadDefaultList();
            return;
        }
        
        switch (selectedFilterObj.typeName) {
            case "Subject":
                programsToDisplay = programsAll.
                    Where(x => 
                        x.subjectIdList.Contains(selectedFilterObj.id)).ToList();
                NotifyDataStateChanged();
                break;
            case "Topic":
                programsToDisplay = programsAll.
                    Where(x => 
                        x.topicIdList.Contains(selectedFilterObj.id)).ToList();
                NotifyDataStateChanged();
                break;
            case "Tag":
                programsToDisplay = programsAll.
                    Where(x => 
                        x.tagIdList.Contains(selectedFilterObj.id)).ToList();
                NotifyDataStateChanged();
                break;
            case "ProgramType":
                programsToDisplay = programsAll.
                    Where(x => 
                        x.programTypeIdList.Contains(selectedFilterObj.id)).ToList();
                NotifyDataStateChanged();
                break;
            default:
                LoadDefaultList();
                break;
        }
    }
    
    // private void OnSelectedFilterChanged() {
    //     if (FiltersAreInvalid() || dbService is null) {
    //         LoadDefaultList();
    //         return;
    //     }
    //
    //     var filterId = string.Empty;
    //     switch (selectedFilterType) {
    //         case "Subject":
    //             filterId = GetFilterId(dbService.SubjectsDb.dbItems);
    //             programsToDisplay = programsAll.
    //                 Where(x => 
    //                     x.subjectIdList.Contains(filterId)).ToList();
    //             NotifyDataStateChanged();
    //             break;
    //         case "Topic":
    //             filterId = GetFilterId(dbService.TopicsDb.dbItems);
    //             programsToDisplay = programsAll.
    //                 Where(x => 
    //                     x.topicIdList.Contains(filterId)).ToList();
    //             NotifyDataStateChanged();
    //             break;
    //         case "Tag":
    //             filterId = GetFilterId(dbService.TagDb.dbItems);
    //             programsToDisplay = programsAll.
    //                 Where(x => 
    //                     x.tagIdList.Contains(filterId)).ToList();
    //             NotifyDataStateChanged();
    //             break;
    //         default:
    //             LoadDefaultList();
    //             break;
    //     }
    // }

    private void LoadDefaultList()
    {
        programsToDisplay = programsAll;
        NotifyDefaultListDisplay();
        // NotifyDataStateChanged();
    }
    
    

    private string GetFilterId(IEnumerable<IndexedValue> dbItemList)
    {
        var filter = selectedFilter;
        var filterId = dbItemList.First(x => x.value == filter).id;
        return filterId ?? string.Empty;
    }

    // private void OnSelectedSubjectChanged()
    // {
    //     var subjId = _dbService.SubjectsDb.dbItems;
    //     var getId = subjId.Select(x => x.value).FirstOrDefault(x => x == _programBrowserVM.selectedFilter);
    //     // var subjectId = programDataService.GetSubjectId(_selectedFilter);
    //     // _programsToDisplay =
    //     //     programDataService.programList.Where(x => x.subjectIdList.Contains(subjectId)).ToList();
    //
    //     if (getId is null)
    //     {
    //         return;
    //     }
    //     _programBrowserVM.programsToDisplay = _programBrowserVM.programsAll.Where(x => x.subjectIdList.Contains(getId)).ToList();
    //     StateHasChanged();
    // }
    
    // private void OnSelectedTopicChanged() {
    //     // var topicId = programDataService.GetTopicId(_selectedFilter);
    //     // _programsToDisplay =
    //     //     programDataService.programList.Where(x => x.topicIdList.Contains(topicId)).ToList();
    //     // StateHasChanged();
    // }
}