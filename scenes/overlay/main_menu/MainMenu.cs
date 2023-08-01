using Godot;

public enum MainMenuEvent
{
    NEW_GAME,
    LOAD_GAME,
    EXIT,
}

public enum StackableMenuEvent
{
    BACK,
}

public partial class MainMenu : Control
{
    [Signal]
    public delegate void ButtonPressedEventHandler(MainMenuEvent @event);

    [Export]
    public Button NewGame;

    [Export]
    public Button LoadGame;

    [Export]
    public Button Exit;

    public override void _Ready()
    {
        this.NewGame.Pressed += OnNewGamePressed;
        this.LoadGame.Pressed += OnLoadGamePressed;
        this.Exit.Pressed += OnExitPressed;
    }

    private void OnNewGamePressed()
    {
        EmitSignal(SignalName.ButtonPressed, (int)MainMenuEvent.NEW_GAME);
    }

    private void OnLoadGamePressed()
    {
        EmitSignal(SignalName.ButtonPressed, (int)MainMenuEvent.LOAD_GAME);
    }

    private void OnExitPressed()
    {
        EmitSignal(SignalName.ButtonPressed, (int)MainMenuEvent.EXIT);
    }
}