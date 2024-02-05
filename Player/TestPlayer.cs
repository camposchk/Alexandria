using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

public class TestPlayer 
{
    public Vector3 Root { get; set; }

    List<(PointF[], Brush)> faces = new();

    public TestPlayer()
    {
        Root = new Vector3(0f, 0f, 20f);
        add(
            (0f, 0f, -80f, 50f, 50f, 100f)
            .Parallelepiped()
            .Isometric(), 
            Brushes.Yellow
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

    // public void StartMove(this (int i, int j) tuple)
    // {
    //     (i, j) = tuple;
    //     target = new Point(i, j);
    // }

    // public void Move()
    // {
    //     // Calculate the vector from the current position to the target
    //     Vector3 direction = target - Root;
    //     float distance = direction.Length();

    //     // Normalize the direction vector, and then move towards the target
    //     if (distance < moveSpeed)
    //     {
    //         Root = target; // If close enough to the target, just set the position to the target
    //     }
    //     else
    //     {
    //         direction = Vector3.Normalize(direction);
    //         Root += direction * moveSpeed; // Move towards the target
    //     }
    // }
}