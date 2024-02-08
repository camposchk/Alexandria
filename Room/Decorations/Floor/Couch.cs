using System;
using System.Drawing;
using System.Numerics;
using System.Collections.Generic;
public class Couch : IDecoration
{
    public Room Room { get; set; }
    private bool moving = false;
    public Vector3 Root { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public float Cost => 40;

    public List<Image> Items => throw new NotImplementedException();

    List<(PointF[], Brush)> faces = new();
    public bool OpenFloorMenu = false;
    public RectangleF MenuFloorMove { get; private set; }
    public RectangleF MenuFloorSpin { get; private set; }
    public RectangleF MenuFloorStore { get; private set; }
    public int Quantity { get; set; } = 10;
    public float SizeFactor { get; set; } = 1f;
    SolidBrush color = new SolidBrush(Colors.GetRandomColor());

    public Couch()
    {
        Root = new Vector3(0f, 0f, 20f);

        add(
            (0f, 30f, -40f, 130f, 20f, 60f)
            .Parallelepiped()
            .Isometric(),
            color
        );    

        add(
            (150f, 20f, 0f, 20f, 50f, 40f)
            .Parallelepiped()
            .Isometric(),
            color
        );

        add(
            (20f, 0f, 0f, 110f, 50f, 20f)
            .Parallelepiped()
            .Isometric(),
            new SolidBrush(Colors.GetRandomColor())
        );

        add(
            (20f, 20f, -20f, 20f, 30f, 60f)
            .Parallelepiped()
            .Isometric(),
            color
        );    
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