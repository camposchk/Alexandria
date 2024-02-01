using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;

public interface IDecoration
{
    float Cost { get; }
    public List<Image> Items { get; }
    void Draw(Graphics g);
    void Move(Point mouseLocation);

    void Spin();

    void Store();
}