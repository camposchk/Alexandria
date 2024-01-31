using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Habbosch;
public class TestDecoration : IDecoration
{
    public Vector3 Root { get; set; }
    List<(PointF[], Brush)> faces = new();
    public TestDecoration()
    {
        Root = new Vector3(0f, 0f, 20f);
        add(
            (0f, 0f, -20f, 7.5f, 7.5f, 40f)
            .Parallelepiped()
            .Isometric(), 
            Brushes.Brown
        );

        add(
            (42.5f, 0f, -20f, 7.5f, 7.5f, 40f)
            .Parallelepiped()
            .Isometric(), 
            Brushes.Brown
        );

        add(
            (0f, 42.5f, -20f, 7.5f, 7.5f, 40f)
            .Parallelepiped()
            .Isometric(), 
            Brushes.Brown
        );

        add(
            (42.5f, 42.5f, -20f, 7.5f, 7.5f, 40f)
            .Parallelepiped()
            .Isometric(), 
            Brushes.Brown
        );
        
        add(
            (0f, 0f, -40f, 50f, 50f, 20f)
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
    
    public void Draw(Graphics g, float x, float y)
    {
        var basePt = PointF.Empty.Isometric();
        g.TranslateTransform(basePt.X + x, basePt.Y + y);

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