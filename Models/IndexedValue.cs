﻿// Markov Chain Sim -- IndexedValue.cs
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

namespace CollegeDataEditor.Models;

public class IndexedValue : IIdentifiable
{
    public string? id { get; set; }
    public string? value { get; set; }
    public string? typeName { get; set; }
    public IndexedValueType valueType { get; set; }

    public override string ToString()
    {
        return value ?? string.Empty;
    }
}