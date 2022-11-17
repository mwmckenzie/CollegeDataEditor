// Markov Chain Sim -- BaseCollegeDataObj.cs
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

namespace CollegeDataEditor.Models;

public class BaseCollegeDataObj : BaseInfoObj
{
    private List<string> tagIdList { get; } = new();
    private List<string> aliasList { get; } = new();
    private List<string> urlList { get; } = new();
    private List<string> noteList { get; } = new();
    

    public virtual void Add(SumProgElements elementType, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;
        switch (elementType)
        {
            case SumProgElements.Alias:
                OnElementAdded(aliasList, value);
                break;
            case SumProgElements.Note:
                OnElementAdded(noteList, value);
                break;
            case SumProgElements.Url:
                var fixedUrl = BuildValidUrl(value);
                if (string.IsNullOrWhiteSpace(url)) url = fixedUrl;
                OnElementAdded(urlList, fixedUrl);
                break;
            case SumProgElements.TagId:
                OnElementAdded(tagIdList, value);
                break;
        }
    }

    private void OnElementAdded(ICollection<string> list, string value)
    {
        if (list.Contains(value)) return;
        list.Add(value);
        OnBaseUpdate();
    }

    private void OnElementRemoved(ICollection<string> list, string value)
    {
        if (!list.Contains(value)) return;
        list.Remove(value);
        OnBaseUpdate();
    }

    private string BuildValidUrl(string urlText)
    {
        if (urlText.Contains("https://", StringComparison.OrdinalIgnoreCase)) return urlText;
        urlText = urlText.Replace("http://", string.Empty);
        return $"https://{urlText}";
    }

    // public virtual void Add(SumProgElements elementType, int value) {
    //     switch (elementType) {
    //         case SumProgElements.TagId:
    //             if (!tagIdList.Contains(value)) {
    //                 tagIdList.Add(value);
    //             }
    //             break;
    //     }
    // }
}