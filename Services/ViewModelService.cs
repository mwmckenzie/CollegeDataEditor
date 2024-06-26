﻿// Markov Chain Sim -- ViewModelService.cs
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

using CollegeDataEditor.ViewModels;

namespace CollegeDataEditor.Services;

public class ViewModelService
{
    public ProgramBrowserViewModel ProgramBrowserVM = new();
    public IndexViewModel IndexViewModel = new();
    public ProgramEditorViewModel ProgramEditorViewModel = new();
    public ProgramViewModel ProgramVM = new();
    public SchoolBrowserViewModel schoolBrowserVM = new();
    
    public IndexedValueDbViewModel TagDbVM = new();
    public IndexedValueDbViewModel SubjectDbVM = new();
    public IndexedValueDbViewModel TopicDbVM = new();
    public IndexedValueDbViewModel ProgramTypeDbVM = new();
    public IndexedValueDbViewModel OrgTypeDbVM = new();
    public IndexedValueDbViewModel ResidenceDbVm = new();
    public IndexedValueDbViewModel CitizenshipDbVM = new();
}