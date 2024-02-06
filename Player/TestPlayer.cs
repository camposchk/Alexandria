using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

public class TestPlayer : IDecoration
{
    public Vector3 Root { get; set; }
    public float X { get; set; } = 200;
    public float Y { get; set; } = -400;
    public float Z { get; set; } = 0;
    public float Width { get; set; } = 50;
    public float Height { get; set; } = 50;
    public float Depth { get; set; } = 100;
    public float Ruby { get; set; } = 100;
    public Color Color { get; set; } = Colors.GetRandomColor();
    public Room Room { get; set; }

    public float Cost => 0f;

    public int Quantity { get; set; }

    public List<Image> Items => throw new NotImplementedException();

    List<Message> Messages = new List<Message>();

    public TestPlayer()
    {
        Root = new Vector3(0f, 0f, 20f);
    }

    public void Speak(string text)
    {
        var ballon = new PointF((int)this.X, (int)this.Y).Isometric();
        Message message = new Message(ballon, text, (int)this.Depth);
        this.Messages.Add(message);
    }

    Brush getDarkerBrush(Brush originalBrush)
    {
        Color originalColor = ((SolidBrush)originalBrush).Color;

        float factor = 0.9f;

        int red = (int)(originalColor.R * factor);
        int green = (int)(originalColor.G * factor);
        int blue = (int)(originalColor.B * factor);

        Color darkerColor = Color.FromArgb(red, green, blue);

        return new SolidBrush(darkerColor);
    }

    public void Draw(Graphics g, float x, float y)
    {
        PointF[][] player = (x, y, 20f - Depth, Width, Height, Depth)
            .Parallelepiped()
            .Isometric();

        Brush brush = new SolidBrush(Color);
        foreach (var face in player)
        {
            g.FillPolygon(brush, face);
            brush = GetDarkerBrush(brush);
        }

        Messages = Messages
            .Where(m => m.IsActivated)
            .ToList();
        foreach (var message in Messages)
        {
            message.Draw(g);
        }
    }

    public void Move()
    {

    }

    public void StartMove(PointF point)
    {

    }

    public Brush GetDarkerBrush(Brush originalBrush)
    {
        Color originalColor = ((SolidBrush)originalBrush).Color;

        float factor = 0.8f;

        int red = (int)(originalColor.R * factor);
        int green = (int)(originalColor.G * factor);
        int blue = (int)(originalColor.B * factor);

        Color darkerColor = Color.FromArgb(red, green, blue);

        return new SolidBrush(darkerColor);
    }

    public void TryMove(Point mouseLocation)
    {
        
    }

    public void Move(Point mouseLocation)
    {
        
    }

    public void Spin() { }

    public void Store() { }
}