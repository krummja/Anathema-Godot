using Godot;

public partial class NewGameMenu : Control
{
    [Signal]
    public delegate void ButtonPressedEventHandler(StackableMenuEvent @event);

    [Signal]
    public delegate void StartGamePressedEventHandler();

    [Export]
    public Button Start;

    [Export]
    public Button Back;

    public override void _Ready()
    {
        this.Start.Pressed += OnStartPressed;
        this.Back.Pressed += OnBackPressed;
    }

    private void OnStartPressed()
    {
        GD.Print("Start Game Pressed");
        EmitSignal(SignalName.StartGamePressed);
    }

    private void OnBackPressed()
    {
        EmitSignal(SignalName.ButtonPressed, (int)StackableMenuEvent.BACK);
    }
}