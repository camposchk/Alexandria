using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class FloorDecoration : IDecoration
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Depth { get; set; }

    public int TileSize { get; set; }
    public List<Image> Items { get; set; }

    public List<RectangleF> Bounds { get; private set; } = new List<RectangleF>();

    public bool MoveOn = false;

    public bool Clicked = false;

    public Point lastMouseLocation = Point.Empty;

    public FloorDecoration(float x, float y, float width, float height, float depth, int tilesize, string imgPath)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
        this.Depth = depth;
        this.TileSize = tilesize;

        this.Items = new(){Bitmap.FromFile(imgPath)};

        updateBounds();
    }

    void updateBounds()
    {
        Bounds.Clear();

        PointF[][] floorDecoration = (this.X, this.Y, 20f, this.Width, this.Height, this.Depth)
            .Parallelepiped()
            .Isometric();

        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        foreach (var face in floorDecoration)
        {
            foreach (var point in face)
            {
                if (point.X < minX) minX = point.X;
                if (point.X > maxX) maxX = point.X;
                if (point.Y < minY) minY = point.Y;
                if (point.Y > maxY) maxY = point.Y;
            }
        }
        Bounds.Add(new RectangleF(minX, minY, maxX - minX, maxY - minY));
    }

    public FloorDecoration() { }

    public void Spin()
    {
        float newHeight = Width;
        float newWidth = Height;

        Height = newHeight;
        Width = newWidth;
    }

    public static FloorDecoration[] GetFloorDecorations()
    {
        return new FloorDecoration[]
        {
            new(200, -200, 100, 50, 75, 2, "./Images/mesa.png"),
            new(400, -900, 100, 50, 75, 4, "./Images/armario.png")
        };
    }
    
    public void Draw(Graphics g)
    {
        foreach (var bound in Bounds)
        {
            Image imageToDraw = this.Items[0];
            using (Pen pen = new Pen(Color.Red))
                g.DrawRectangle(pen, bound);
            
            g.DrawImage(imageToDraw, bound);
        }
    }

    public void DrawRec(Graphics g, FloorDecoration item)
    {

        if (item.Items.Count > 0)
        {
            PointF[][] floorDecoration = (item.X, item.Y, 20f, item.Width, item.Height, item.Depth)
                .Parallelepiped()
                .Isometric();

            foreach (var face in floorDecoration)
            {
                g.FillPolygon(Brushes.Red, face);
            }
        }
    }


    public void OnFloorDecorationClick(Point mouseLocation)
    {
        foreach (var bound in Bounds)
        {
            if (bound.Contains(mouseLocation))
            {
                Clicked = true;
                MoveOn = !MoveOn;
                break;
            }
            else Clicked = false;
        }
    }

    public void Move(Point mouseLocation)
    {
        if (!MoveOn)
        {
            lastMouseLocation = Point.Empty;
            return;
        }

        if (lastMouseLocation == Point.Empty)
        {
            lastMouseLocation = mouseLocation;
            return;
        }

        float deltaX = mouseLocation.X - lastMouseLocation.X;
        float deltaY = mouseLocation.Y - lastMouseLocation.Y;

        var newMouseLocation = new PointF(deltaX, deltaY).Normal();

        this.X += newMouseLocation.X;
        this.Y += newMouseLocation.Y;

        lastMouseLocation = mouseLocation;
        updateBounds();
    }

    public void Store()
    {
        throw new NotImplementedException();
    }
}