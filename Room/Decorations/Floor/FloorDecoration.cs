using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
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
    public float Cost { get; private set; }
    public List<Image> Items { get; set; }

    public List<RectangleF> Bounds { get; private set; } = new List<RectangleF>();

    public bool MoveOn = false;
    public bool SpinOn = false;
    public bool StoreOn = false;

    public bool Clicked = false;
    public bool OpenFloorMenu = false;

    public Point lastMouseLocation = Point.Empty;

    public RectangleF MenuFloorMove { get; private set; }
    public RectangleF MenuFloorSpin { get; private set; }
    public RectangleF MenuFloorStore { get; private set; }

    public FloorDecoration(float cost, float x, float y, float width, float height, float depth, int tilesize, string imgPath)
    {
        this.Cost = cost;
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
        this.Depth = depth;
        this.TileSize = tilesize;

        this.Items = new(){Bitmap.FromFile(imgPath)};

        updateBounds();
    }

    public static FloorDecoration[] GetFloorDecorations()
    {
        return new FloorDecoration[]
        {
            new(80, 300, -900, 100, 100, 75, 2, "./Images/couch.png"),
            new(20, 400, -900, 50, 50, 150, 2, "./Images/lamp.png"),
            new(40, 500, -900, 100, 100, 100, 2, "./Images/table.png"),
            new(100, 600, -900, 50, 125, 150, 2, "./Images/closet.png"),
            new(50, 700, -900, 100, 50, 100, 2, "./Images/chair.png"),
        };
    }

    public void Draw(Graphics g)
    {
        foreach (var bound in Bounds)
        {
            Image imageToDraw = this.Items[0];
            
            if (MoveOn)
                imageToDraw = SetImageOpacity(imageToDraw, 0.5f);
            if (!MoveOn)
                imageToDraw = SetImageOpacity(imageToDraw, 1f);

            
            if(OpenFloorMenu)
            {
                MenuFloorMove = new RectangleF(bound.X, bound.Y - 40, bound.Width, 30);
                MenuFloorSpin = new RectangleF(bound.X, MenuFloorMove.Y - 35, bound.Width, 30);
                MenuFloorStore = new RectangleF(bound.X, MenuFloorSpin.Y - 35, bound.Width, 30);

                g.DrawRectangle(Pens.Red, MenuFloorMove);
                g.DrawRectangle(Pens.Blue, MenuFloorSpin);
                g.DrawRectangle(Pens.Yellow, MenuFloorStore);
            }

            g.DrawImage(imageToDraw, bound);
            g.DrawRectangle(Pens.Red, bound);
        }
        
        // Brush brush = Brushes.Azure;
        // foreach (var face in floorDecoration)
        // {
        //     g.FillPolygon(brush, face);
        //     brush = GetDarkerBrush(brush);
        // }
    }
    Brush GetDarkerBrush(Brush originalBrush)
    {
        Color originalColor = ((SolidBrush)originalBrush).Color;

        float factor = 0.9f;

        int red = (int)(originalColor.R * factor);
        int green = (int)(originalColor.G * factor);
        int blue = (int)(originalColor.B * factor);

        Color darkerColor = Color.FromArgb(red, green, blue);

        return new SolidBrush(darkerColor);
    }
    
    public void Spin()
    {
        if (SpinOn)
        {
            float newHeight = this.Width;
            float newWidth = this.Height;

            this.Height = newHeight;
            this.Width = newWidth;

            updateBounds();
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
        var cursor = new PointF(mouseLocation.X, mouseLocation.Y);
        var normalCursor = cursor.Normal(20);

        this.X = snapToGrid(normalCursor.X, 50);
        this.Y = snapToGrid(normalCursor.Y, 50);

        lastMouseLocation = mouseLocation;
        updateBounds();
    }

    private float snapToGrid(float value, int gridSize)
    {
        return (float)(Math.Round(value / gridSize) * gridSize);
    }

    public void Store()
    {
        throw new NotImplementedException();
    }

    public void OnFloorDecorationClick(Point mouseLocation)
    {
        if (MenuFloorMove.Contains(mouseLocation))
        {
            MoveOn = true;
            Clicked = true;
            OpenFloorMenu = false; 

            var point = new PointF(this.X, this.Y).Isometric();

            point.Y -=  75;
            
            point = point.Normal();

            this.X = point.X;
            this.Y = point.Y;

            updateBounds();
            return;
        }
        else MoveOn = false;

        if (MenuFloorSpin.Contains(mouseLocation))
        {
            SpinOn = true;
            Clicked = true;
            OpenFloorMenu = false; 
            return;
        }
        else SpinOn = false;

        if (MenuFloorStore.Contains(mouseLocation))
        {
            StoreOn = true;
            Clicked = true;
            OpenFloorMenu = false; 
            return;
        }
        else StoreOn = false;

        foreach (var bound in Bounds)
        {
            if (bound.Contains(mouseLocation))
            {
                Clicked = true;
                OpenFloorMenu = !OpenFloorMenu;

                break;
            }
            else Clicked = false;
        }
    }

    PointF[][] floorDecoration;
    void updateBounds()
    {
        Bounds.Clear();

        floorDecoration = (this.X, this.Y, 20f, this.Width, this.Height, this.Depth)
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

    private Image SetImageOpacity(Image image, float opacity)
    {
        Bitmap bmp = new(image.Width, image.Height);

        using Graphics gfx = Graphics.FromImage(bmp);
        
        ColorMatrix matrix = new()
        {
            Matrix33 = opacity
        };

        using ImageAttributes attributes = new();
        attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

        gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);

        return bmp;
    }
}