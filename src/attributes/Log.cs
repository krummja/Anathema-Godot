using Godot;
using System;


[AttributeUsage(AttributeTargets.Method)]
public class LogAttribute : Attribute
{
    public LogAttribute(string message)
    {
        GD.Print(message);
    }
}
