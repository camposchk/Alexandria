using System;
using System.Drawing;

public interface IDecoration
{
    void Move(Point mouseLocation);

    void Spin();

    void Store();
}