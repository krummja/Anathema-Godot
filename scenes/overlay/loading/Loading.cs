using Godot;

public partial class Loading : Control
{
    [Export]
    public ProgressBar ProgressBar;

    public bool IsLoading = false;

    public Director Director { get; set; }

    private float progress;

    public override void _Process(double delta)
    {
        if (this.IsLoading && this.Director != null)
        {
            this.progress = ((float)this.Director.Progress[0]) * 100;
            this.ProgressBar.Value = this.progress;
        }
    }
}