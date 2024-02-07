using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

public class TestPlayer : IDecoration
{
    private float targetX = float.NaN;
    private float targetY = float.NaN;
    private float ftargetX = float.NaN;
    private float ftargetY = float.NaN;

    public Vector3 Root { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float Width { get; set; } = 50;
    public float Height { get; set; } = 50;
    public float Depth { get; set; } = 100;
    public float Ruby { get; set; } = 100;
    public Color Color { get; set; } = Colors.GetRandomColor();
    public Room Room { get; set; }

    public float Cost => 0f;

    public int Quantity { get; set; }

    public List<Image> Items => throw new NotImplementedException();

    List<Message> Messages = new List<Message>();

    public TestPlayer()
    {
        Root = new Vector3(0f, 0f, 20f);
    }

    public void Speak(string text)
    {
        var ballon = new PointF((int)this.X, (int)this.Y).Isometric();
        Message message = new Message(ballon, text, (int)this.Depth);
        this.Messages.Add(message);
    }

    public void Draw(Graphics g, float x, float y)
    {
        var normal = new PointF(x, y).Normal();
        if (float.IsNaN(targetX))
        {
            this.X = normal.X;
            this.Y = normal.Y;
        }
        PointF[][] player = (this.X, this.Y, 20f - Depth, Width, Height, Depth)
            .Parallelepiped()
            .Isometric();

        Brush brush = new SolidBrush(Color);
        foreach (var face in player)
        {
            g.FillPolygon(brush, face);
            brush = Colors.GetDarkerBrush(brush);
        }

        Messages = Messages
            .Where(m => m.IsActivated)
            .ToList();
        foreach (var message in Messages)
        {
            message.Draw(g);
        }
        
        this.i = Room.IndexSelection.X;
        this.j = Room.IndexSelection.Y;
        targetX = Room.NormalSelection.X;
        targetY = Room.NormalSelection.Y;
    }

    DateTime last = DateTime.Now;
    bool activated = false;
    public void MovePlayer()
    {   
        var now = DateTime.Now;
        var timePassed = now - last;
        last = now;
        var secs = (float)timePassed.TotalSeconds;
        if (!activated)
            return;

        var dx = ftargetX - X;
        var dy = ftargetY - Y;
        var mod = MathF.Sqrt(dx * dx + dy * dy);
        if (mod < 2f)
        {
            activated = false;
            Room.Remove(this);
            Room.Set(this, fi, fj);
            return;
        }

        var dirX = dx / mod;
        var dirY = dy / mod;
        X += 300 * dirX * secs;
        Y += 300 * dirY * secs;
    }

    List<(int i, int j)> path = new List<(int i, int j)>();
    public void ActivePlayer()
    {
        if (Room.Decorations[i, j] is not null)
            return;
        activated = true;
        ftargetX = targetX;
        ftargetY = targetY;
        fi = i;
        fj = j;
        return;
        var start = Room.Find(this);
        var goal = (this.i, this.j);
        var queue = new PriorityQueue<(int i, int j), float>();
        var distMap = new Dictionary<(int i, int j), float>();
        var comeMap = new Dictionary<(int i, int j), (int i, int j)>();
        var visited = new bool[Room.VecWidth, Room.VecHeight];

        distMap[start] = 0;
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var crr = queue.Dequeue();
            if (visited[crr.i, crr.j])
                continue;
            visited[crr.i, crr.j] = true;
            if (crr == goal)
                break;
            
            var neighborhood = new (int i, int j)[] {
                (crr.i + 1, crr.j), (crr.i - 1, crr.j),
                (crr.i, crr.j - 1), (crr.i, crr.j + 1)
            };
            foreach (var neighbor in neighborhood)
            {
                if (neighbor.i < 0 || neighbor.j < 0 || neighbor.i >= Room.VecWidth || neighbor.j >= Room.VecHeight)
                    continue;
                if (Room.Decorations[neighbor.i, neighbor.j] is not null)
                    continue;

                if (!distMap.ContainsKey(neighbor))
                {
                    distMap.Add(neighbor, float.PositiveInfinity);
                    comeMap.Add(neighbor, (-1, -1));
                }
                
                var newDist = distMap[crr] + 1;
                var oldDist = distMap[neighbor];
                if (newDist > oldDist)
                    continue;
                
                distMap[neighbor] = newDist;
                comeMap[neighbor] = crr;
                queue.Enqueue(neighbor, newDist);
            }
        }

        path.Clear();
        var it = goal;
        while (it != start)
        {
            path.Add((it.i, it.j));
            it = comeMap[it];
        }
        path.Reverse();
    }

    int i = 0, j = 0;
    int fi = 0, fj = 0;
    public void TryMove(Point mouseLocation) { }

    public void Spin() { }

    public void Store() { }
}