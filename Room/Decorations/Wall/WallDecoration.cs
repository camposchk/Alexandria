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
    public Room Room { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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

    public void TryMove(Point mouseLocation)
    {
        throw new System.NotImplementedException();
    }

    public void Spin()
    {
        throw new System.NotImplementedException();
    }

    public void Store()
    {
        throw new System.NotImplementedException();
    }

    public void Draw(Graphics g, float x, float y)
    {
        throw new System.NotImplementedException();
    }
}