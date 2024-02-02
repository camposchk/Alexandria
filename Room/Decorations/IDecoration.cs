using System;
using System.Drawing;

public interface IDecoration
{
    Room Room { get; set; }
    void Draw(Graphics g, float x, float y);
    void Click(PointF cursor) { }
    void TryMove(Point mouseLocation);

    void Spin();

    void Store();
}