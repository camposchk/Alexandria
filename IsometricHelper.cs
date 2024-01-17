using System.Linq;
using System.Drawing;
using System.Numerics;

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

    public static Vector3 Normal(this Vector2 vec, float z)
    {
        const float sqrt2 = 1.41421356237f;
        const float sqrt3 = 1.73205080757f;

        return new Vector3(
             sqrt2 * sqrt3 / 2 * (sqrt2 / sqrt3 * z - vec.Y) + vec.X / sqrt2,
             sqrt2 * sqrt3 / 2 * (sqrt2 / sqrt3 * z - vec.Y) - vec.X / sqrt2,
             z
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

    public static PointF Isometric(this Vector3 vec)
    {
        const float sqrt2 = 1.41421356237f;
        const float sqrt3 = 1.73205080757f;

        return new PointF(
            sqrt2 * (vec.X - vec.Y) / 2,
            sqrt2 / sqrt3 * vec.Z - (vec.X + vec.Y) /(sqrt2 * sqrt3)
        );
    }

    public static PointF[] Isometric(this PointF[] pts, float z = 0f)
    {
        return pts
            .Select(p => p.Isometric(z))
            .ToArray();
    }

    public static PointF[] Isometric(this Vector3[] pts)
    {
        return pts
            .Select(p => p.Isometric())
            .ToArray();
    }

    public static PointF[][] Isometric(this Vector3[][] pts)
    {
        return pts
            .Select(p => p.Isometric())
            .ToArray();
    }

    public static Vector3[][] Parallelepiped(this (float x, float y, float z, float width, float height, float depth) tuple)
    {
        (float x, float y, float z, float width, float height, float depth) = tuple;
        return new Vector3[][] {
            new Vector3[] {
                new Vector3(x, y, z),
                new Vector3(x + width, y, z),
                new Vector3(x + width, y + height, z),
                new Vector3(x, y + height, z),
            },
            new Vector3[] {
                new Vector3(x, y, z),
                new Vector3(x + width, y, z),
                new Vector3(x + width, y, z + depth),
                new Vector3(x, y, z + depth),
            },
            new Vector3[] {
                new Vector3(x, y, z),
                new Vector3(x, y + height, z),
                new Vector3(x, y + height, z + depth),
                new Vector3(x, y, z + depth),
            },
        };
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
