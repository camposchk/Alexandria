using System.Collections.Generic;
using System.Drawing;

public class FloorTexture
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public List<Image> Textures { get; set; }

    public FloorTexture(float x, float y, float width, float height, string imgPath)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;

        this.Textures = new(){Bitmap.FromFile(imgPath)};
    }

    public static FloorTexture[] GetFloorTextures()
    {
        return new FloorTexture[]
        {
            new(300, -900, 50, 50, "./Images/Decos/Floor/Textures/wood.jpg")
        };
    }
}