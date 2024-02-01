using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Habbosch;
public class TestDecoration : IDecoration
{
    public int X { get; set; }
    public int Y { get; set; }

    public float Cost => throw new NotImplementedException();

    public List<Image> Items => throw new NotImplementedException();

    List<(PointF[], Brush)> faces = new();
    public TestDecoration()
    {
        add(
            (0f, 0f, 80f, 10f, 10f, 60f)
            .Parallelepiped()
            .Isometric(), 
            Brushes.Brown
        );

        add(
            (20f, 0f, 80f, 10f, 10f, 60f)
            .Parallelepiped()
            .Isometric(), 
            Brushes.Brown
        );

        add(
            (0f, 20f, 80f, 10f, 10f, 60f)
            .Parallelepiped()
            .Isometric(), 
            Brushes.Brown
        );

        add(
            (20f, 20f, 80f, 10f, 10f, 60f)
            .Parallelepiped()
            .Isometric(), 
            Brushes.Brown
        );
        
        add(
            (0f, 0f, 100f, 30f, 30f, 20f)
            .Parallelepiped()
            .Isometric(), 
            Brushes.DarkOrange
        );
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
    
    public void Draw(Graphics g)
    {
        var basePt = PointF.Empty.Isometric();
        g.TranslateTransform(basePt.X + X, basePt.Y + Y);

        foreach (var face in faces)
            g.FillPolygon(face.Item2, face.Item1);

        g.ResetTransform();
    }

    public void Move(Point mouseLocation)
    {
        throw new NotImplementedException();
    }

    public void Spin()
    {
        throw new NotImplementedException();
    }

    public void Store()
    {
        throw new NotImplementedException();
    }
}