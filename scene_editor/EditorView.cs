using System;
using Godot;


public partial class EditorView : Control
{
    private VSeparator separator;

    private Nullable<Vector2> clickedPos;

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            clickedPos = mouseButton.Position;
        }

        else if (@event is InputEventMouseMotion mouseMotion)
        {
            clickedPos = null;
        }
    }
}
