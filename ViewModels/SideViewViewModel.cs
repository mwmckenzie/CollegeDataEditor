using MudBlazor;

namespace CollegeDataEditor.ViewModels;

public class SideViewViewModel
{
    public Anchor anchor = Anchor.Right;
    public string height = "100%";

    public bool open;
    public string width = "40%";

    public void DisplaySideView()
    {
        open = true;
    }
    public void CloseSideView()
    {
        open = false;
    }
}