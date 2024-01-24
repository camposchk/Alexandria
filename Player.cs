using System;
using System.Drawing;

public class Player 
{
    public float X { get; set; } = 0;
    public float Y { get; set; } = 0;
    public float Z { get; set; } = 0;
    public float Width { get; set; } = 50;
    public float Height { get; set; } = 50;
    public float Depth { get; set; } = 100;
    public int Ruby { get; set; } = 100;

    public void Draw(Graphics g, float x, float y, float z, float width, float height, float depth, Color baseColor)
    {
        PointF[][] player = (x, y, 20f - depth, width, height, depth)
            .Parallelepiped()
            .Isometric();

        Brush brush = new SolidBrush(baseColor);
        foreach (var face in player)
        {
            g.FillPolygon(brush, face);
            brush = GetDarkerBrush(brush);
        }
    }

    private PointF target = new(300, 300);
    public void StartMove(PointF destiny)
    {
        target = destiny;
    }
    public void Move()
    {
        var dx = target.X - X;
        var dy = target.Y - Y;
        var mod = MathF.Sqrt(dx * dx + dy * dy);
        if (mod < 10)
            return;

        dx *= 10 / mod;
        dy *= 10 / mod;

        X += dx;
        Y += dy;
    }

    Brush GetDarkerBrush(Brush originalBrush)
    {
        Color originalColor = ((SolidBrush)originalBrush).Color;

        float factor = 0.8f;

        int red = (int)(originalColor.R * factor);
        int green = (int)(originalColor.G * factor);
        int blue = (int)(originalColor.B * factor);

        Color darkerColor = Color.FromArgb(red, green, blue);

        return new SolidBrush(darkerColor);
    }
}