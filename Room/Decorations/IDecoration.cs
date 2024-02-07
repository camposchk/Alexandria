using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;

public interface IDecoration
{
    Room Room { get; set; }
    void Draw(Graphics g, float x, float y);
    void Click(PointF cursor) { }
    void TryMove(Point mouseLocation);
    float Cost { get; }
    int Quantity { get; set; }

    void Spin();

    void Store();
}