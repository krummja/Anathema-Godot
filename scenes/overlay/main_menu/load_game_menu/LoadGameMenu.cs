using Godot;

public partial class LoadGameMenu : Control
{
    [Signal]
    public delegate void ButtonPressedEventHandler(StackableMenuEvent @event);

    [Export]
    public Button Back;

    public override void _Ready()
    {
        this.Back.Pressed += OnBackPressed;
    }

    public void OnBackPressed()
    {
        EmitSignal(SignalName.ButtonPressed, (int)StackableMenuEvent.BACK);
    }
}