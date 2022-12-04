// Markov Chain Sim -- SchoolBrowserViewModel.cs
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

using CollegeDataEditor.Helpers;
using CollegeDataEditor.Models;
using MudBlazor;

namespace CollegeDataEditor.ViewModels;

public class SchoolBrowserViewModel
{
    // Events 
    public event Action? OnDefaultListDisplay;
    public event Action? OnSchoolSelected;
    
    // DB
    public Db<School> db;
    private bool dbInitialized = false;

    // Helpers
    private SchoolLookUps _lookUps = new();
    
    // Schools
    private School _selectedSchool = new();
    public School selectedSchool
    {
        get => _selectedSchool;
        set
        {
            _selectedSchool = value;
            OnSchoolSelected?.Invoke();
        }
    }

    private List<School> _schoolsToDisplay = new();
    public List<School> schoolsToDisplay
    {
        get => _schoolsToDisplay;
        set => _schoolsToDisplay = value;
    }

    private List<School> _schools = new();
    public List<School> schools
    {
        get => _schools;
        set => _schools = value;
    }
    
    public int schoolCount => schools.Count;
    
    // States
    public string stateFullName => 
        _lookUps.GetStateFullNameByFip(_selectedStateAbbrev.Key);
    
    private KeyValuePair<int, string> _selectedState;
    public KeyValuePair<int, string> selectedState
    {
        get => _selectedState;
        set => _selectedState = value;
    }
    private Dictionary<int, string> _states = new();
    public Dictionary<int, string> states
    {
        get => _states;
        set => _states = value;
    }
    public int stateCount => _states.Count;

    // Controls
    public bool dense { get; set; } = true;
    public bool hover { get; set; } = true;
    public bool striped { get; set; }
    public bool bordered { get; set; }
    public string searchString1 { get; set; } = "";
    public int rowsPerPage { get; set; } = 25;
    
    // Side Panel
    public bool open { get; set; }
    public Anchor anchor { get; set; } = Anchor.End;
    public string width { get; set; } = "40%";
    public string height { get; set; } = "100%";
    
    private KeyValuePair<int, string> _selectedStateAbbrev;
    public KeyValuePair<int, string> selectedStateAbbrev
    {
        get => _selectedStateAbbrev;
        set
        {
            _selectedStateAbbrev = value;
            OnStateAbbrevSelected();
        }
    }
    
    private void OnStateAbbrevSelected()
    {
        schoolsToDisplay = schools
            .Where(x => x.StateFips == _selectedStateAbbrev.Key).ToList();
    }

    private Dictionary<int, string> _stateAbbrevs = new();
    public Dictionary<int, string> stateAbbrevs
    {
        get => _stateAbbrevs;
        set => _stateAbbrevs = value;
    }

    public void SetDb(Db<School> dbIn)
    {
        if (dbInitialized)
        {
            return;
        }
        db = dbIn;
        schools = db.dbItems.OrderBy(x => x.Name).ToList();
        schoolsToDisplay = schools;

        foreach (var school in schools)
        {
            if (school?.StateFips is null || stateAbbrevs.ContainsKey((int)school.StateFips))
            {
                continue;
            }
            stateAbbrevs.Add((int)school.StateFips, school.State);
        }

        dbInitialized = true;
    }
    
    public bool FilterFunc1(School school) {
        return FilterFunc(school, searchString1);
    }

    public bool FilterFunc(School school, string searchString) {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (school.Name is not null) {
            if (school.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        if (school.Alias is not null) {
            if (school.Alias.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return $"{school.State} {school.City} {school.SchoolUrl}".Contains(searchString);
    }
    
}