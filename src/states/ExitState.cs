using Godot;

public partial class ExitState : State<States>
{
    public ExitState(Main main) : base(main, States.EXIT)
    {
        this.Name = "__Exit";
    }

    public override void Enter()
    {
        SceneTree tree = this.main.GetTree();
        tree.Root.PropagateNotification((int)Node.NotificationWMCloseRequest);
        this.main.PrepareExit();
    }
}