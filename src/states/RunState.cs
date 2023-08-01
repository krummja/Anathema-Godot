using Godot;

public partial class RunState : State<States>
{
    public RunState(Main main) : base(main, States.RUN)
    {
        this.Name = "__Run";
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent)
        {
            if (keyEvent.Keycode == Key.Escape)
            {
                this.main.StateMachine.TransitionTo(States.MENU);
            }
        }
    }
}