using System;
using System.Collections.Generic;
using System.Drawing;

public class Player 
{
    public float X { get; set; } = 0;
    public float Y { get; set; } = 0;
    public float Z { get; set; } = 0;
    public float Width { get; set; } = 50;
    public float Height { get; set; } = 50;
    public float Depth { get; set; } = 100;
    public float Ruby { get; set; } = 100;

    public bool IsSpeaking = false;

    private List<IPlayerOutfit> outfits = new List<IPlayerOutfit>();
    public List<IDecoration> purchasedDecorations = new List<IDecoration>();

    public void AddOutfit(IPlayerOutfit outfit)
    {
        outfits.Add(outfit);
    }

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

        foreach (var outfit in outfits)
        {
            outfit.Draw(g, this);
        }

        if (IsSpeaking)
        {
            g.DrawRectangle(Pens.Red, 100, 100, 100, 100);
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

    public void Buy(IDecoration deco)
    {
        if (Ruby >= deco.Cost)
        {
            Ruby -= deco.Cost;
            purchasedDecorations.Add(deco);
        }
        else
        {
            // Não tem rubis suficientes. Lidar com isso (mensagem de erro, etc.)
        }
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
}