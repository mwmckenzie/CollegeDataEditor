// Markov Chain Sim -- KenzExtensions.cs
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

namespace CollegeDataEditor.Helpers;

public static class KenzExtensions
{
    public static string FixUrlPrefix(this string str)
    {
        return str.Contains("https://") ? str : $"https://{str}";
    }
    
    public static string ShortenUrl(this string str)
    {
        str.FixUrlPrefix();
        if (str.Contains('/') && str.Split('/').Length >= 4)
        {
            var replace = str.Split('/', 4)[3];
            return replace.Length > 0 ? str.Replace(replace, string.Empty) + "..."
                : str;
        }
        return str;
    }
}