using System;
using System.Drawing;

public interface IDecoration
{
    void Draw(Graphics g);
    void Move(Point mouseLocation);

    void Spin();

    void Store();
}