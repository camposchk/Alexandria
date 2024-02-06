using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class Player 
{
    public float X { get; set; } = 200;
    public float Y { get; set; } = -400;
    public float Z { get; set; } = 0;
    public float Width { get; set; } = 50;
    public float Height { get; set; } = 50;
    public float Depth { get; set; } = 100;
    public float Ruby { get; set; } = 100;
    public Color Color { get; set; } = Colors.GetRandomColor();

    List<Message> Messages = new List<Message>();

    private List<IPlayerOutfit> outfits = new List<IPlayerOutfit>();
    public List<IDecoration> purchasedDecorations = new List<IDecoration>();

    public void AddOutfit(IPlayerOutfit outfit)
    {
        outfits.Add(outfit);
    }

    public void Draw(Graphics g)
    {
        Draw(g, X, Y, Z, Width, Height, Depth, Color);
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

        Messages = Messages
            .Where(m => m.IsActivated)
            .ToList();
        foreach (var message in Messages)
        {
            message.Draw(g);
        }
    }

    public void Speak(string text)
    {
        var ballon = new PointF((int)this.X, (int)this.Y).Isometric();
        Message message = new Message(ballon, text, (int)this.Depth);
        this.Messages.Add(message);
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
        if (Ruby >= deco.Cost && deco.Quantity > 0)
        {
            Ruby -= deco.Cost;
            deco.Quantity -= 1;
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