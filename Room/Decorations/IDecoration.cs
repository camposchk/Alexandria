using System;
using System.Drawing;

public interface IDecoration
{
    void Draw(Graphics g, float x, float y);
    void Move(Point mouseLocation);

    void Spin();

    void Store();
}