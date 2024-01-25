using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class FloorDecoration : IDecoration
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Depth { get; set; }

    public int TileSize { get; set; }
    public List<Image> Items { get; set; }

    public FloorDecoration(float x, float y, float z, float width, float height, float depth, int tilesize, string imgPath)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
        this.Width = width;
        this.Height = height;
        this.Depth = depth;
        this.TileSize = tilesize;

        this.Items = new(){Bitmap.FromFile(imgPath)};
    }

    public FloorDecoration() { }

    public void Move(Point mouseLocation)
    {

    }

    public void Spin()
    {
        float newHeight = Width;
        float newWidth = Height;

        Height = newHeight;
        Width = newWidth;
    }

    public FloorDecoration[] GetFloorDecorations()
    {
        return new FloorDecoration[]
        {
            new(200, -200, 200, 100, 50, 75, 2, "./Images/mesa.png"),
            new(400, -900, 200, 100, 50, 75, 4, "./Images/armario.png")
        };
    }

    // public void Draw(Graphics g, FloorDecoration item)
    // {
    //     if (item.Items.Count > 0)
    //     {
    //         Image imageToDraw = item.Items[0];

    //         RectangleF imageRect = new RectangleF(item.X, item.Y, item.Width, item.Height);

    //         g.DrawImage(imageToDraw, imageRect);
    //     }
    // }

    public void Draw(Graphics g, FloorDecoration item)
    {
        if (item.Items.Count > 0)
        {
            Image imageToDraw = item.Items[0];
            PointF[][] floorDecoration = (item.X, item.Y, item.Z, item.Width, item.Height, item.Depth)
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

            float width = maxX - minX;
            float height = maxY - minY;

            using (Pen pen = new Pen(Color.Red))
                g.DrawRectangle(pen, minX, minY, width, height);
            
            g.DrawImage(imageToDraw, minX, minY, width, height);
        }
    }

    public void DrawRec(Graphics g, FloorDecoration item)
    {

        if (item.Items.Count > 0)
        {
            Image imageToDraw = item.Items[0];
            PointF[][] floorDecoration = (item.X, item.Y, item.Z, item.Width, item.Height, item.Depth)
                .Parallelepiped()
                .Isometric();

            foreach (var face in floorDecoration)
            {
                g.FillPolygon(Brushes.Red, face);
            }
        }
    }

    // public void OnFloorDecorationClick(Point mouseLocation)
    // {
    //     foreach(var item in fl o)
    //     {
    //         if (item.Contains(mouseLocation))
    //         {
    //             Move();
    //         }
    //     }

    // }

    public void Move()
    {
        throw new NotImplementedException();
    }
}