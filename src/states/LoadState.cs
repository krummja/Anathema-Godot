public partial class LoadState : State<States>
{
    private bool isLoading = false;

    public LoadState(Main main) : base(main, States.LOAD)
    {
        this.Name = "__Load";
    }

    public override void Enter()
    {
        Loading loading = this.main.Director.LoadImmediate<Loading>(
            "loading",
            "loading",
            MountPoint.OVERLAY
        );

        loading.IsLoading = true;
        loading.Director = this.main.Director;

        this.main.Overlay.Push(loading);
        this.main.Director.LoadStarted += OnLoadStarted;
        this.main.Director.LoadComplete += OnLoadComplete;

        if (!this.isLoading)
        {
            this.main.Director.StartLoad(
                "areas",
                "testing_area",
                MountPoint.WORLD
            );
        }
    }

    public override void Exit()
    {
        this.main.Director.LoadStarted -= OnLoadStarted;
        this.main.Director.LoadComplete -= OnLoadComplete;

        Loading loading = this.main.Overlay.Pop<Loading>();
        loading.IsLoading = false;
    }

    private void OnLoadStarted()
    {
        this.isLoading = true;
    }

    private void OnLoadComplete()
    {
        this.main.StateMachine.TransitionTo(States.RUN);
    }
}