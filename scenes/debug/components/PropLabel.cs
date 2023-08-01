using Godot;

[Tool]
public partial class PropLabel : Label
{
    private string _text;

    [Export]
    public string Content
    {
        get => _text;
        set => _text = value;
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            Text = this._text;
        }
    }
}