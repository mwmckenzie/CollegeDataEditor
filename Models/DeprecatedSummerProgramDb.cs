// Markov Chain Sim -- SummerProgramDb.cs
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

namespace CollegeDataEditor.Models;

public class DeprecatedSummerProgramDb
{
    private readonly List<SummerProgramObj> _summerPrograms = new();


    public void Add(SummerProgramObj obj)
    {
        if (_summerPrograms.Contains(obj)) return;
        _summerPrograms.Add(obj);
        obj.summerProgram.Subscribe(OnObjUpdated);
    }

    private void OnObjUpdated(SummerProgramObj summerProgramObj)
    {
        throw new NotImplementedException();
    }
}