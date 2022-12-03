// Markov Chain Sim -- LookupService.cs
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

namespace CollegeDataEditor.Services;

public class LookupService
{
    public DbService dbService { get; set; }
    
    public LookupService(DbService service)
    {
        dbService = service;
    }

    public List<IndexedValue> subjects => dbService.SubjectsDb.dbItems;
    public List<IndexedValue> topics => dbService.TopicsDb.dbItems;
    public List<IndexedValue> tags => dbService.TagDb.dbItems;
    public List<IndexedValue> orgTypes => dbService.OrgTypesDb.dbItems;
    public List<IndexedValue> programTypes => dbService.ProgramTypesDb.dbItems;
    public List<IndexedValue> citizenshipList => dbService.CitizenshipsDb.dbItems;
    public List<IndexedValue> residences => dbService.ResidencesDb.dbItems;

    public List<SummerProgram> summerPrograms => dbService.SummerProgramsDb.dbItems;
    public List<Org> orgs => dbService.OrgsDb.dbItems;
    public List<Application> applications => dbService.ApplicationsDb.dbItems;
    public List<Session> sessions => dbService.SessionsDb.dbItems;
    public List<StudentInfo> studentInfoList => dbService.StudentInfoDb.dbItems;


    public IndexedValue FindById(List<IndexedValue> list, string searchId)
    {
        return list.FirstOrDefault(x => x.id.Equals(searchId, StringComparison.Ordinal));
    }

    public IndexedValue FindSubjectById(string searchId) => FindById(subjects, searchId);
    public IndexedValue FindTopicById(string searchId) => FindById(topics, searchId);
    public IndexedValue FindTagById(string searchId) => FindById(tags, searchId);
    public IndexedValue FindOrgTypeById(string searchId) => FindById(orgTypes, searchId);
    public IndexedValue FindProgramTypeById(string searchId) => FindById(programTypes, searchId);
    public IndexedValue FindCitizenshipById(string searchId) => FindById(citizenshipList, searchId);
    public IndexedValue FindResidenceById(string searchId) => FindById(residences, searchId);
    
    public Org FindById(List<Org> list, string searchId)
    {
        return list.FirstOrDefault(x => x.id.Equals(searchId, StringComparison.Ordinal));
    }
    
    public Org FindOrgById(string searchId) => FindById(orgs, searchId);
    
    
    public SummerProgram FindById(List<SummerProgram> list, string searchId)
    {
        return list.FirstOrDefault(x => x.id.Equals(searchId, StringComparison.Ordinal));
    }
    
    public SummerProgram FindSummerProgramById(string searchId) => FindById(summerPrograms, searchId);
    
    public Application FindById(List<Application> list, string searchId)
    {
        return list.FirstOrDefault(x => x.id.Equals(searchId, StringComparison.Ordinal));
    }
    
    public Application FindApplicationById(string searchId) => FindById(applications, searchId);
    
    public Session FindById(List<Session> list, string searchId)
    {
        return list.FirstOrDefault(x => x.id.Equals(searchId, StringComparison.Ordinal));
    }
    
    public Session FindSessionById(string searchId) => FindById(sessions, searchId);
    
    public StudentInfo FindById(List<StudentInfo> list, string searchId)
    {
        return list.FirstOrDefault(x => x.id.Equals(searchId, StringComparison.Ordinal));
    }
    
    public StudentInfo FindStudentInfoById(string searchId) => FindById(studentInfoList, searchId);
    
    public List<string> ProgramAggregateTagIdList(SummerProgram program)
    {
        var list = new List<string>();
        if (program is null)
        {
            return list;
        }
        
        AddIdList(program.tagIdList, list);
        
        foreach (var id in program.applicationIdList)
        {
            var idListObj = FindApplicationById(id);
            if (idListObj is null)
            {
                continue;
            }
            AddIdList(idListObj.tagIdList, list);
        }
        
        foreach (var id in program.sessionIdList)
        {
            var idListObj = FindSessionById(id);
            if (idListObj is null)
            {
                continue;
            }
            AddIdList(idListObj.tagIdList, list);
        }
        
        foreach (var id in program.studentInfoIdList)
        {
            var idListObj = FindStudentInfoById(id);
            if (idListObj is null)
            {
                continue;
            }
            AddIdList(idListObj.tagIdList, list);
        }
        
        foreach (var id in program.orgIdList)
        {
            var idListObj = FindOrgById(id);
            if (idListObj is null)
            {
                continue;
            }
            AddIdList(idListObj.tagIdList, list);
        }
        
        void AddIdList(List<string> idList, List<string> addToList)
        {
            if (idList is null || idList.Count < 1)
            {
                return;
            }
            addToList.AddRange(idList);
        }
        return list;
    }
}