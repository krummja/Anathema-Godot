using Godot;
using Godot.Collections;

public enum MountPoint
{
    OVERLAY,
    WORLD,
}

public partial class Director : Node
{
    [Signal]
    public delegate void LoadCompleteEventHandler();

    [Signal]
    public delegate void LoadStartedEventHandler();

    [Export]
    public string WorldPath = "res://scenes/world/";

    [Export]
    public string OverlayPath = "res://scenes/overlay/";

    [Export]
    public Node2D World;

    [Export]
    public Control Overlay;

    private bool isLoading = false;
    private PackedScene loadedScene;
    private MountPoint mountPoint;
    private string scenePath;
    private Dictionary<MountPoint, string> scenePaths;

    public Array Progress { get; private set; }

    public override void _Ready()
    {
        scenePaths = new Dictionary<MountPoint, string>();
        scenePaths[MountPoint.WORLD] = WorldPath;
        scenePaths[MountPoint.OVERLAY] = OverlayPath;
    }

    public override void _Process(double delta)
    {
        ResourceLoader.ThreadLoadStatus status = ResourceLoader
            .LoadThreadedGetStatus(scenePath, Progress);

        while (isLoading)
        {
            switch (status)
            {
                case ResourceLoader.ThreadLoadStatus.InProgress:
                    return;

                case ResourceLoader.ThreadLoadStatus.Loaded:
                    loadedScene = ResourceLoader
                        .LoadThreadedGet(scenePath) as PackedScene;

                    EmitSignal(SignalName.LoadComplete);
                    this.Mount(mountPoint);
                    this.isLoading = false;
                    return;

                case ResourceLoader.ThreadLoadStatus.Failed:
                    GD.PrintErr("Resource loading failed");
                    return;

                case ResourceLoader.ThreadLoadStatus.InvalidResource:
                    GD.PrintErr("Invalid resource");
                    return;

                default:
                    GD.PrintErr("State not found");
                    return;
            }
        }
    }

    public T LoadImmediate<T>(string folder, string scene, MountPoint mount)
        where T : Node
    {
        this.SetScenePath(folder, scene, mount);
        this.loadedScene = (PackedScene)ResourceLoader.Load(scenePath);
        Node mounted = this.Mount(mount);

        EmitSignal(SignalName.LoadComplete);

        return mounted as T;
    }

    public void StartLoad(string folder, string scene, MountPoint mount)
    {
        this.SetScenePath(folder, scene, mount);
        this.mountPoint = mount;
        this.Progress = new Array();

        EmitSignal(SignalName.LoadStarted);

        this.isLoading = true;
        ResourceLoader.LoadThreadedRequest(scenePath);
    }

    public Node GetLoadedScene()
    {
        return this.loadedScene.Instantiate();
    }

    private Node Mount(MountPoint mount)
    {
        if (this.loadedScene == null)
            return null;

        Node child = GetLoadedScene();

        switch (mount)
        {
            case MountPoint.WORLD:
                this.World.AddChild(child);
                break;

            case MountPoint.OVERLAY:
                this.Overlay.AddChild(child);
                break;
        }

        return child;
    }

    private void SetScenePath(string folder, string scene, MountPoint mount)
    {
        this.scenePath = $"{scenePaths[mount]}/{folder}/{scene}.tscn";
    }
}