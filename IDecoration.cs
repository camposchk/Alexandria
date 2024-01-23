using System;

public interface IDecoration
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Depth { get; set; }

    public void Place() { } 
    public void Move() { } 
}