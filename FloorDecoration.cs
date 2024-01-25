using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualBasic;

public class FloorDecoration
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

    public void Move()
    {

    }

    public void Spin(float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * (float)Math.PI / 180.0f;

        float newX = X * (float)Math.Cos(angleInRadians) - Z * (float)Math.Sin(angleInRadians);
        float newZ = X * (float)Math.Sin(angleInRadians) + Z * (float)Math.Cos(angleInRadians);

        X = newX;
        Z = newZ;
    }

    public FloorDecoration[] GetFloorDecorations()
    {
        return new FloorDecoration[]
        {
            new(100, 100, 0, 200, 100, 100, 2, "./Images/placeholder.png"),
            new(200, 200, 200, 200, 200, 100, 4, "./Images/placeholder.png")
        };
    }

    public void Draw(Graphics g, FloorDecoration item)
    {
        if (item.Items.Count > 0)
        {
            Image imageToDraw = item.Items[0];

            RectangleF imageRect = new RectangleF(item.X, item.Y, item.Width, item.Height);

            g.DrawImage(imageToDraw, imageRect);
        }
    }

}