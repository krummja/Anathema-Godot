using Godot;


public partial class BootState : State<States>
{
    public BootState(Main main) : base(main, States.BOOT)
    {
        this.Name = "__Boot";
    }

    public override void Enter()
    {
        this.main.StateMachine.TransitionTo(States.MENU);
    }
}
