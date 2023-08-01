using Godot;

public partial class SplashScreen : Control
{
    [Signal]
    public delegate void OnSplashCompleteEventHandler();

    [Export]
    public AnimationPlayer SplashAnimator;

    [Export]
    public AnimationPlayer TransitionAnimator;

    [Export]
    public Label DebugLabel;

    public override void _Ready()
    {
        SplashAnimator.AnimationFinished += (name) =>
        {
            if (name == "splash")
                TransitionAnimator.Play("fade_in");
        };

        TransitionAnimator.AnimationFinished += (name) =>
        {
            if (name == "fade_in")
                EmitSignal(SignalName.OnSplashComplete);
        };

        SplashAnimator.Play("splash");
    }
}