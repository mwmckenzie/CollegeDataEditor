using System.ComponentModel.DataAnnotations;
using CollegeDataEditor.Helpers;
using CollegeDataEditor.Interfaces;

namespace CollegeDataEditor.Models;

public class CardBaseObj : BaseInfoObj, IDropTableItem
{
    private readonly string _name;

    [Display(Name = "Icon")] public string icon { get; set; } = StaticRefs.captrsIcon;

    [Display(Name = "Card Type")] public string cardType { get; set; } = string.Empty;

    [Display(Name = "Card Title")] public string cardTitle { get; set; } = string.Empty;

    [Display(Name = "Text Content Sections")]
    public List<string> textContentSections { get; set; } = new();

    public bool isSpecialText { get; set; }

    public string Name
    {
        get => ToString();
        init => _name = value;
    }

    public string Selector { get; set; } = "1";
    public int Order { get; set; }


    public override string ToString()
    {
        var typeText = string.IsNullOrWhiteSpace(cardType) ? "Unknown Type" : cardType;
        var titleText = string.IsNullOrWhiteSpace(cardTitle) ? "Unknown Title" : cardTitle;
        return $"{typeText}: {titleText}";
    }

    public CardBaseObj Copy()
    {
        var card = new CardBaseObj
        {
            Name = name ?? string.Empty,
            Selector = Selector,
            Order = -1,
            name = name,
            displayName = displayName,
            isValid = isValid,
            id = Guid.NewGuid().ToString(),
            url = url,
            icon = icon,
            cardType = cardType,
            cardTitle = cardTitle,
            isSpecialText = isSpecialText
        };

        foreach (var textContentSection in textContentSections) card.textContentSections.Add(textContentSection);

        return card;
    }
}