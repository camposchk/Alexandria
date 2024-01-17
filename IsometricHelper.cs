using System.Drawing;
using System.Linq;

public static class IsometricHelper
{
    public static PointF Normal(this PointF p, float z = 0f)
    {
        const float sqrt2 = 1.41421356237f;
        const float sqrt3 = 1.73205080757f;

        return new PointF(
             sqrt2 * sqrt3 / 2 * (sqrt2 / sqrt3 * z - p.Y) + p.X / sqrt2,
             sqrt2 * sqrt3 / 2 * (sqrt2 / sqrt3 * z - p.Y) - p.X / sqrt2
        );
    }

    public static PointF[] Normal(this PointF[] pts, float z = 0f)
    {
        return pts
            .Select(p => p.Normal(z))
            .ToArray();
    }

    public static PointF Isometric(this PointF p, float z = 0f)
    {
        const float sqrt2 = 1.41421356237f;
        const float sqrt3 = 1.73205080757f;

        return new PointF(
            sqrt2 * (p.X - p.Y) / 2,
            sqrt2 / sqrt3 * z - (p.X + p.Y) /(sqrt2 * sqrt3)
        );
    }

    public static PointF[] Isometric(this PointF[] pts, float z = 0f)
    {
        return pts
            .Select(p => p.Isometric(z))
            .ToArray();
    }

    public static PointF[] Rectangle(this (float x, float y, float width, float height) tuple)
    {
        return new PointF[] {
            new PointF(tuple.x, tuple.y),
            new PointF(tuple.x + tuple.width, tuple.y),
            new PointF(tuple.x + tuple.width, tuple.y + tuple.height),
            new PointF(tuple.x, tuple.y + tuple.height),
        };
    }

    public static PointF[] Rectangle(this (int x, int y, int width, int height) tuple)
    {
        return new PointF[] {
            new PointF(tuple.x, tuple.y),
            new PointF(tuple.x + tuple.width, tuple.y),
            new PointF(tuple.x + tuple.width, tuple.y + tuple.height),
            new PointF(tuple.x, tuple.y + tuple.height),
        };
    }
}
