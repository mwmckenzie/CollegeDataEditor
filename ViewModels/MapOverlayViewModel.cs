namespace CollegeDataEditor.ViewModels;

public class MapOverlayViewModel
{
    public string mapFilename { get; set; }
    public bool mapOverlayIsVisible { get; set; }

    public void DisplayMap(string filename)
    {
        mapFilename = filename;
        mapOverlayIsVisible = true;
    }
}