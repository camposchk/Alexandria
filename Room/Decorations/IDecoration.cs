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
    public List<Image> Items { get; }
    void Draw(Graphics g);
    void Move(Point mouseLocation);

    void Spin();

    void Store();
}