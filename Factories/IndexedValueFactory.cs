// Markov Chain Sim -- IndexedValueFactory.cs
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

namespace CollegeDataEditor.Factories;

public class IndexedValueFactory : Factory
{
    public static IndexedValue Create(IndexedValueType valueType, string value)
    {
        var val = new IndexedValue()
        {
            id = Guid.NewGuid().ToString(),
            value = value,
            valueType = valueType,
        };

        switch (valueType)
        {
            case IndexedValueType.Tag:
                val.typeName = "Tag";
                break;
            case IndexedValueType.Subject:
                val.typeName = "Subject";
                break;
            case IndexedValueType.Topic:
                val.typeName = "Topic";
                break;
            case IndexedValueType.ProgramType:
                val.typeName = "ProgramType";
                break;
            case IndexedValueType.OrgType:
                val.typeName = "OrgType";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
        }

        return val;
    }
}