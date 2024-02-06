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

    public float Cost => throw new NotImplementedException();

    public List<Image> Items => throw new NotImplementedException();

    List<(PointF[], Brush)> faces = new();
    public bool OpenFloorMenu = false;
    public RectangleF MenuFloorMove { get; private set; }
    public RectangleF MenuFloorSpin { get; private set; }
    public RectangleF MenuFloorStore { get; private set; }
    public int Quantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Lamp()
    {
        Root = new Vector3(0f, 0f, 20f);
        add(
            (0f, 0f, 15f, 50f, 50f, 5f)
            .Parallelepiped()
            .Isometric(),
            Brushes.Gray
        );

        add(
            (27f, 27f, -60f, 4f, 4f, 80f)
            .Parallelepiped()
            .Isometric(),
            Brushes.Silver
        );

        for (int i = 0; i < 5; i++)
        {
            add(
                (10f + i, 10f + i, -57f - 3 * i, 30f - 2 * i, 30f - 2 * i, 3f)
                .Parallelepiped()
                .Isometric(),
                Brushes.LightYellow
            );
        }

    }

    private void add(PointF[][] faces, Brush baseBrush)
    {
        foreach (var face in faces)
        {
            this.faces.Add((face, baseBrush));
            baseBrush = getDarkerBrush(baseBrush);
        }
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
        var basePt = PointF.Empty.Isometric();
        g.TranslateTransform(basePt.X + x, basePt.Y + y);

        foreach (var face in faces)
            g.FillPolygon(face.Item2, face.Item1);

        g.ResetTransform();

        if(OpenFloorMenu)
        {
            MenuFloorMove = new RectangleF(x, y - 40, 400, 30);
            MenuFloorSpin = new RectangleF(x, MenuFloorMove.Y - 35, 400, 30);
            MenuFloorStore = new RectangleF(x, MenuFloorSpin.Y - 35, 400, 30);

            g.DrawRectangle(Pens.Red, MenuFloorMove);
            g.DrawRectangle(Pens.Blue, MenuFloorSpin);
            g.DrawRectangle(Pens.Yellow, MenuFloorStore);
        }
    }

    public void Click(PointF cursor)
    {
        if (OpenFloorMenu)
        {
            moving = !moving && MenuFloorMove.Contains(cursor);

            OpenFloorMenu = 
                MenuFloorMove.Contains(cursor) ||
                MenuFloorSpin.Contains(cursor) ||
                MenuFloorStore.Contains(cursor);
        }

        if (Room.Decorations[Room.IndexSelection.X, Room.IndexSelection.Y] == this)
            OpenFloorMenu = true;
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