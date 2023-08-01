using Godot;

[Tool]
public partial class RowProp : MarginContainer
{
    [Export]
    public string PropLabelContent;

    private HBoxContainer row;
    private PropLabel propLabel;
    private Label valueLabel;

    public override void _Ready()
    {
        this.row = GetNode<HBoxContainer>("./Row");
        this.valueLabel = GetNode<Label>("./Row/Value");
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            if (this.propLabel == null)
                this.propLabel = GetNode<PropLabel>("./Row/Property");
            this.propLabel.Content = PropLabelContent;
        }
    }
}