using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

public class Player : IDecoration
{
    private float targetX = float.NaN;
    private float targetY = float.NaN;
    private float ftargetX = float.NaN;
    private float ftargetY = float.NaN;
    public List<IDecoration> purchasedDecorations = new List<IDecoration>();

    public Vector3 Root { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float Width { get; set; } = 50;
    public float Height { get; set; } = 50;
    public float Depth { get; set; } = 100;
    public float Ruby { get; set; } = 100;
    public Color Color { get; set; } = Colors.GetRandomColor();
    public Room Room { get; set; }
    private List<IPlayerOutfit> outfits = new List<IPlayerOutfit>();

    public float Cost => 0f;

    public int Quantity { get; set; }

    public List<Image> Items => throw new NotImplementedException();

    int i = 0, j = 0;
    int fi = 0, fj = 0;
    List<Message> Messages = new List<Message>();

    public Player()
    {
        Root = new Vector3(0f, 0f, 20f);
    }

    public void Speak(string text)
    {
        var ballon = new PointF((int)this.X, (int)this.Y).Isometric();
        Message message = new Message(ballon, text, (int)this.Depth);
        this.Messages.Add(message);
    }

    public void Draw(Graphics g, float x, float y)
    {
        var normal = new PointF(x, y).Normal();
        if (float.IsNaN(targetX))
        {
            this.X = normal.X;
            this.Y = normal.Y;
        }
        PointF[][] player = (this.X, this.Y, 20f - Depth, Width, Height, Depth)
            .Parallelepiped()
            .Isometric();

        Brush brush = new SolidBrush(Color);
        foreach (var face in player)
        {
            g.FillPolygon(brush, face);
            brush = Colors.GetDarkerBrush(brush);
        }

        Messages = Messages
            .Where(m => m.IsActivated)
            .ToList();
        foreach (var message in Messages)
        {
            message.Draw(g);
        }

        foreach (var outfit in outfits)
        {
            outfit.Draw(g, this);
        }

        this.i = Room.IndexSelection.X;
        this.j = Room.IndexSelection.Y;
        targetX = Room.NormalSelection.X;
        targetY = Room.NormalSelection.Y;
    }

    DateTime last = DateTime.Now;
    bool activated = false;
    public void MovePlayer()
    {
        var now = DateTime.Now;
        var timePassed = now - last;
        last = now;
        var secs = (float)timePassed.TotalSeconds;
        if (!activated)
            return;

        var dx = ftargetX - X;
        var dy = ftargetY - Y;
        var mod = MathF.Sqrt(dx * dx + dy * dy);
        if (mod < 2f)
        {
            activated = false;
            Room.Remove(this);
            Room.Set(this, fi, fj);
            return;
        }

        var dirX = dx / mod;
        var dirY = dy / mod;
        X += 300 * dirX * secs;
        Y += 300 * dirY * secs;
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

    public void AddOutfit(IPlayerOutfit outfit)
    {
        outfits.Add(outfit);
    }

    public void ActivePlayer()
    {
        if (Room.Decorations[i, j] is not null)
            return;
        activated = true;
        ftargetX = targetX;
        ftargetY = targetY;
        fi = i;
        fj = j;
        return;
    }

    public void TryMove(Point mouseLocation) { }

    public void Spin() { }

    public void Store() { }
}