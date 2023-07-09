using Godot;
using System.Collections.Generic;


public partial class Overlay : Control
{
    public Stack<Node> Screens { get; private set; }

    public override void _Ready()
    {
        this.Screens = new Stack<Node>();
    }

    public void Push(Node screen)
    {
        this.Screens.Push(screen);
        GD.Print(this.Screens.Count);
    }

    public T Pop<T>() where T : Node
    {
        Node node = this.Screens.Pop();
        RemoveChild(node);
        node.QueueFree();
        return node as T;
    }
}
