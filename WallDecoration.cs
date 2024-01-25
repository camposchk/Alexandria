using System.Collections.Generic;
using System.Drawing;

public class WallDecoration : IDecoration
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Depth { get; set; }

    public int TileSize { get; set; }
    public List<Image> Items { get; set; }

    public WallDecoration(float x, float y, float z, float width, float height, float depth, int tilesize, string imgPath)
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

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public void Spin()
    {
        throw new System.NotImplementedException();
    }
}