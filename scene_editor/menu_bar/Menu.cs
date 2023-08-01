using Godot;

public partial class Menu : MenuBar
{
    private PopupMenu addMenu;

    public override void _Ready()
    {
        addMenu = GetMenuPopup(0);
        addMenu.AddItem("Test Node");
        addMenu.IdPressed += OnAddPressed;
    }

    private void OnAddPressed(long id)
    {
        GD.Print(id);
    }
}