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
using MudBlazor;

namespace CollegeDataEditor.ViewModels;

public class ProgramBrowserViewModel
{
    
    public event Action? OnValueChange;
    public event Action? OnDefaultListDisplay;
    public event Action? OnProgramSelected;
    
    public const string DefaultFilterType = "Subject";
    
    public DbService? dbService { get; set; }
    public LookupService? lookUpService { get; set; }

    public List<SummerProgram> programsAll { get; set; } = new();
    public List<SummerProgram> programsToDisplay { get; set; } = new();

    private SummerProgram _selected = new();
    public SummerProgram selected
    {
        get => _selected;
        set
        {
            _selected = value;
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

    public DateRange dateRange { get; set; } 
        = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(5).Date);

    public int programCount => programsAll.Count;
    
    public bool filterBySubject { get; set; } = true;
    public bool filterByTopic { get; set; } = true;
    public bool filterByTag { get; set; } = true;
    public bool filterByProgramType { get; set; } = true;
    public bool filterByOrg { get; set; } = true;
    public bool filterByDate { get; set; } = true;
    public bool filterBySchool { get; set; } = true;
    public bool filterByAlarm { get; set; } = false;

    private IndexedValue _selectedSubjFilter = new();
    public IndexedValue selectedSubjFilter
    {
        get => _selectedSubjFilter;
        set
        {
            _selectedSubjFilter = value;
            OnSelectedFilterObjChanged();
        } 
    }
    
    private IndexedValue _selectedTopicFilter = new();
    public IndexedValue selectedTopicFilter
    {
        get => _selectedTopicFilter;
        set
        {
            _selectedTopicFilter = value;
            OnSelectedFilterObjChanged();
        } 
    }
    
    private IndexedValue _selectedTagFilter = new();
    public IndexedValue selectedTagFilter
    {
        get => _selectedTagFilter;
        set
        {
            _selectedTagFilter = value;
            OnSelectedFilterObjChanged();
        } 
    }
    
    private IndexedValue _selectedOrgFilter = new();
    public IndexedValue selectedOrgFilter
    {
        get => _selectedOrgFilter;
        set
        {
            _selectedOrgFilter = value;
            OnSelectedFilterObjChanged();
        } 
    }
    
    private IndexedValue _selectedSchoolFilter = new();
    public IndexedValue selectedSchoolFilter
    {
        get => _selectedSchoolFilter;
        set
        {
            _selectedSchoolFilter = value;
            OnSelectedFilterObjChanged();
        } 
    }

    private IndexedValue _selectedProgramTypeFilter = new();
    public IndexedValue selectedProgramTypeFilter
    {
        get => _selectedProgramTypeFilter;
        set
        {
            _selectedProgramTypeFilter = value;
            OnSelectedFilterObjChanged();
        } 
    }
    
    public bool hasSubjFilter => 
        !string.IsNullOrWhiteSpace(_selectedSubjFilter.value) && filterBySubject;
        
    public bool hasTopicFilter => 
        !string.IsNullOrWhiteSpace(_selectedTopicFilter.value) && filterByTopic;
        
    public bool hasTagFilter => 
        !string.IsNullOrWhiteSpace(_selectedTagFilter.value) && filterByTag;
        
    public bool hasProgTypeFilter => 
        !string.IsNullOrWhiteSpace(_selectedProgramTypeFilter.value) && filterByProgramType;
        
    public bool hasOrgFilter => 
        !string.IsNullOrWhiteSpace(_selectedOrgFilter.value) && filterByOrg;
    
    public bool hasActiveFilter => hasOrgFilter || hasSubjFilter || hasTagFilter 
                                   || hasTopicFilter || hasProgTypeFilter;
    
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
        selectedSubjFilter = new();
        selectedTopicFilter = new();
        selectedTagFilter = new();
        selectedOrgFilter = new();
        selectedProgramTypeFilter = new();
        selectedSchoolFilter = new();
        
        LoadDefaultList();
    }

    private void OnSelectedFilterObjChanged()
    {
        if ( dbService is null) {
            LoadDefaultList();
            return;
        }

        bool changesMade = false;

        var filteredPrograms = new List<SummerProgram>();
        filteredPrograms.AddRange(programsAll);

        if (!string.IsNullOrWhiteSpace(_selectedSubjFilter.id) && filterBySubject)
        {
            filteredPrograms = filteredPrograms.
                Where(x => 
                    x.subjectIdList.Contains(_selectedSubjFilter.id)).ToList();
            changesMade = true;
        }

        if (!string.IsNullOrWhiteSpace(_selectedTopicFilter.id) && filterByTopic)
        {
            filteredPrograms = filteredPrograms.
                Where(x => 
                    x.topicIdList.Contains(_selectedTopicFilter.id)).ToList();
            changesMade = true;
        }

        if (!string.IsNullOrWhiteSpace(_selectedTagFilter.id) && filterByTag)
        {
            filteredPrograms = filteredPrograms.Where(x =>
                    lookUpService.ProgramAggregateTagIdList(x)
                        .Contains(_selectedTagFilter.id))
                .ToList();
            changesMade = true;
        }

        if (!string.IsNullOrWhiteSpace(_selectedProgramTypeFilter.id) && filterByProgramType)
        {
            programsToDisplay = programsAll.
                Where(x => 
                    x.programTypeIdList
                        .Contains(_selectedProgramTypeFilter.id))
                .ToList();
            changesMade = true;

        }

        if (changesMade)
        {
            programsToDisplay = filteredPrograms;
        }
        else
        {
            LoadDefaultList();
        }
        NotifyDataStateChanged();
        
        // switch (selectedFilterObj.typeName) {
        //     case "Subject":
        //         programsToDisplay = programsAll.
        //             Where(x => 
        //                 x.subjectIdList.Contains(selectedFilterObj.id)).ToList();
        //         NotifyDataStateChanged();
        //         break;
        //     case "Topic":
        //         programsToDisplay = programsAll.
        //             Where(x => 
        //                 x.topicIdList.Contains(selectedFilterObj.id)).ToList();
        //         NotifyDataStateChanged();
        //         break;
        //     case "Tag":
        //         programsToDisplay = programsAll.Where(x =>
        //                 lookUpService.ProgramAggregateTagIdList(x).Contains(selectedFilterObj.id))
        //             .ToList();
        //             //x.tagIdList.Contains(selectedFilterObj.id)).ToList();
        //         NotifyDataStateChanged();
        //         break;
        //     case "ProgramType":
        //         programsToDisplay = programsAll.
        //             Where(x => 
        //                 x.programTypeIdList.Contains(selectedFilterObj.id)).ToList();
        //         NotifyDataStateChanged();
        //         break;
        //     default:
        //         LoadDefaultList();
        //         break;
        // }
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
    
    public void ReloadProgramsFromDb()
    {
        programsAll = dbService.SummerProgramsDb.dbItems
            .OrderBy(x => x.displayName)
            .ToList();
        LoadDefaultList();
        NotifyDataStateChanged();
    }

    private string GetFilterId(IEnumerable<IndexedValue> dbItemList)
    {
        var filter = selectedFilter;
        var filterId = dbItemList.First(x => x.value == filter).id;
        return filterId ?? string.Empty;
    }
    
    public async Task<bool> DeleteSelectedProgram()
    {
        dbService.SummerProgramsDb.editingItem = selected;
        return await dbService.SummerProgramsDb.DeleteFromDbAsync();
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