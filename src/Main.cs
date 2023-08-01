using Godot;

public partial class Main : Node
{
    [Export]
    public Node State;

    [Export]
    public Director Director;

    [Export]
    public World World;

    [Export]
    public Overlay Overlay;

    public StateMachine<States> StateMachine { get; private set; }

    private bool canExit = false;

    public override void _Ready()
    {
        // Set up state machine and register states.
        this.StateMachine = new StateMachine<States>(this);
        this.StateMachine.Add(new BootState(this));
        this.StateMachine.Add(new MenuState(this));
        this.StateMachine.Add(new ExitState(this));
        this.StateMachine.Add(new LoadState(this));
        this.StateMachine.Add(new RunState(this));

        // Transition to BOOT to start the state system.
        this.StateMachine.TransitionTo(States.BOOT);
    }

    public override void _Process(double delta)
    {
        if (this.canExit)
            GetTree().Quit();
    }

    public void PrepareExit()
    {
        // Check that all systems have shut down, game is saved, etc.
        this.canExit = true;
    }
}