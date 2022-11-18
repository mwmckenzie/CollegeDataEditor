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

using CollegeDataEditor.Models;
using ValueType = CollegeDataEditor.Enums.ValueType;

namespace CollegeDataEditor.Factories;

public class IndexedValueFactory : Factory
{
    public static IndexedValue Create(ValueType valueType, string value)
    {
        var val = new IndexedValue()
        {
            id = Guid.NewGuid().ToString(),
            value = value,
            valueType = valueType,
        };

        switch (valueType)
        {
            case ValueType.Tag:
                val.typeName = "Tag";
                break;
            case ValueType.Subject:
                val.typeName = "Subject";
                break;
            case ValueType.Topic:
                val.typeName = "Topic";
                break;
            case ValueType.ProgramType:
                val.typeName = "ProgramType";
                break;
            case ValueType.OrgType:
                val.typeName = "OrgType";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
        }

        return val;
    }
}