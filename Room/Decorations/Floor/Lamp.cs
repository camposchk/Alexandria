using System;
using System.Drawing;
using System.Numerics;
using System.Collections.Generic;
public class Lamp : IDecoration
{
    public Room Room { get; set; }
    private bool moving = false;
    public Vector3 Root { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public float Cost => 10;

    public List<Image> Items => throw new NotImplementedException();

    List<(PointF[], Brush)> faces = new();
    public int Quantity { get; set; } = 3;
    public float SizeFactor { get; set; } = 1f;

    public Lamp()
    {
        Root = new Vector3(0f, 0f, 20f);
        add(
            (0f, 0f, 15f, 40f, 40f, 5f)
            .Parallelepiped()
            .Isometric(),
            new SolidBrush(Colors.GetRandomColor())
        );

        add(
            (27f, 27f, -60f, 4f, 4f, 80f)
            .Parallelepiped()
            .Isometric(),
            new SolidBrush(Colors.GetRandomColor())
        );

        var lampBrush = new SolidBrush(Colors.GetRandomColor());
        for (int i = 0; i < 10; i++)
        {
            add(
                (10f + i, 10f + i, -57f - 3 * i, 40f - 2 * i, 40f - 2 * i, 5f)
                .Parallelepiped()
                .Isometric(),
                lampBrush
            );
        }

    }

    private void add(PointF[][] faces, Brush baseBrush)
    {
        foreach (var face in faces)
        {
            this.faces.Add((face, baseBrush));
            baseBrush = Colors.GetDarkerBrush(baseBrush);
        }
    }
    
    public void Draw(Graphics g, float x, float y)
    {
        var basePt = PointF.Empty.Isometric();
        g.TranslateTransform(basePt.X + x, basePt.Y + y);
        g.ScaleTransform(SizeFactor, SizeFactor);

        foreach (var face in faces)
            g.FillPolygon(face.Item2, face.Item1);

        g.ResetTransform();

    }

    public void Click(PointF cursor)
    {
        if (Room.Decorations[Room.IndexSelection.X, Room.IndexSelection.Y] == this)
            moving = !moving;
    }

    public void TryMove(Point mouseLocation)
    {
        if (!moving)
            return;
        var selected = Room.Decorations[
            Room.IndexSelection.X,
            Room.IndexSelection.Y
        ];
        
        if (selected is not null)
            return;
        
        Room.Remove(this);
        Room.Set(this,
            Room.IndexSelection.X,
            Room.IndexSelection.Y
        );
    }

    public void Spin()
    {
        throw new NotImplementedException();
    }

    public void Store()
    {
        throw new NotImplementedException();
    }

    public void Draw(Graphics g)
    {
        throw new NotImplementedException();
    }

    public void Move(Point mouseLocation)
    {
        throw new NotImplementedException();
    }
}